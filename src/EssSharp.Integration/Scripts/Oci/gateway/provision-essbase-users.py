#!/usr/bin/env python3
"""Gives the identity service's users their Essbase roles (the "essbase" entry of each user in users.json).

In Identity Cloud mode Essbase has no users of its own: it looks them up in the identity service and only keeps the
roles. This runs on every start of the gateway and again every PROVISION_INTERVAL seconds, so that a user added to
users.json, or an application rebuilt by the import watcher (which loses its permissions), is provisioned within a few
minutes. Every call is idempotent; only changes are logged.

Settings: ESSBASE_URL, ESSBASE_USER, ESSBASE_PASSWORD (the administrator), OCI_USERS_FILE, PROVISION_INTERVAL.
"""
import base64
import json
import os
import time
import urllib.error
import urllib.parse
import urllib.request

ESSBASE_URL = (os.environ.get("ESSBASE_URL") or "http://essbase:9000").rstrip("/") + "/essbase/rest/v1/"
USERS_FILE = os.environ.get("OCI_USERS_FILE", os.path.join(os.path.dirname(os.path.abspath(__file__)), "users.json"))
INTERVAL = int(os.environ.get("PROVISION_INTERVAL", "120"))
AUTHORIZATION = "Basic " + base64.b64encode(("%s:%s" % (os.environ.get("ESSBASE_USER") or "admin",
                                                        os.environ.get("ESSBASE_PASSWORD") or "")).encode()).decode()


def log(message):
    print(time.strftime("%H:%M:%S"), "provision:", message, flush=True)


def rest(method, path, body=None):
    request = urllib.request.Request(ESSBASE_URL + path, data=json.dumps(body).encode() if body is not None else None,
                                     method=method, headers={"Authorization": AUTHORIZATION, "Accept": "application/json",
                                                             "Content-Type": "application/json"})
    try:
        with urllib.request.urlopen(request, timeout=60) as response:
            text = response.read().decode()
            return response.status, (json.loads(text) if text.strip().startswith(("{", "[")) else text)
    except urllib.error.HTTPError as error:
        text = error.read().decode(errors="replace")
        return error.code, (json.loads(text) if text.strip().startswith("{") else text)
    except (urllib.error.URLError, OSError, ValueError) as error:
        return 0, repr(error)


def wanted_roles():
    with open(USERS_FILE, encoding="utf-8") as source:
        users = json.load(source).get("users") or []
    return [(user["userName"], user.get("essbase") or {}) for user in users if user.get("essbase")]


def current_role(user):
    status, body = rest("GET", "permissions/" + urllib.parse.quote(user))
    return body.get("role") if status == 200 and isinstance(body, dict) else None


def current_application_role(application, user):
    status, body = rest("GET", "applications/%s/permissions/%s" % (urllib.parse.quote(application), urllib.parse.quote(user)))
    return body.get("role") if status == 200 and isinstance(body, dict) else None


def provision(known_missing):
    """One pass over users.json. Returns the applications still missing, to log each once."""
    missing = set()
    for user, wanted in wanted_roles():
        role = wanted.get("role")
        if role and (current_role(user) or "").lower() != role.lower():
            status, body = rest("PUT", "permissions/" + urllib.parse.quote(user), {"id": user, "role": role, "group": False})
            log("%s: service role %s%s" % (user, role, "" if status in (200, 204) else " FAILED (HTTP %d: %s)" % (status, body)))
        for application, app_role in (wanted.get("applications") or {}).items():
            if rest("GET", "applications/" + urllib.parse.quote(application))[0] != 200:
                missing.add(application)
                if application not in known_missing:
                    log("%s: application %s does not exist yet; its role waits" % (user, application))
                continue
            if (current_application_role(application, user) or "").lower() == app_role.lower():
                continue
            status, body = rest("PUT", "applications/%s/permissions/%s" % (urllib.parse.quote(application), urllib.parse.quote(user)),
                                {"id": user, "role": app_role, "group": False})
            log("%s: %s on %s%s" % (user, app_role, application, "" if status in (200, 204) else " FAILED (HTTP %d: %s)" % (status, body)))
    return missing


def main():
    log("Essbase at %s, users from %s" % (ESSBASE_URL, USERS_FILE))
    while rest("GET", "about")[0] != 200:
        time.sleep(30)
    known_missing = set()
    while True:
        try:
            known_missing = provision(known_missing)
        except Exception as error:  # a broken pass is reported and tried again at the next interval
            log("pass failed: %r" % (error,))
        time.sleep(INTERVAL)


if __name__ == "__main__":
    main()
