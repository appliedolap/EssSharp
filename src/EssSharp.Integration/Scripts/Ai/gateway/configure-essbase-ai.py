#!/usr/bin/env python3
"""Gives Essbase applications an AI connection backed by this stack's Oracle AI Database.

Essbase is set up over REST the way a user would set it up: a connection to the database, chat and vector credentials,
an AI connection that uses them, the association between that connection and each application, and the job that
vectorizes each cube's outline.

The credentials carry a key that signs each request to the model service. The AI gateway never checks the signature,
so a key made for the occasion will do, but it has to be a real key because the database parses it.

It runs in the AI gateway, inside the AI database's network, each time that database starts: it
waits for the database, then configures until a pass succeeds. Every step checks before it acts, so a pass can be
repeated. It finishes with a warm-up question. Settings come from the environment (see SETTINGS).
"""
import base64
import hashlib
import json
import os
import socket
import subprocess
import sys
import tempfile
import time
import urllib.error
import urllib.parse
import urllib.request

SETTINGS = {
    "ESSBASE_URL": "http://essbase:9000",
    "ESSBASE_USER": "admin",
    "ESSBASE_PASSWORD": "password1",
    "AI_DATABASE_HOST": "ai-database",
    "APP_USER": "ESSAI",
    "APP_USER_PASSWORD": "AiUser_Passw0rd1",
    "OCI_REGION": "us-chicago-1",
    "AI_CHAT_MODEL": "cohere.command-a-03-2025",
    "AI_EMBED_MODEL": "cohere.embed-english-v3.0",
    # Space separated Application.Cube pairs to limit the setup to, each waited for (a new stack imports them after it
    # starts) and then skipped if it has still not appeared. Empty for every cube on the server.
    "AI_APPLICATIONS": "",
    "AI_APPLICATION_WAIT_MINUTES": "60",
    # The question the warm-up asks once everything is configured, and the cube it asks (any configured cube when that
    # one is not).
    "AI_WARMUP_QUERY": "Show actual sales by year.",
    "AI_WARMUP_CUBE": "Sample.Basic",
    # How long to keep trying before giving up until the next start.
    "AI_SETUP_MINUTES": "60",
}
S = {name: os.environ.get(name) or default for name, default in SETTINGS.items()}

DATABASE_CONNECTION = "ess_ai_database"
CHAT_CREDENTIAL = "ess_ai_chat"
VECTOR_CREDENTIAL = "ess_ai_vector"
AI_CONNECTION = "ess_ai_genai"
OCID = "ocid1.%s.oc1..aaaaaaaaessbaselocalstack"
EMBED_URL = "https://inference.generativeai.%s.oci.oraclecloud.com/20231130/actions/embedText" % S["OCI_REGION"]

AUTHORIZATION = "Basic " + base64.b64encode(("%s:%s" % (S["ESSBASE_USER"], S["ESSBASE_PASSWORD"])).encode()).decode()


def rest(method, path, body=None):
    """Sends a request to the Essbase REST API and returns the status and the body text."""
    headers = {"Authorization": AUTHORIZATION, "Accept": "application/json"}
    # A GET that names a content type is refused by some routes (ai/connection answers 415).
    if method != "GET":
        headers["Content-Type"] = "application/json"
    request = urllib.request.Request(
        S["ESSBASE_URL"].rstrip("/") + "/essbase/rest/v1/" + path,
        method=method,
        data=None if body is None else json.dumps(body).encode(),
        headers=headers)
    try:
        with urllib.request.urlopen(request, timeout=600) as response:
            return response.status, response.read().decode()
    except urllib.error.HTTPError as error:
        return error.code, error.read().decode()
    except (urllib.error.URLError, OSError) as error:
        return 0, str(error)


def wait_for(path, minutes, what):
    """Waits until a GET of the path succeeds, and returns whether it did."""
    deadline = time.time() + minutes * 60
    while True:
        if rest("GET", path)[0] == 200:
            return True
        if time.time() >= deadline:
            return False
        print("    waiting for %s" % what, flush=True)
        time.sleep(30)


def require(response, step):
    status, body = response
    if not 200 <= status < 300:
        raise RuntimeError("%s failed: HTTP %d. %s" % (step, status, body))
    return body


