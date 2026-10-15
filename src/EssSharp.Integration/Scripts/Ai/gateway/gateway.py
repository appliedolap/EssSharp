#!/usr/bin/env python3
"""The AI gateway: answers the calls an Oracle AI Database makes for Essbase's AI features.

Essbase's AI features do not call a model themselves. They ask an Oracle database to do it, through DBMS_CLOUD_AI (chat)
and DBMS_VECTOR_CHAIN (embeddings), and the database calls

    https://inference.generativeai.<region>.oci.oraclecloud.com/20231130/actions/chat
    https://inference.generativeai.<region>.oci.oraclecloud.com/20231130/actions/embedText

The gateway runs as a sidecar of the database container, sharing its network, where a hosts entry sends that host name
to 127.0.0.1. It presents a certificate for the host name from a local certificate authority that the database's SSL
wallet trusts (generate-certs.sh issues both). Requests are signed by the database with the credential's key; the
gateway does not check the signature.

It answers in one of two modes, chosen by AI_GATEWAY_MODE:

  * canned (for tests): deterministic answers. embedText returns a hashed bag-of-words vector per input, so texts that
    share words are close together, which is all Essbase's member lookup needs. Chat answers the question prompt from
    answers.json (keys matched against the lower-cased question), echoes the JSON back for the follow-up prompt that
    refines it, and gives a short fixed reply to anything else. To cover another question in a test, add an entry to
    answers.json; it is read on every request, so no restart is needed.
  * model (the default when AI_MODEL_URL is set): real models. Chat goes to AI_CHAT_MODEL_NAME and embedText to
    AI_EMBED_MODEL_NAME on the Ollama server at AI_MODEL_URL, translated between the OCI and Ollama formats. With
    AI_CHAT_URL also set (the base URL of an OpenAI-compatible API, such as https://api.openai.com/v1), chat goes there
    instead, with AI_CHAT_API_KEY as its key; embeddings stay on Ollama. In this mode the question prompt is preceded by
    the rules in hints/ and the cube's outline levels, and the answer is corrected against the outline before Essbase
    sees it (essbase_hints.py).

Settings a harness provides: AI_GATEWAY_MODE, AI_GATEWAY_CERTIFICATE and AI_GATEWAY_PRIVATE_KEY (the files
generate-certs.sh writes), and, for configure-essbase-ai.py, ESSBASE_URL, ESSBASE_USER, ESSBASE_PASSWORD,
AI_DATABASE_HOST, APP_USER, APP_USER_PASSWORD and AI_APPLICATIONS. start.sh runs all three and logs "Essbase AI
configured" when Essbase is ready to answer questions.
"""
import hashlib
import json
import math
import os
import re
import ssl
import sys
import urllib.error
import urllib.request
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer

import essbase_hints

PORT = int(os.environ.get("AI_GATEWAY_PORT", "443"))
CERTIFICATE = os.environ.get("AI_GATEWAY_CERTIFICATE", "/opt/ai-certs/server.crt")
PRIVATE_KEY = os.environ.get("AI_GATEWAY_PRIVATE_KEY", "/opt/ai-certs/server.key")

# Real models, on the Ollama server AI_MODEL_URL names. The embedding model must produce DIMENSIONS values.
MODEL_URL = (os.environ.get("AI_MODEL_URL") or "").rstrip("/")

# "canned" or "model" (see above). Without a setting, model when a model server is named.
MODE = (os.environ.get("AI_GATEWAY_MODE") or ("model" if MODEL_URL else "canned")).strip().lower()
if MODE not in ("canned", "model"):
    raise SystemExit("AI_GATEWAY_MODE must be canned or model, not %r" % MODE)
if MODE == "model" and not MODEL_URL:
    raise SystemExit("AI_GATEWAY_MODE model needs AI_MODEL_URL")

