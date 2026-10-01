#!/usr/bin/env python3
"""A stand-in for the OCI Generative AI inference service, used by the EssSharp integration tests.

Essbase's AI features do not call Oracle's cloud themselves. They ask an Oracle database to do it, through
DBMS_CLOUD_AI (chat) and DBMS_VECTOR_CHAIN (embeddings), and the database calls

    https://inference.generativeai.<region>.oci.oraclecloud.com/20231130/actions/chat
    https://inference.generativeai.<region>.oci.oraclecloud.com/20231130/actions/embedText

The harness runs this server under that hostname (as a docker network alias) with a certificate from a throwaway test
CA that the database trusts, so the whole chain can run without an OCI tenancy. Requests are signed by the database with
the credential's key; this server does not check the signature.

The answers are deterministic:

  * embedText returns a hashed bag-of-words vector per input, so texts that share words are close together, which is all
    Essbase's member lookup needs.
  * chat answers the two prompts Essbase sends while generating MDX - the natural language question (answered from the
    ANSWERS table below) and the follow-up that refines the JSON (answered by echoing the JSON back) - and anything else
    with a short fixed reply.

To cover another natural language question in a test, add an entry to ANSWERS.
"""
import hashlib
import json
import math
import os
import re
import ssl
import sys
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer

PORT = int(os.environ.get("FAKE_OCI_PORT", "443"))
CERTIFICATE = os.environ.get("FAKE_OCI_CERTIFICATE", "/opt/fake-oci/server.crt")
PRIVATE_KEY = os.environ.get("FAKE_OCI_PRIVATE_KEY", "/opt/fake-oci/server.key")

# The size of the vectors. It matches the cohere.embed-english-v3.0 model that the harness configures.
DIMENSIONS = 1024

# The reply to a chat request that is not one of the prompts above.
DEFAULT_REPLY = "OK"

# Essbase asks the model to turn a question into operators and a point of view (POV) - the members to query - as JSON,
# followed by an "info:" line. Keys are matched against the lower-cased question.
ANSWERS = {
    "actual sales by year": (
        {
            "operator": "none",
            "operands": [],
            "sort": {},
            "POV": {
                "Scenario": [{"member_name": "Actual", "hierarchy_scope": ""}],
                "Measures": [{"member_name": "Sales", "hierarchy_scope": ""}],
                "Year": [{"member_name": "Year", "hierarchy_scope": "children"}],
            },
        },
        "Shows actual sales for each quarter of the year.",
    ),
}


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
        for key, (operators, info) in ANSWERS.items():
            if key in question:
                return json.dumps(operators) + "\ninfo: " + info
        return "info: This question is not related to Essbase or MDX."

    return DEFAULT_REPLY


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
        except Exception as error:  # a request the fake cannot make sense of must still get an answer
            print("ERROR %s: %r" % (self.path, error), flush=True)
            self.send_json(500, {"code": "InternalServerError", "message": "The fake OCI service could not handle the request: %r" % (error,)})

    def handle_request(self):
        raw = self.read_body()
        try:
            body = json.loads(raw or b"{}")
        except ValueError:
            body = {}
        if not isinstance(body, dict):
            body = {}
        print("POST %s (%d bytes)" % (self.path, len(raw)), flush=True)

        model = ((body.get("servingMode") or {}).get("modelId")) or "fake-model"

        if self.path.endswith("/actions/chat"):
            text = reply_to(body)
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
            return self.send_json(200, {"id": "fake-embeddings", "modelId": model, "modelVersion": "3.0", "inputs": None,
                                        "embeddings": [embed(text) for text in (body.get("inputs") or [])]})

        self.send_json(404, {"code": "NotFound", "message": "The fake OCI service has no handler for " + self.path})

    do_GET = do_POST


if __name__ == "__main__":
    context = ssl.SSLContext(ssl.PROTOCOL_TLS_SERVER)
    context.load_cert_chain(CERTIFICATE, PRIVATE_KEY)
    server = ThreadingHTTPServer(("0.0.0.0", PORT), Handler)

    # Leave the TLS handshake to the thread that serves each connection (it happens on the first read), so that a client
    # that connects and never completes it cannot stop the server accepting everyone else.
    server.socket = context.wrap_socket(server.socket, server_side=True, do_handshake_on_connect=False)
    print("fake OCI GenAI listening on %d" % PORT, flush=True)
    sys.stdout.flush()
    server.serve_forever()