def require_or_exists(response, step):
    if "already exist" in response[1].lower():
        return response[1]
    return require(response, step)


def signing_key():
    """Returns a new RSA private key (PKCS#8, base64 without the armor) and its OCI style fingerprint."""
    with tempfile.TemporaryDirectory() as directory:
        key = os.path.join(directory, "key.pem")
        subprocess.run(["openssl", "genpkey", "-algorithm", "RSA", "-pkeyopt", "rsa_keygen_bits:2048", "-out", key],
                       check=True, capture_output=True)
        public = subprocess.run(["openssl", "pkey", "-in", key, "-pubout", "-outform", "DER"],
                                check=True, capture_output=True).stdout
        with open(key) as file:
            private = "".join(line.strip() for line in file if not line.startswith("-----"))
    digest = hashlib.md5(public).hexdigest()
    return private, ":".join(digest[index:index + 2] for index in range(0, len(digest), 2))


def vectorize(application, cube):
    """Starts the vectorize outline job and waits for it to finish."""
    body = require(rest("POST", "ai/applications/%s/job/vectorize/databases/%s" % (application, cube)),
                   "Start the vectorize outline job for %s.%s" % (application, cube))
    job = json.loads(body)["job_ID"]
    deadline = time.time() + 30 * 60
    while time.time() < deadline:
        status = json.loads(require(rest("GET", "jobs/%s" % job), "Read vectorize job %s" % job))
        # 100 is in progress, 200 completed, 300 completed with warnings; anything else is a failure.
        code = status.get("statusCode")
        if code in (200, 300):
            print("    vectorize job %s completed%s" % (job, " with warnings" if code == 300 else ""), flush=True)
            return
        if code != 100:
            raise RuntimeError("Vectorize job %s for %s.%s failed: %s" % (job, application, cube, json.dumps(status)))
        time.sleep(3)
    raise RuntimeError("Vectorize job %s for %s.%s did not finish within 30 minutes" % (job, application, cube))


def warm_up(application, cube):
    """Asks one question, so that the first real one does not fail.

    Essbase keeps a pool of connections to the AI database, and after the database restarts the first request through a
    stale connection fails. Asking until a request goes through clears them. A failure here is reported but is not fatal.
    """
    path = "ai/applications/%s/databases/%s" % (application, cube)
    profile = "WARMUP_%d" % int(time.time())
    query = urllib.parse.quote(S["AI_WARMUP_QUERY"])
    try:
        for attempt in range(1, 6):
            status, body = rest("POST", "%s/mdxgenerator?profileName=%s&isConvStart=true&includeAttributesInNlq=false&nlq=%s"
                                % (path, profile, query))
            try:
                answered = status == 200 and bool(json.loads(body).get("mdx"))
            except ValueError:
                answered = False
            if answered:
                print("==> Warm-up answered on attempt %d" % attempt, flush=True)
                return
            time.sleep(5)
        print("==> Warm-up got no MDX after 5 attempts (HTTP %d): %s" % (status, body), flush=True)
    finally:
        rest("DELETE", "%s/conversationHistory?profileName=%s" % (path, profile))


def ai_disabled(application, cube):
    """Returns whether Essbase reports its AI registry settings as off, or cannot answer yet (it is restarting to apply
    them). The check needs an existing cube."""
    status, body = rest("POST", "ai/applications/%s/databases/%s/listSampleQueries" % (application, cube))
    return status in (0, 502, 503) or "Registry setting" in body or "No registry is enabled" in body


def wait_for_ai(application, cube, minutes=30):
    """Waits for Essbase to report AI as on. On a new stack the Essbase container restarts its server to apply the AI
    registry settings once its own setup is done (waitForAndSetUpEnvironment.sh)."""
    deadline = time.time() + minutes * 60
    while ai_disabled(application, cube):
        if time.time() >= deadline:
            raise RuntimeError("Essbase still reports AI as off after %d minutes; are the AI flags set in its registry "
                               "(ENABLE_ESSBASE_MCP on the database service)?" % minutes)
        print("    waiting for Essbase to apply its AI registry settings", flush=True)
        time.sleep(30)