# The canned answers, read on every request so that a test can add one without a restart.
ANSWERS_FILE = os.environ.get("AI_GATEWAY_ANSWERS",
                              os.path.join(os.path.dirname(os.path.abspath(__file__)), "answers.json"))
CHAT_MODEL = os.environ.get("AI_CHAT_MODEL_NAME", "qwen2.5:7b")
EMBED_MODEL = os.environ.get("AI_EMBED_MODEL_NAME", "mxbai-embed-large")

# An OpenAI-compatible chat API to use instead of Ollama's, and its key.
CHAT_URL = (os.environ.get("AI_CHAT_URL") or "").rstrip("/")
CHAT_API_KEY = os.environ.get("AI_CHAT_API_KEY") or ""

# Rules to put ahead of Essbase's question prompt, as a system message: _all.md for every cube, and <App>.<Cube>.md for
# that cube alone. They settle choices Essbase's prompt leaves to the model, such as whether "by market" means Market's
# children.
HINTS_DIR = os.environ.get("AI_CHAT_HINTS_DIR", os.path.join(os.path.dirname(os.path.abspath(__file__)), "hints"))

# Whether to read each cube's outline, describe its levels to the model and correct the model's level choices (see
# essbase_hints.py). On unless AI_OUTLINE_HINTS is 0.
OUTLINE_HINTS = os.environ.get("AI_OUTLINE_HINTS", "1") != "0"

# Set AI_CHAT_SELF_CHECK to have the model check its answer to a question prompt once, against the question, before the
# answer goes back to Essbase. It helps small models that drop a member the question names, at the cost of a second call.
# "auto" (the default) checks with a local model only: an API model rarely needs it, and it doubles the calls.
SELF_CHECK_SETTING = (os.environ.get("AI_CHAT_SELF_CHECK") or "auto").strip().lower()
SELF_CHECK_PROMPT = (
    'Check your answer against the question "%s". Every scenario, measure and other member the question names must '
    'appear in the JSON (when it compares A and B, both A and B), each "by", "each" or "per" must have the '
    'hierarchy_scope the rules give it, and nothing the question does not ask for may be added. Then give the final '
    'answer again in exactly the same format: the JSON, then the "info:" line, with no other text.')

SELF_CHECK = SELF_CHECK_SETTING in ("1", "on", "true", "yes") or (SELF_CHECK_SETTING == "auto" and not CHAT_URL)

# The context window to run an Ollama chat model with, in tokens. Ollama's default (4,096) is too small: Essbase's question
# prompt is about 2,600 tokens, the hints add about 1,200 and the self-check repeats the answer, and Ollama silently drops
# the start of a prompt that does not fit - which is where the hints are.
CHAT_CONTEXT = int(os.environ.get("AI_CHAT_CONTEXT", "8192"))

# The reasoning effort to ask a reasoning model for (OpenAI's reasoning_effort), or empty for the model's default.
CHAT_REASONING_EFFORT = os.environ.get("AI_CHAT_REASONING_EFFORT") or ""

# mxbai-embed-large expects this prefix on search queries (not on the texts searched), as Cohere's models are told the
# input type instead.
QUERY_PREFIX = os.environ.get("AI_EMBED_QUERY_PREFIX", "Represent this sentence for searching relevant passages: ")

# The size of the vectors. It matches the cohere.embed-english-v3.0 model that the harness configures.
DIMENSIONS = 1024

# The reply to a chat request that is not one of the prompts above.
DEFAULT_REPLY = "OK"

def canned_answers():
    """Returns the canned answers, (key, answer JSON, info) for each entry of answers.json, where key is matched against
    the lower-cased question. Essbase asks for operators and a point of view (POV) - the members to query - as JSON,
    followed by an "info:" line."""
    try:
        with open(ANSWERS_FILE, encoding="utf-8") as source:
            entries = json.load(source).get("answers") or []
    except (OSError, ValueError) as error:
        print("Could not read %s: %r" % (ANSWERS_FILE, error), flush=True)
        return []
    return [((entry.get("question") or "").lower(), entry.get("answer") or {}, entry.get("info") or "")
            for entry in entries if entry.get("question")]


