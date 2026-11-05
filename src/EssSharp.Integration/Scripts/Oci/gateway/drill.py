"""A URL drill-through target for the stack, so that clients can be tested against URL drill-through definitions.

Drillbridge and FDMEE deploy URL drill-through definitions to Essbase (type application/x-hyperion-applicationbuilder-
report). A client that drills through one opens the definition's URL with its tokens filled in. This module answers those
URLs on the gateway's plain HTTP port (9005 on the host), records what arrived, and shows it on the page it answers with:

    GET or POST /drill/<anything>   records the request and answers with a page showing it
    GET         /drill/requests     the recorded requests, newest last, as JSON
    DELETE      /drill/requests     forgets them

The sso value, passwords and credential headers are recorded by their length only, as Drillbridge's url-listener.py does.

From the gateway's start, ensure_definitions() keeps the URL drill-through definitions below on Sample.Basic over Actual
(creating them, or updating their URL when it changed), checking every two minutes so that a rebuilt Sample.Basic gets
them back. The stack then offers one relational and two URL reports on the same cells:

    drillthrough_url_samplebasic   Drillbridge's own form: ?sso=$SSO_TOKEN$&$ATTR(ds,id,pos,gen,level.edge)$
    drillthrough_url_values        the $$<Dimension>-VALUE$$ form, one query parameter per dimension

DRILL_BASE_URL is the address the host reaches the gateway at (the 26.1 stack sets http://localhost:9005, or the port
IDENTITY_PORT names). Without it no definitions are made, since they could not point at the gateway.
"""

import base64
import collections
import html
import json
import os
import re
import threading
import time
import urllib.error
import urllib.parse
import urllib.request

PREFIX = "/drill"
REQUESTS_PATH = "/drill/requests"

# Empty when the host's address of the gateway is not known (a test harness that lets Docker assign the port): the
# definitions are then not made, and the drill target still answers.
BASE_URL = (os.environ.get("DRILL_BASE_URL") or "").rstrip("/")

ESSBASE_URL = (os.environ.get("ESSBASE_URL") or "http://essbase:9000").rstrip("/")
AUTHORIZATION = "Basic " + base64.b64encode(("%s:%s" % (os.environ.get("ESSBASE_USER", "admin"),
                                                        os.environ.get("ESSBASE_PASSWORD", "password1"))).encode()).decode()

APPLICATION = "Sample"
CUBE = "Basic"
REGIONS = ["Actual"]
DEFINITIONS = {
    "drillthrough_url_samplebasic": "/drill/samplebasic/v2?sso=$SSO_TOKEN$&$ATTR(ds,id,pos,gen,level.edge)$",
    "drillthrough_url_values": "/drill/samplebasic-values/v2?"
                               + "&".join("%s=$$%s-VALUE$$" % (dimension.lower(), dimension)
                                          for dimension in ("Year", "Measures", "Product", "Market", "Scenario")),
}

SECRET_FIELDS = {"sso", "password", "token"}
SECRET_HEADERS = {"authorization", "cookie", "proxy-authorization"}

# The requests a test may read back; the oldest drop off.
RECORDED = collections.deque(maxlen=200)
LOCK = threading.Lock()


def log(message):
    print("DRILL " + message, flush=True)


def handles(path):
    return path == PREFIX or path.startswith(PREFIX + "/") or path.startswith(PREFIX + "?")


def scrub(value):
    """Hides an sso token inside a value, such as a Referer that repeats the drill URL."""
    return re.sub(r"(sso=)[^&\s]+", lambda match: match.group(1) + "<redacted>", value)


def redact(pairs, secret):
    return [[key, "<redacted, %d chars>" % len(value) if key.lower() in secret else scrub(value)] for key, value in pairs]


def send(handler, status, content_type, data):
    handler.send_response(status)
    handler.send_header("Content-Type", content_type)
    handler.send_header("Content-Length", str(len(data)))
    handler.send_header("Cache-Control", "no-store")
    handler.end_headers()
    handler.wfile.write(data)