def main():
    print("==> Essbase at %s" % S["ESSBASE_URL"], flush=True)
    if not wait_for("about", 30, "Essbase to answer"):
        raise RuntimeError("Essbase did not answer within 30 minutes")

    listed = [tuple(pair.partition(".")[::2]) for pair in S["AI_APPLICATIONS"].split()]
    first = wait_for_first_cube(listed)
    if first:
        # On a new stack, Essbase turns AI on by restarting once it has imported its cubes, so every cube is there after.
        wait_for_ai(*(urllib.parse.quote(part) for part in first))

    print("==> Database connection %s" % DATABASE_CONNECTION, flush=True)
    if rest("GET", "connections/" + DATABASE_CONNECTION)[0] != 200:
        require(rest("POST", "connections", {
            "name": DATABASE_CONNECTION, "description": "The Oracle AI Database of this stack",
            "type": "DB", "subtype": "ORACLE", "host": S["AI_DATABASE_HOST"], "port": 1521, "service": "FREEPDB1",
            "user": S["APP_USER"], "password": S["APP_USER_PASSWORD"]}), "Create the database connection")
    require(rest("POST", "connections/%s/actions/test" % DATABASE_CONNECTION), "Connect to the AI database")

    print("==> Chat and vector credentials", flush=True)
    private_key, fingerprint = signing_key()
    ocids = {"user_ocid": OCID % "user", "tenancy_ocid": OCID % "tenancy", "compartment_ocid": OCID % "compartment",
             "private_key": private_key, "fingerprint": fingerprint}
    require_or_exists(rest("POST", "ai/dbconnection/%s/chat/credential/signingkey/%s" % (DATABASE_CONNECTION, CHAT_CREDENTIAL),
                           dict(ocids, chat_model=S["AI_CHAT_MODEL"])), "Create the chat credential")
    require_or_exists(rest("POST", "ai/dbconnection/%s/vector/credential/%s" % (DATABASE_CONNECTION, VECTOR_CREDENTIAL),
                           dict(ocids, url=EMBED_URL, embed_model=S["AI_EMBED_MODEL"])), "Create the vector credential")

    print("==> AI connection %s" % AI_CONNECTION, flush=True)
    if rest("GET", "connections/" + AI_CONNECTION)[0] != 200:
        require(rest("POST", "connections", {
            "name": AI_CONNECTION, "description": "The AI connection of this stack", "type": "AI", "subtype": "AI",
            "ai_connection": DATABASE_CONNECTION, "chat_credential": CHAT_CREDENTIAL,
            "vector_credential": VECTOR_CREDENTIAL, "chat_model": S["AI_CHAT_MODEL"],
            "oci_compartment_id": OCID % "compartment", "region": S["OCI_REGION"], "url": EMBED_URL,
            "embed_model": S["AI_EMBED_MODEL"], "transfer_timeout": "3000", "oci_apiformat": "GENERIC"}),
            "Create the AI connection")

    ready = []
    associated = {}  # application -> the cubes vectorized in this pass, for the applications this pass associated
    for application, cube in listed or all_cubes():
        pair = "%s.%s" % (application, cube)
        quoted = urllib.parse.quote(application)
        if listed and not wait_for("applications/%s/databases/%s" % (quoted, urllib.parse.quote(cube)),
                                   int(S["AI_APPLICATION_WAIT_MINUTES"]), pair):
            print("==> Skipping %s: it does not exist" % pair, flush=True)
            continue
        if application not in associated:
            status, body = rest("GET", "ai/connection?application=" + quoted)
            if status == 200 and AI_CONNECTION in body:
                print("==> %s is already associated with %s" % (pair, AI_CONNECTION), flush=True)
                ready.append((quoted, urllib.parse.quote(cube)))
                continue
            print("==> Associating %s with %s" % (application, AI_CONNECTION), flush=True)
            require(rest("POST", "ai/applications/%s/connection/%s" % (quoted, AI_CONNECTION)),
                    "Associate the AI connection with " + application)
            associated[application] = []

        print("==> Vectorizing %s" % pair, flush=True)
        try:
            vectorize(quoted, urllib.parse.quote(cube))
        except Exception as error:
            # One cube Essbase cannot vectorize does not hold up the others. An application left associated with none of
            # its cubes vectorized would be taken for a finished setup by the next run, so it is released to try again.
            print("==> Skipping %s: %s" % (pair, error), flush=True)
            if not associated[application]:
                rest("DELETE", "ai/applications/%s/connection" % quoted)
                del associated[application]
            continue
        associated[application].append(cube)
        ready.append((quoted, urllib.parse.quote(cube)))

    # The applications share one AI connection, and so one pool, so one warm-up serves them all.
    if ready:
        wanted = tuple(urllib.parse.quote(part) for part in S["AI_WARMUP_CUBE"].partition(".")[::2])
        warm_up(*(wanted if wanted in ready else ready[0]))

    print("Essbase AI configured", flush=True)