def embed(text):
    """Returns a unit-length vector with one signed count per hashed word."""
    vector = [0.0] * DIMENSIONS
    for word in re.findall(r"[a-z0-9]+", (text or "").lower()):
        digest = int(hashlib.md5(word.encode()).hexdigest(), 16)
        vector[digest % DIMENSIONS] += 1.0 if (digest >> 100) & 1 else -1.0
    length = math.sqrt(sum(value * value for value in vector)) or 1.0
    return [round(value / length, 6) for value in vector]


def prompt_text(body):
    """Returns the prompt of a chat request, whichever API format it was sent in."""
    chat = body.get("chatRequest") or {}
    if chat.get("message"):
        return chat["message"]
    return " ".join(part.get("text", "") for message in (chat.get("messages") or []) for part in message.get("content", []))


def reply_to(body):
    """Returns the model's reply to a chat request."""
    prompt = prompt_text(body)

    # The follow-up that refines the JSON with feedback from the conversation history: return the JSON unchanged.
    if "refine the given JSON" in prompt:
        match = re.search(r"Input JSON:\s*(\{.*\})\s*Conversation History:", prompt, re.S)
        if match:
            return match.group(1)

    # The question itself.
    questions = re.findall(r'For the NLQ: "(.*?)", identify all the operators mentioned', prompt, re.S)
    if questions:
        question = questions[-1].lower()
        for key, operators, info in canned_answers():
            if key in question:
                return json.dumps(operators) + "\ninfo: " + info
        return "info: This question is not related to Essbase or MDX."

    return DEFAULT_REPLY


def ollama(path, body):
    """Sends a request to the Ollama server and returns the parsed response."""
    request = urllib.request.Request(MODEL_URL + path, data=json.dumps(body).encode(),
                                     headers={"Content-Type": "application/json"})
    with urllib.request.urlopen(request, timeout=600) as response:
        return json.loads(response.read().decode())


def read_hint(name):
    """Returns the text of a hints file, or an empty string when there is none."""
    try:
        with open(os.path.join(HINTS_DIR, name), encoding="utf-8") as hint:
            return hint.read().strip()
    except OSError:
        return ""


def question_cube(chat):
    """Returns the outline hints for the cube an Essbase question prompt (the one that asks for operators) is about, or
    None for any other prompt or when the outline cannot be read."""
    prompt = prompt_text({"chatRequest": chat})
    if not OUTLINE_HINTS or "identify all the operators mentioned" not in prompt:
        return None
    cubes = re.findall(r'Essbase application "([^"]+)" with "([^"]+)" database', prompt)
    if not cubes:
        return None
    embed_texts = (lambda texts: ollama("/api/embed", {"model": EMBED_MODEL, "input": texts})["embeddings"]) \
        if MODEL_URL else None
    return essbase_hints.cube_hints(cubes[-1][0], cubes[-1][1], HINTS_DIR, embed_texts)


def hints_for(chat):
    """Returns the rules for an Essbase question prompt (the one that asks for operators): the rules for every cube, the
    levels read from the cube's outline, and the cube's own file. Returns an empty string for any other prompt."""
    prompt = prompt_text({"chatRequest": chat})
    if "identify all the operators mentioned" not in prompt:
        return ""
    cubes = re.findall(r'Essbase application "([^"]+)" with "([^"]+)" database', prompt)
    cube = question_cube(chat)
    parts = [read_hint("_all.md"), cube.rules() if cube else ""]
    if cubes:
        parts.append(read_hint("%s.%s.md" % cubes[-1]))
    return (chr(10) * 2).join(part for part in parts if part)