def handle(handler, method, raw):
    """Answers a request under /drill. Returns after sending the response."""
    parts = urllib.parse.urlsplit(handler.path)
    path = parts.path.rstrip("/") or "/"

    if path == REQUESTS_PATH:
        if method == "GET":
            with LOCK:
                recorded = list(RECORDED)
            return send(handler, 200, "application/json; charset=utf-8", json.dumps(recorded, indent=2).encode())
        if method == "DELETE":
            with LOCK:
                RECORDED.clear()
            log("forgot the recorded requests")
            return send(handler, 204, "text/plain", b"")
        return send(handler, 405, "text/plain; charset=utf-8", b"GET or DELETE only")

    content_type = handler.headers.get("Content-Type") or ""
    body = raw.decode("utf-8", "replace")
    form = urllib.parse.parse_qsl(body, keep_blank_values=True) if "form-urlencoded" in content_type.lower() else None
    entry = {
        "time": time.strftime("%Y-%m-%dT%H:%M:%S"),
        "client": handler.client_address[0],
        "method": method,
        "path": parts.path,
        "query": redact(urllib.parse.parse_qsl(parts.query, keep_blank_values=True), SECRET_FIELDS),
        "headers": redact(list(handler.headers.items()), SECRET_HEADERS),
        "form": redact(form, SECRET_FIELDS) if form is not None else None,
        "body": None if form is not None or not body else body[:4000],
    }
    with LOCK:
        RECORDED.append(entry)
    log("%s %s (%d query fields, %s form fields)" % (method, parts.path, len(entry["query"]),
                                                    len(form) if form is not None else "no"))
    return send(handler, 200, "text/html; charset=utf-8", page(entry).encode())


def page(entry):
    def table(title, pairs):
        if not pairs:
            return ""
        rows = "".join("<tr><td>%s</td><td>%s</td></tr>" % (html.escape(key), html.escape(value)) for key, value in pairs)
        return "<h2>%s</h2><table border=\"1\" cellpadding=\"4\">%s</table>" % (html.escape(title), rows)

    return ("<html><head><title>Drill-through received</title></head><body>"
            "<h1>Drill-through received</h1><p>%s %s</p>%s%s%s<p>All recorded requests: "
            "<a href=\"%s\">%s</a></p></body></html>") % (
        html.escape(entry["method"]), html.escape(entry["path"]),
        table("Query", entry["query"]), table("Form", entry["form"] or []),
        "<h2>Body</h2><pre>%s</pre>" % html.escape(entry["body"]) if entry["body"] else "",
        REQUESTS_PATH, REQUESTS_PATH)


def rest(method, path, body=None):
    """Sends a request to the Essbase REST API and returns the status and the parsed body (or the text)."""
    headers = {"Authorization": AUTHORIZATION, "Accept": "application/json"}
    if body is not None:
        headers["Content-Type"] = "application/json"
    request = urllib.request.Request(ESSBASE_URL + "/essbase/rest/v1/" + path, method=method,
                                     data=None if body is None else json.dumps(body).encode(), headers=headers)
    try:
        with urllib.request.urlopen(request, timeout=120) as response:
            text = response.read().decode()
            status = response.status
    except urllib.error.HTTPError as error:
        text = error.read().decode(errors="replace")
        status = error.code
    except (urllib.error.URLError, OSError) as error:
        return 0, str(error)
    try:
        return status, json.loads(text) if text else None
    except ValueError:
        return status, text


def ensure_definitions(interval=120):
    """Keeps the URL drill-through definitions on Sample.Basic: every interval seconds, creates a missing one and updates
    a URL that changed, so a Sample.Basic that is rebuilt (by the import watcher, or by hand) gets them back. While
    Sample.Basic does not exist (it is being rebuilt, or its user removed it) nothing is made, and nothing is logged."""
    if not BASE_URL:
        log("DRILL_BASE_URL is not set; no URL drill-through definitions were made")
        return
    cube = "applications/%s/databases/%s" % (APPLICATION, CUBE)
    in_place = set()  # the definitions already reported as in place, so a steady stack logs nothing
    while True:
        if rest("GET", cube)[0] == 200:
            for name, target in DEFINITIONS.items():
                ensure_definition(cube, name, BASE_URL + target, in_place)
        else:
            in_place.clear()
        time.sleep(interval)


def ensure_definition(cube, name, url, in_place):
    definition = {"name": name, "type": "URL", "url": url, "drillableRegions": REGIONS}
    status, existing = rest("GET", "%s/reports/%s" % (cube, name))
    if status == 200 and isinstance(existing, dict):
        if existing.get("url") == url:
            if name not in in_place:
                log("%s is in place" % name)
                in_place.add(name)
            return
        status, answer = rest("PUT", "%s/reports/%s" % (cube, name), definition)
        verb = "updated"
    else:
        status, answer = rest("POST", "%s/reports" % cube, definition)
        verb = "created"
    if 200 <= status < 300:
        log("%s %s: %s" % (verb, name, url))
        in_place.add(name)
    else:
        log("could not make %s: HTTP %d %s" % (name, status, answer))
        in_place.discard(name)


def start():
    threading.Thread(target=ensure_definitions, daemon=True).start()