def all_cubes():
    """Returns every cube on the server, as (application, cube) pairs."""
    cubes = []
    for application in json.loads(require(rest("GET", "applications"), "List the applications")).get("items", []):
        quoted = urllib.parse.quote(application["name"])
        databases = json.loads(require(rest("GET", "applications/%s/databases" % quoted),
                                       "List the cubes of " + application["name"])).get("items", [])
        cubes.extend((application["name"], database["name"]) for database in databases)
    return cubes


def wait_for_first_cube(listed):
    """Waits for the first listed cube, or for any cube when none are listed, and returns it (or None)."""
    minutes = int(S["AI_APPLICATION_WAIT_MINUTES"])
    if listed:
        application, cube = listed[0]
        found = wait_for("applications/%s/databases/%s" % (urllib.parse.quote(application), urllib.parse.quote(cube)),
                         minutes, "%s.%s" % listed[0])
        return listed[0] if found else None
    deadline = time.time() + minutes * 60
    while True:
        cubes = all_cubes()
        if cubes or time.time() >= deadline:
            return cubes[0] if cubes else None
        print("    waiting for a cube", flush=True)
        time.sleep(30)


def wait_for_port(port, minutes, what):
    """Waits until something listens on the port of this network (the database's, shared with this service)."""
    deadline = time.time() + minutes * 60
    while time.time() < deadline:
        try:
            with socket.create_connection(("127.0.0.1", port), timeout=5):
                return
        except OSError:
            time.sleep(5)
    raise RuntimeError("Nothing listened for %s on port %d within %d minutes" % (what, port, minutes))


def wait_for_models(minutes=120):
    """Waits until the Ollama server named by AI_MODEL_URL has the chat and embedding models (the first start of a stack
    downloads them), leaving out a chat model served elsewhere (AI_CHAT_URL). Does nothing without AI_MODEL_URL."""
    url = (os.environ.get("AI_MODEL_URL") or "").rstrip("/")
    if not url:
        return
    wanted = [os.environ.get("AI_EMBED_MODEL_NAME", "mxbai-embed-large")]
    if not os.environ.get("AI_CHAT_URL"):
        wanted.append(os.environ.get("AI_CHAT_MODEL_NAME", "qwen2.5:7b"))
    wanted = [name if ":" in name else name + ":latest" for name in wanted]
    deadline = time.time() + minutes * 60
    while True:
        try:
            with urllib.request.urlopen(url + "/api/tags", timeout=10) as response:
                present = {model.get("name") for model in json.loads(response.read().decode()).get("models", [])}
        except (urllib.error.URLError, OSError, ValueError):
            present = set()
        missing = [name for name in wanted if name not in present]
        if not missing:
            return
        if time.time() >= deadline:
            raise RuntimeError("The models %s were not available within %d minutes" % (missing, minutes))
        print("    waiting for the models %s" % missing, flush=True)
        time.sleep(30)


def run():
    """Configures until a pass succeeds. The database finishes its own AI setup after it starts listening, so the first
    passes can fail on missing privileges or a pluggable database that is being reopened."""
    deadline = time.time() + int(S["AI_SETUP_MINUTES"]) * 60
    attempt = 0
    while True:
        attempt += 1
        try:
            wait_for_port(443, 5, "the AI gateway")
            wait_for_port(1521, 30, "the AI database")
            wait_for_models()
            main()
            return
        except Exception as error:
            if time.time() >= deadline:
                print("Essbase AI setup gave up after %d attempts: %s" % (attempt, error), flush=True)
                sys.exit(1)
            print("Essbase AI setup attempt %d failed, retrying in 30 seconds: %s" % (attempt, error), flush=True)
            time.sleep(30)


if __name__ == "__main__":
    run()