def chat_messages(chat):
    """Returns the messages of an OCI chat request (GENERIC or COHERE format) in Ollama's form, led by any hints."""
    roles = {"USER": "user", "ASSISTANT": "assistant", "CHATBOT": "assistant", "SYSTEM": "system"}
    hints = hints_for(chat)
    messages = [{"role": "system", "content": hints}] if hints else []
    if (chat.get("apiFormat") or "").upper() == "COHERE":
        if chat.get("preambleOverride"):
            messages.append({"role": "system", "content": chat["preambleOverride"]})
        for item in chat.get("chatHistory") or []:
            messages.append({"role": roles.get((item.get("role") or "").upper(), "user"), "content": item.get("message", "")})
        messages.append({"role": "user", "content": chat.get("message", "")})
        return messages
    for message in chat.get("messages") or []:
        content = message.get("content")
        if isinstance(content, list):
            content = "".join(part.get("text", "") for part in content if isinstance(part, dict))
        messages.append({"role": roles.get((message.get("role") or "").upper(), "user"), "content": content or ""})
    return messages


def openai_reply(chat):
    """Returns the reply of the OpenAI-compatible chat API to an OCI chat request.

    Reasoning models refuse some sampling settings, so a request that is refused for a setting is sent again without it.
    """
    request = {"model": CHAT_MODEL, "messages": chat_messages(chat)}
    for source, target in (("temperature", "temperature"), ("topP", "top_p"), ("maxTokens", "max_completion_tokens")):
        if chat.get(source) is not None:
            request[target] = chat[source]
    if CHAT_REASONING_EFFORT:
        request["reasoning_effort"] = CHAT_REASONING_EFFORT
    while True:
        http = urllib.request.Request(CHAT_URL + "/chat/completions", data=json.dumps(request).encode(),
                                      headers={"Content-Type": "application/json", "Authorization": "Bearer " + CHAT_API_KEY})
        try:
            with urllib.request.urlopen(http, timeout=600) as response:
                answer = json.loads(response.read().decode())
            break
        except urllib.error.HTTPError as error:
            detail = error.read().decode()
            refused = next((key for key in ("temperature", "top_p", "max_completion_tokens", "reasoning_effort")
                            if key in request and "'%s'" % key in detail), None)
            if error.code != 400 or refused is None:
                raise RuntimeError("HTTP %d from the chat API: %s" % (error.code, detail[:500]))
            print("The chat API refused %s; sending again without it" % refused, flush=True)
            del request[refused]
    return ((answer.get("choices") or [{}])[0].get("message") or {}).get("content") or ""


def model_reply(body):
    """Returns the chat model's reply to an OCI chat request, checked once against the question when SELF_CHECK is on."""
    chat = body.get("chatRequest") or {}
    reply = single_reply(chat)
    if "refine the given JSON" in prompt_text(body):
        return checked_refinement(reply, prompt_text(body))
    questions = re.findall(r'For the NLQ: "(.*?)", identify all the operators mentioned', prompt_text(body), re.S)
    if SELF_CHECK and questions and (chat.get("apiFormat") or "").upper() != "COHERE":
        checked = dict(chat, messages=list(chat.get("messages") or []) + [
            {"role": "ASSISTANT", "content": [{"type": "TEXT", "text": reply}]},
            {"role": "USER", "content": [{"type": "TEXT", "text": SELF_CHECK_PROMPT % questions[-1]}]}])
        second = single_reply(checked)
        if LOG_TRAFFIC:
            print(("SELF CHECK changed the reply to %s" % second) if second.strip() != reply.strip()
                  else "SELF CHECK kept the reply", flush=True)
        reply = second
    cube = question_cube(chat)
    return cube.correct(reply, questions[-1] if questions else "") if cube else reply


def checked_refinement(reply, prompt):
    """Returns the model's reply to Essbase's follow-up prompt, which asks it to refine the JSON it gave against the
    conversation so far, in a form Essbase can parse: the JSON written out again, or - when the reply holds no JSON that
    parses - the JSON Essbase sent in, unchanged. Essbase gives up on the whole question when this reply does not parse.
    """
    start = reply.find("{")
    try:
        answer, end = json.JSONDecoder().raw_decode(reply[start:]) if start >= 0 else (None, 0)
    except ValueError:
        answer = None
    sent = re.search(r"Input JSON:\s*(\{.*\})\s*Conversation History:", prompt, re.S)
    if isinstance(answer, dict):
        drop_invented_dimensions(answer, sent.group(1) if sent else "")
        rest = reply[start + end:]
        info = rest[rest.find("info:"):].strip() if "info:" in rest else ""
        return json.dumps(answer) + ("\n" + info if info else "")
    print("REFINE reply did not parse; %s" % ("returned the JSON Essbase sent" if sent else "passed it through"),
          flush=True)
    return sent.group(1) if sent else reply


def drop_invented_dimensions(answer, sent_json):
    """Removes from a refined answer's POV and sort any dimension that neither the JSON Essbase sent in nor any cube the
    gateway has read has (a small model asked to refine may add "Date: 2023" or "Page: Summary"; Essbase then gives up
    on the question)."""
    try:
        sent = json.loads(sent_json) if sent_json else {}
    except ValueError:
        sent = {}
    allowed = {key.lower() for part in ("POV", "sort") for key in (sent.get(part) or {})}
    allowed |= {name.lower() for name in essbase_hints.known_dimensions()}
    for part in ("POV", "sort"):
        members = answer.get(part)
        if isinstance(members, dict):
            for key in [key for key in members if key.lower() not in allowed]:
                print("REFINE dropped invented dimension %s: %s" % (key, json.dumps(members[key])), flush=True)
                del members[key]


def single_reply(chat):
    """Returns the chat model's reply to the messages of an OCI chat request."""
    if CHAT_URL:
        return openai_reply(chat)
    options = {"num_ctx": CHAT_CONTEXT}
    for source, target in (("temperature", "temperature"), ("topP", "top_p"), ("topK", "top_k"), ("maxTokens", "num_predict")):
        if chat.get(source) is not None:
            options[target] = chat[source]
    answer = ollama("/api/chat", {"model": CHAT_MODEL, "messages": chat_messages(chat), "stream": False, "options": options})
    return (answer.get("message") or {}).get("content", "")


def model_embeddings(body):
    """Returns the embedding model's vectors for an OCI embedText request."""
    texts = body.get("inputs") or []
    if (body.get("inputType") or "").upper() == "SEARCH_QUERY":
        texts = [QUERY_PREFIX + text for text in texts]
    return ollama("/api/embed", {"model": EMBED_MODEL, "input": texts}).get("embeddings", [])


LOGGED_EMBED_FIELDS = False

# Set AI_LOG_TRAFFIC to log what Essbase sends and what comes back, to see how it uses the models.
LOG_TRAFFIC = bool(os.environ.get("AI_LOG_TRAFFIC"))


class Handler(BaseHTTPRequestHandler):
    protocol_version = "HTTP/1.1"

    # A client that connects and then says nothing must not hold a thread for ever.
    timeout = 60

    def log_message(self, *args):
        pass

    def send_json(self, status, body):
        payload = json.dumps(body).encode()
        self.send_response(status)
        self.send_header("Content-Type", "application/json")
        self.send_header("Content-Length", str(len(payload)))
        self.end_headers()
        self.wfile.write(payload)

    def read_body(self):
        """Reads the request body, which the database may send with a Content-Length or in chunks."""
        if "chunked" in (self.headers.get("Transfer-Encoding") or "").lower():
            body = b""
            while True:
                size = int(self.rfile.readline().split(b";")[0].strip() or b"0", 16)
                if size == 0:
                    while self.rfile.readline().strip():  # the trailer, up to the blank line
                        pass
                    return body
                body += self.rfile.read(size)
                self.rfile.readline()  # the CRLF that ends the chunk
        length = int(self.headers.get("Content-Length") or 0)
        return self.rfile.read(length) if length else b""

    def do_POST(self):
        try:
            self.handle_request()
        except Exception as error:  # a request the gateway cannot make sense of must still get an answer
            print("ERROR %s: %r" % (self.path, error), flush=True)
            self.send_json(500, {"code": "InternalServerError", "message": "The AI gateway could not handle the request: %r" % (error,)})

    def handle_request(self):
        raw = self.read_body()
        try:
            body = json.loads(raw or b"{}")
        except ValueError:
            body = {}
        if not isinstance(body, dict):
            body = {}
        print("POST %s (%d bytes)" % (self.path, len(raw)), flush=True)

        model = ((body.get("servingMode") or {}).get("modelId")) or "ai-gateway"

        if self.path.endswith("/actions/chat"):
            text = model_reply(body) if MODE == "model" else reply_to(body)
            if LOG_TRAFFIC:
                print(("CHAT REQUEST %s" + chr(10) + "CHAT REPLY %s") % (json.dumps(body.get("chatRequest")), text), flush=True)
            if ((body.get("chatRequest") or {}).get("apiFormat") or "").upper() == "COHERE":
                return self.send_json(200, {"modelId": model, "modelVersion": "1.0", "chatResponse": {
                    "apiFormat": "COHERE", "text": text, "finishReason": "COMPLETE",
                    "chatHistory": [], "documents": [], "toolCalls": []}})
            return self.send_json(200, {"modelId": model, "modelVersion": "1.0", "chatResponse": {
                "apiFormat": "GENERIC", "timeCreated": "2026-01-01T00:00:00.000Z",
                "choices": [{"index": 0, "finishReason": "stop",
                             "message": {"role": "ASSISTANT", "content": [{"type": "TEXT", "text": text}]}}],
                "usage": {"promptTokens": 1, "completionTokens": 1, "totalTokens": 2}}})

        if self.path.endswith("/actions/embedText"):
            global LOGGED_EMBED_FIELDS
            if not LOGGED_EMBED_FIELDS:
                LOGGED_EMBED_FIELDS = True
                print("embedText fields: %s" % json.dumps({key: value for key, value in body.items() if key != "inputs"}),
                      flush=True)
            vectors = model_embeddings(body) if MODE == "model" else [embed(text) for text in (body.get("inputs") or [])]
            if LOG_TRAFFIC:
                inputs = body.get("inputs") or []
                print("EMBED %d inputs, e.g. %s" % (len(inputs), json.dumps(inputs[:3])), flush=True)
                with open("/tmp/embed-inputs.jsonl", "a") as log:
                    log.write(json.dumps(inputs) + chr(10))
            return self.send_json(200, {"id": "embeddings", "modelId": model, "modelVersion": "3.0", "inputs": None,
                                        "embeddings": vectors})

        self.send_json(404, {"code": "NotFound", "message": "The AI gateway has no handler for " + self.path})

    do_GET = do_POST


if __name__ == "__main__":
    context = ssl.SSLContext(ssl.PROTOCOL_TLS_SERVER)
    context.load_cert_chain(CERTIFICATE, PRIVATE_KEY)
    server = ThreadingHTTPServer(("0.0.0.0", PORT), Handler)

    # Leave the TLS handshake to the thread that serves each connection (it happens on the first read), so that a client
    # that connects and never completes it cannot stop the server accepting everyone else.
    server.socket = context.wrap_socket(server.socket, server_side=True, do_handshake_on_connect=False)
    print("AI gateway listening on %d, %s" % (PORT, "chat %s at %s, embeddings %s at %s"
                                               % (CHAT_MODEL, CHAT_URL or MODEL_URL, EMBED_MODEL, MODEL_URL)
                                               if MODE == "model" else "canned answers from " + ANSWERS_FILE),
          flush=True)
    sys.stdout.flush()
    server.serve_forever()
