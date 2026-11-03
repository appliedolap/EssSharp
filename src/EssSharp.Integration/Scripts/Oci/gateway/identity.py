"""The identity role of the OCI gateway: an Oracle Identity Cloud look-alike for the stack.

With IDENTITY_PROVIDER=IDCS ("essbase identity idcs"), the Essbase domain is created in the image's own Identity Cloud
mode, pointed at the gateway as https://<tenant>.<host>. WebLogic's Identity Cloud integrator then accepts
"Authorization: Bearer <access token>" on Essbase REST, which is how Dodeca and EssSharp use OAuth against Essbase, and
the OPSS identity store resolves users and groups here. Users, groups and OAuth clients come from users.json, next to
this file; the Essbase administrator (ESSBASE_USER) is always present. provision-essbase-users.py gives the users their
Essbase roles.

What is served (the subset WebLogic and Oracle's authorization clients use, learned from WebLogic's own calls):
  /.well-known/openid-configuration, /.well-known/idcs-configuration   discovery, including the SCIM and access sections
  /oauth2/v1/token          client_credentials, password, authorization_code, refresh_token, device_code
  /oauth2/v1/authorize      a sign-in page that redirects with a code (authorization code grant)
  /oauth2/v1/device, /oauth2/v1/device/verify   device code grant, approved on the verify page
  /oauth2/v1/userinfo, /oauth2/v1/introspect, /oauth2/v1/revoke, /oauth2/v1/userlogout
  /admin/v1/SigningCert/jwk the RS256 signing key (JWKS)
  /admin/v1/Users, /admin/v1/IDSUsers, /admin/v1/Groups, /admin/v1/IDSGroups, /admin/v1/Me   SCIM, with filters
  /sso/v1/sdk/assert        the integrator's user verification (Asserter schema)
  /sso/v1/sdk/authenticate  the integrator's password check for basic-auth users (PasswordAuthenticator schema)

Tokens are RS256 JWTs signed with signing.key (generate-certs.sh issues it); the signature is produced with the openssl
binary, so no package is needed. Every request is logged on one line. Standard library only.
"""
import base64
import hashlib
import json
import os
import secrets
import subprocess
import time
import urllib.parse
import uuid

CERT_DIR = os.environ.get("OCI_CERT_DIR", "/opt/oci-certs")
TENANT = os.environ.get("IDCS_TENANT", "idcs-essbase")
HOST = os.environ.get("IDCS_HOST", "identity.local")
PORT = int(os.environ.get("IDCS_PORT", "443"))
BASE = "https://%s.%s%s" % (TENANT, HOST, "" if PORT == 443 else ":%d" % PORT)
ISSUER = "https://identity.oraclecloud.com/"
TOKEN_TTL = int(os.environ.get("OCI_TOKEN_TTL", "3600"))
USERS_FILE = os.environ.get("OCI_USERS_FILE", os.path.join(os.path.dirname(os.path.abspath(__file__)), "users.json"))
SIGNING_KEY = os.path.join(CERT_DIR, "signing.key")
SIGNING_CRT = os.path.join(CERT_DIR, "signing.crt")
KID = "SIGNING_KEY"
SCIM_ERROR = "urn:ietf:params:scim:api:messages:2.0:Error"
PREFIXES = ("/.well-known/", "/oauth2/", "/admin/v1/", "/sso/v1/", "/identity")


def b64u(data):
    return base64.urlsafe_b64encode(data).rstrip(b"=").decode()


def b64u_decode(text):
    return base64.urlsafe_b64decode(text + "=" * (-len(text) % 4))


def guid(name):
    return hashlib.md5(name.encode()).hexdigest()


def now():
    return int(time.time())


def log(message):
    print(time.strftime("%H:%M:%S"), message, flush=True)


# ---- the directory ----------------------------------------------------------------------------------------------------
def load_directory():
    """Reads users.json; the Essbase administrator is added when the file does not name it, since Essbase looks every
    user up here, and the ESSBASE_USER client secret from the environment wins over the file's."""
    with open(USERS_FILE, encoding="utf-8") as source:
        directory = json.load(source)
    users = list(directory.get("users") or [])
    admin = os.environ.get("ESSBASE_USER") or "admin"
    if not any((user.get("userName") or "").lower() == admin.lower() for user in users):
        users.insert(0, {"userName": admin, "password": os.environ.get("ESSBASE_PASSWORD") or "", "givenName": "Essbase",
                         "familyName": "Administrator", "groups": ["essbase-admins"]})
    groups = {name: {"id": guid("group:" + name), "displayName": name} for name in directory.get("groups") or []}
    by_name = {}
    for user in users:
        user = dict(user)
        user["id"] = guid("user:" + user["userName"])
        user["displayName"] = user.get("displayName") or ("%s %s" % (user.get("givenName", ""), user.get("familyName", ""))).strip() or user["userName"]
        user.setdefault("groups", [])
        for name in user["groups"]:
            groups.setdefault(name, {"id": guid("group:" + name), "displayName": name})
        by_name[user["userName"].lower()] = user
    clients = dict(directory.get("clients") or {})
    for client_id, client in clients.items():
        client["id"] = guid("client:" + client_id)
        client["clientId"] = client_id
        client.setdefault("name", client_id)
    if os.environ.get("IDCS_CLIENT_SECRET") and os.environ.get("IDCS_CLIENT_ID", "essbase") in clients:
        clients[os.environ.get("IDCS_CLIENT_ID", "essbase")]["secret"] = os.environ["IDCS_CLIENT_SECRET"]
    return by_name, groups, clients


USERS, GROUPS, CLIENTS = load_directory()


def scim_user(user):
    doc = {"schemas": ["urn:ietf:params:scim:schemas:core:2.0:User",
                       "urn:ietf:params:scim:schemas:oracle:idcs:extension:user:User"],
           "id": user["id"], "ocid": "ocid1.user.oc1..%s" % user["id"], "externalId": user["id"],
           "userName": user["userName"], "displayName": user["displayName"],
           "name": {"givenName": user.get("givenName", ""), "familyName": user.get("familyName", ""),
                    "formatted": user["displayName"]},
           "emails": [{"value": user.get("email", ""), "type": "work", "primary": True, "verified": True}],
           "active": True, "userType": "User",
           "urn:ietf:params:scim:schemas:oracle:idcs:extension:user:User": {"isFederatedUser": False, "status": "active"},
           "groups": [{"value": GROUPS[name]["id"], "display": name, "type": "direct",
                       "$ref": BASE + "/admin/v1/Groups/" + GROUPS[name]["id"]} for name in user["groups"] if name in GROUPS],
           "meta": {"resourceType": "User", "created": "2026-01-01T00:00:00Z", "lastModified": "2026-01-01T00:00:00Z",
                    "location": BASE + "/admin/v1/Users/" + user["id"]}}
    return doc


def scim_group(group):
    members = [{"value": user["id"], "display": user["displayName"], "name": user["userName"], "type": "User",
                "$ref": BASE + "/admin/v1/Users/" + user["id"]}
               for user in USERS.values() if group["displayName"] in user["groups"]]
    return {"schemas": ["urn:ietf:params:scim:schemas:core:2.0:Group",
                        "urn:ietf:params:scim:schemas:oracle:idcs:extension:group:Group"],
            "id": group["id"], "ocid": "ocid1.group.oc1..%s" % group["id"], "externalId": group["id"],
            "displayName": group["displayName"], "members": members,
            "urn:ietf:params:scim:schemas:oracle:idcs:extension:group:Group": {"creationMechanism": "api"},
            "meta": {"resourceType": "Group", "created": "2026-01-01T00:00:00Z", "lastModified": "2026-01-01T00:00:00Z",
                     "location": BASE + "/admin/v1/Groups/" + group["id"]}}


def list_response(resources, start=1, count=None):
    page = resources[start - 1:] if count is None else resources[start - 1:start - 1 + count]
    return {"schemas": ["urn:ietf:params:scim:api:messages:2.0:ListResponse"], "totalResults": len(resources),
            "startIndex": start, "itemsPerPage": len(page), "Resources": page}


def values_at(doc, path):
    """Returns the values of a SCIM attribute path (userName, name.givenName, members.value) as strings."""
    found = [doc]
    for part in path.split("."):
        following = []
        for item in found:
            if isinstance(item, dict):
                for key, value in item.items():
                    if key.lower() == part.lower():
                        following.extend(value if isinstance(value, list) else [value])
        found = following
    return [str(value) for value in found if not isinstance(value, (dict, list))]


def matches(doc, expression):
    """A small SCIM filter: attr op "value" joined by and/or; ops eq, ne, co, sw, ew, pr. What does not parse matches."""
    expression = expression.strip()
    if not expression:
        return True
    for joiner, combine in ((" or ", any), (" and ", all)):
        parts = expression.split(joiner)
        if len(parts) > 1:
            return combine(matches(doc, part) for part in parts)
    tokens = expression.strip("()").split(None, 2)
    if len(tokens) == 2 and tokens[1].lower() == "pr":
        return bool(values_at(doc, tokens[0]))
    if len(tokens) < 3:
        return True
    attribute, operator, wanted = tokens[0], tokens[1].lower(), tokens[2].strip().strip('"').lower()
    actual = [value.lower() for value in values_at(doc, attribute)]
    if operator == "eq":
        return wanted in actual
    if operator == "ne":
        return wanted not in actual
    if operator == "co":
        return any(wanted in value for value in actual)
    if operator == "sw":
        return any(value.startswith(wanted) for value in actual)
    if operator == "ew":
        return any(value.endswith(wanted) for value in actual)
    return True


# ---- tokens ------------------------------------------------------------------------------------------------------------
def public_key():
    """Returns the signing key's JWKS fields (n, e, x5c, x5t, x5t#S256), read with openssl."""
    modulus = subprocess.run(["openssl", "rsa", "-in", SIGNING_KEY, "-noout", "-modulus"], capture_output=True,
                             text=True, check=True).stdout
    n = bytes.fromhex(modulus.strip().split("=", 1)[1])
    der = subprocess.run(["openssl", "x509", "-in", SIGNING_CRT, "-outform", "DER"], capture_output=True, check=True).stdout
    return (b64u(n), "AQAB", base64.b64encode(der).decode(), b64u(hashlib.sha1(der).digest()),
            b64u(hashlib.sha256(der).digest()))


N, E, X5C, X5T, X5T256 = public_key()
JWKS = {"keys": [{"kty": "RSA", "kid": KID, "use": "sig", "alg": "RS256", "n": N, "e": E, "x5c": [X5C], "x5t": X5T,
                  "x5t#S256": X5T256, "key_ops": ["verify"]}]}


def sign(claims):
    header = {"kid": KID, "x5t": X5T, "alg": "RS256", "typ": "JWT"}
    signing_input = (b64u(json.dumps(header, separators=(",", ":")).encode()) + "."
                     + b64u(json.dumps(claims, separators=(",", ":")).encode()))
    signature = subprocess.run(["openssl", "dgst", "-sha256", "-sign", SIGNING_KEY], input=signing_input.encode(),
                               capture_output=True, check=True).stdout
    return signing_input + "." + b64u(signature)


def claims_of(token):
    try:
        return json.loads(b64u_decode(token.split(".")[1]))
    except Exception:
        return None


def split_scope(scope):
    """Identity Cloud scopes carry the audience in front of the scope: https://host:443/essbase openid -> aud + scopes."""
    audiences, scopes = [], []
    for item in (scope or "").split():
        if item.startswith("http") and "/" in item[8:]:
            cut = item.index("/", 8)
            audiences.append(item[:cut])
            scopes.append(item[cut:])
        elif item.startswith("urn:opc:") and "urn:opc:resource" in item:
            cut = item.index("urn:opc:resource")
            audiences.append(item[:cut])
            scopes.append(item[cut:])
        else:
            scopes.append(item)
    return audiences, scopes


CODES, DEVICES, REFRESH_TOKENS = {}, {}, {}


def issue(client, user=None, scope="", grant="password", nonce=None):
    """Returns a token response for a user (or, without one, for the client itself) with Identity Cloud's claims."""
    audiences, scopes = split_scope(scope)
    issued = now()
    access = {"user_tz": "America/Chicago", "iss": ISSUER, "tok_type": "AT", "client_id": client["clientId"],
              "client_name": client["name"], "client_guid": client["id"], "client_tenantname": TENANT, "tenant": TENANT,
              "user.tenant.name": TENANT, "iat": issued, "exp": issued + TOKEN_TTL, "jti": uuid.uuid4().hex, "gtp": grant,
              "aud": list(dict.fromkeys(audiences + [BASE, "https://%s.%s:443" % (TENANT, HOST)])),
              "scope": " ".join(scopes), "sidle": 480, "tenant_iss": BASE, "ca_guid": guid("ca:" + TENANT), "primTenant": True}
    if user:
        access.update({"sub": user["userName"], "sub_type": "user", "sub_mappingattr": "userName", "user_id": user["id"],
                       "user_displayname": user["displayName"], "user_lang": "en", "user_locale": "en",
                       "user_isAdmin": "essbase-admins" in user["groups"], "user_tenantname": TENANT})
    else:
        access.update({"sub": client["clientId"], "sub_type": "client", "sub_mappingattr": "clientId"})
    response = {"access_token": sign(access), "token_type": "Bearer", "expires_in": TOKEN_TTL}
    if user and "openid" in scopes:
        identity = {"iss": ISSUER, "sub": user["userName"], "aud": client["clientId"], "iat": issued,
                    "exp": issued + TOKEN_TTL, "tok_type": "IT", "user_id": user["id"],
                    "user_displayname": user["displayName"], "user.tenant.name": TENANT, "upn": user["userName"],
                    "preferred_username": user["userName"], "email": user.get("email", ""),
                    "given_name": user.get("givenName", ""), "family_name": user.get("familyName", ""),
                    "name": user["displayName"], "groups": user["groups"], "jti": uuid.uuid4().hex, "sid": uuid.uuid4().hex}
        if nonce:
            identity["nonce"] = nonce
        response["id_token"] = sign(identity)
    if user and ("offline_access" in scopes or grant in ("authorization_code", "device_code")):
        refresh = secrets.token_urlsafe(32)
        REFRESH_TOKENS[refresh] = (client["clientId"], user["userName"].lower(), scope)
        response["refresh_token"] = refresh
    return response


def find_user(username, password=None):
    user = USERS.get((username or "").lower())
    if user and (password is None or (user.get("password") or None) == password):
        return user
    return None


def bearer_user(handler):
    """Returns (user, claims) for the request's bearer token; user is None for a client token or a bad token."""
    claims = claims_of((handler.headers.get("Authorization") or "")[7:])
    if not claims or claims.get("exp", 0) < now():
        return None, claims
    return (USERS.get(str(claims.get("sub", "")).lower()) if claims.get("sub_type") == "user" else None), claims


SIGN_IN_PAGE = """<html><head><title>Essbase stack identity</title></head>
<body style="font-family:sans-serif;text-align:center;margin-top:3em">
<h2>Essbase stack identity</h2><p>%s</p><form method="post">%s
<p><input name="username" placeholder="user name" autofocus> <input name="password" type="password" placeholder="password"></p>
<p><button type="submit">Sign in</button></p></form>
<p style="color:#666;font-size:small">Users come from users.json of the stack's OCI gateway.</p></body></html>"""


def hidden_fields(params, skip=()):
    return "".join('<input type="hidden" name="%s" value="%s">' % (key, value.replace('"', "&quot;"))
                   for key, value in params.items() if key not in skip)


# ---- responses -------------------------------------------------------------------------------------------------------------
def send_json(handler, status, body, headers=None, scim=False):
    data = json.dumps(body).encode()
    handler.send_response(status)
    handler.send_header("Content-Type", "application/scim+json; charset=utf-8" if scim else "application/json; charset=utf-8")
    handler.send_header("Content-Length", str(len(data)))
    handler.send_header("Cache-Control", "no-store")
    for key, value in (headers or {}).items():
        handler.send_header(key, value)
    handler.end_headers()
    handler.wfile.write(data)


def send_html(handler, status, html, headers=None):
    data = html.encode()
    handler.send_response(status)
    handler.send_header("Content-Type", "text/html; charset=utf-8")
    handler.send_header("Content-Length", str(len(data)))
    for key, value in (headers or {}).items():
        handler.send_header(key, value)
    handler.end_headers()
    handler.wfile.write(data)


def who(handler):
    header = handler.headers.get("Authorization") or ""
    if header.lower().startswith("basic "):
        try:
            return "basic:" + base64.b64decode(header[6:]).decode().split(":", 1)[0]
        except Exception:
            return "basic:?"
    if header.lower().startswith("bearer "):
        claims = claims_of(header[7:]) or {}
        return "bearer:%s/%s" % (claims.get("sub", "?"), claims.get("sub_type", "?"))
    return "-"


def client_of(handler, form):
    header = handler.headers.get("Authorization") or ""
    client_id, secret = form.get("client_id", [""])[0], form.get("client_secret", [""])[0]
    if header.lower().startswith("basic "):
        try:
            client_id, secret = base64.b64decode(header[6:]).decode().split(":", 1)
        except Exception:
            pass
    client = CLIENTS.get(client_id)
    if not client or (client.get("secret") and client["secret"] != secret):
        return None
    return client


# ---- the endpoints ----------------------------------------------------------------------------------------------------------
def discovery(handler, idcs):
    admin = BASE + "/admin/v1/"
    openid = {"issuer": ISSUER, "authorization_endpoint": BASE + "/oauth2/v1/authorize",
              "token_endpoint": BASE + "/oauth2/v1/token", "userinfo_endpoint": BASE + "/oauth2/v1/userinfo",
              "revocation_endpoint": BASE + "/oauth2/v1/revoke", "introspection_endpoint": BASE + "/oauth2/v1/introspect",
              "device_authorization_endpoint": BASE + "/oauth2/v1/device",
              "end_session_endpoint": BASE + "/oauth2/v1/userlogout", "jwks_uri": admin + "SigningCert/jwk",
              "scopes_supported": ["openid", "email", "profile", "offline_access", "urn:opc:idm:__myscopes__"],
              "response_types_supported": ["code", "token", "id_token", "code id_token"],
              "subject_types_supported": ["public"], "id_token_signing_alg_values_supported": ["RS256"],
              "grant_types_supported": ["authorization_code", "client_credentials", "password", "refresh_token",
                                        "urn:ietf:params:oauth:grant-type:device_code"],
              "token_endpoint_auth_methods_supported": ["client_secret_basic", "client_secret_post"],
              "claims_supported": ["sub", "iss", "aud", "exp", "iat", "user_id", "user_displayname", "user.tenant.name"]}
    if not idcs:
        return send_json(handler, 200, openid)
    # WebLogic's integrator reads scim_users_endpoint, asserter_endpoint and password_authenticator_endpoint, and drops
    # the whole document when one is missing; the OPSS identity store reads scim_users/groups/resource_types_endpoint.
    scim = {"scim_version": "2.0", "scim_users_endpoint": admin + "Users", "scim_groups_endpoint": admin + "Groups",
            "scim_resource_types_endpoint": admin + "ResourceTypes", "scim_apps_endpoint": admin + "Apps",
            "scim_app_roles_endpoint": admin + "AppRoles", "scim_me_endpoint": admin + "Me",
            "users_endpoint": admin + "Users", "groups_endpoint": admin + "Groups", "me_endpoint": admin + "Me",
            "schemas_endpoint": admin + "Schemas", "resource_types_endpoint": admin + "ResourceTypes",
            "search_endpoint": admin + ".search", "bulk_endpoint": admin + "Bulk",
            "service_provider_config_endpoint": admin + "ServiceProviderConfig", "signing_cert_endpoint": admin + "SigningCert/jwk"}
    return send_json(handler, 200, {
        "openid-configuration": openid,
        "tenant-settings": {"tenantName": TENANT, "jwks_uri": openid["jwks_uri"], "tenant_base_url": BASE},
        "scim-configuration": scim,
        "access-configuration": {"asserter_endpoint": BASE + "/sso/v1/sdk/assert",
                                 "password_authenticator_endpoint": BASE + "/sso/v1/sdk/authenticate",
                                 "user_category_endpoint": admin + "UserCategories"},
        "cloud-gate-configuration": {"logout_uri": BASE + "/oauth2/v1/userlogout"},
        "oauth2-configuration": {"signing_cert_url": admin + "SigningCert/jwk", "token_endpoint": openid["token_endpoint"],
                                 "authorization_endpoint": openid["authorization_endpoint"]}})


def token(handler, form):
    client = client_of(handler, form)
    if not client:
        return send_json(handler, 401, {"error": "invalid_client", "error_description": "Unknown client or bad secret"})
    grant = form.get("grant_type", [""])[0]
    scope = form.get("scope", [""])[0]
    # For tests: a lifetime of the caller's choosing (seconds, negative for a token that has already expired).
    global TOKEN_TTL
    default_ttl = TOKEN_TTL
    try:
        if form.get("expires_in", [""])[0].strip():
            TOKEN_TTL = int(form["expires_in"][0])
        if grant == "client_credentials":
            return send_json(handler, 200, issue(client, None, scope, grant))
        return token_for_user(handler, client, grant, scope, form)
    finally:
        TOKEN_TTL = default_ttl


def token_for_user(handler, client, grant, scope, form):
    """The grants that end in a user's token."""
    if grant == "password":
        user = find_user(form.get("username", [""])[0], form.get("password", [""])[0])
        if not user:
            return send_json(handler, 401, {"error": "invalid_grant", "error_description": "Bad user name or password"})
        return send_json(handler, 200, issue(client, user, scope, grant))
    if grant == "authorization_code":
        code = CODES.pop(form.get("code", [""])[0], None)
        if not code:
            return send_json(handler, 400, {"error": "invalid_grant", "error_description": "Unknown or used code"})
        return send_json(handler, 200, issue(client, USERS[code["user"]], code["scope"], grant, code.get("nonce")))
    if grant == "refresh_token":
        refresh = REFRESH_TOKENS.pop(form.get("refresh_token", [""])[0], None)
        if not refresh:
            return send_json(handler, 400, {"error": "invalid_grant", "error_description": "Unknown refresh token"})
        return send_json(handler, 200, issue(client, USERS[refresh[1]], refresh[2], grant))
    if grant == "urn:ietf:params:oauth:grant-type:device_code":
        device_code = form.get("device_code", [""])[0]
        device = DEVICES.get(device_code)
        if not device:
            return send_json(handler, 400, {"error": "invalid_grant"})
        if not device.get("user"):
            return send_json(handler, 400, {"error": "authorization_pending"})
        DEVICES.pop(device_code, None)
        return send_json(handler, 200, issue(client, USERS[device["user"]], device["scope"], "device_code"))
    return send_json(handler, 400, {"error": "unsupported_grant_type", "error_description": grant})


def authorize(handler, method, params):
    if method == "GET" or not params.get("username"):
        return send_html(handler, 200, SIGN_IN_PAGE % ("Sign in to continue to %s." % params.get("client_id", "the application"),
                                                        hidden_fields(params)))
    user = find_user(params.get("username"), params.get("password"))
    if not user:
        return send_html(handler, 200, SIGN_IN_PAGE % ("Bad user name or password.", hidden_fields(params, ("username", "password"))))
    code = secrets.token_urlsafe(24)
    CODES[code] = {"user": user["userName"].lower(), "scope": params.get("scope", ""), "nonce": params.get("nonce"),
                   "client": params.get("client_id")}
    redirect = params.get("redirect_uri", "")
    location = "%s%scode=%s" % (redirect, "&" if "?" in redirect else "?", code)
    if params.get("state"):
        location += "&state=" + urllib.parse.quote(params["state"])
    return send_html(handler, 302, "", {"Location": location})


def device(handler, form):
    client = client_of(handler, form)
    if not client:
        return send_json(handler, 401, {"error": "invalid_client"})
    device_code = secrets.token_urlsafe(24)
    user_code = "%s-%s" % (secrets.token_hex(2).upper(), secrets.token_hex(2).upper())
    DEVICES[device_code] = {"user_code": user_code, "scope": form.get("scope", [""])[0], "client": client["clientId"], "user": None}
    return send_json(handler, 200, {"device_code": device_code, "user_code": user_code,
                                    "verification_uri": BASE + "/oauth2/v1/device/verify",
                                    "verification_uri_complete": BASE + "/oauth2/v1/device/verify?user_code=" + user_code,
                                    "expires_in": 600, "interval": 5})


def device_verify(handler, method, params):
    hidden = hidden_fields({"user_code": params.get("user_code", "")})
    if method == "GET" or not params.get("username"):
        return send_html(handler, 200, SIGN_IN_PAGE % ("Approve device code %s." % params.get("user_code", "?"), hidden))
    user = find_user(params.get("username"), params.get("password"))
    pending = next((item for item in DEVICES.values() if item["user_code"] == params.get("user_code")), None)
    if not user or not pending:
        return send_html(handler, 200, SIGN_IN_PAGE % ("Bad user name, password or code.", hidden))
    pending["user"] = user["userName"].lower()
    return send_html(handler, 200, "<html><body style='font-family:sans-serif;text-align:center;margin-top:3em'>"
                                   "Device approved for %s. Close this window.</body></html>" % user["displayName"])


def scim(handler, method, path, query, raw):
    start = int(query.get("startIndex", ["1"])[0])
    count = int(query["count"][0]) if "count" in query else None
    expression = query.get("filter", [""])[0]
    if method == "POST" and path.endswith("/.search"):
        try:
            body = json.loads(raw.decode() or "{}")
        except Exception:
            body = {}
        expression, start, count = body.get("filter", ""), body.get("startIndex", 1), body.get("count")
        path = path[:-len("/.search")]
    # the OPSS identity store derives IDSUsers and IDSGroups from the SCIM users and groups endpoints
    path = path.replace("/admin/v1/IDSUsers", "/admin/v1/Users").replace("/admin/v1/IDSGroups", "/admin/v1/Groups")
    if path == "/admin/v1/Me":
        user, claims = bearer_user(handler)
        if user:
            return send_json(handler, 200, scim_user(user), scim=True)
        if claims and claims.get("sub_type") == "client":
            return send_json(handler, 200, {"schemas": ["urn:ietf:params:scim:schemas:oracle:idcs:App"], "id": claims.get("client_guid"),
                                            "displayName": claims.get("client_name"), "name": claims.get("client_id"), "active": True}, scim=True)
        return send_json(handler, 401, {"schemas": [SCIM_ERROR], "status": "401", "detail": "No token"}, scim=True)
    if path == "/admin/v1/Users":
        docs = [scim_user(user) for user in USERS.values()]
        return send_json(handler, 200, list_response([doc for doc in docs if matches(doc, expression)], start, count), scim=True)
    if path.startswith("/admin/v1/Users/"):
        wanted = path.rsplit("/", 1)[1]
        user = next((user for user in USERS.values() if user["id"] == wanted or user["userName"].lower() == wanted.lower()), None)
        if user:
            return send_json(handler, 200, scim_user(user), scim=True)
        return send_json(handler, 404, {"schemas": [SCIM_ERROR], "status": "404", "detail": "No such user"}, scim=True)
    if path == "/admin/v1/Groups":
        docs = [scim_group(group) for group in GROUPS.values()]
        return send_json(handler, 200, list_response([doc for doc in docs if matches(doc, expression)], start, count), scim=True)
    if path.startswith("/admin/v1/Groups/"):
        wanted = path.rsplit("/", 1)[1]
        group = next((group for group in GROUPS.values() if group["id"] == wanted or group["displayName"].lower() == wanted.lower()), None)
        if group:
            return send_json(handler, 200, scim_group(group), scim=True)
        return send_json(handler, 404, {"schemas": [SCIM_ERROR], "status": "404", "detail": "No such group"}, scim=True)
    if path in ("/admin/v1/Apps", "/admin/v1/AppRoles", "/admin/v1/Grants", "/admin/v1/Schemas", "/admin/v1/ResourceTypes"):
        return send_json(handler, 200, list_response([]), scim=True)
    log("  !! no handler for SCIM path %s" % path)
    return send_json(handler, 404, {"schemas": [SCIM_ERROR], "status": "404", "detail": "The OCI gateway has no handler for " + path}, scim=True)


def sdk(handler, path, raw):
    """The integrator verifies an asserted user (Asserter schema: mappingAttribute and mappingAttributeValue) and checks
    basic-auth passwords (PasswordAuthenticator schema). Both answer with the user's record and group memberships."""
    try:
        body = json.loads(raw.decode() or "{}")
    except Exception:
        body = {}
    if path.endswith("/authenticate"):
        user = find_user(body.get("userName") or body.get("mappingAttributeValue"), body.get("password"))
        if not user:
            return send_json(handler, 401, {"schemas": [SCIM_ERROR], "status": "401", "detail": "Bad user name or password"})
    else:
        attribute, value = body.get("mappingAttribute", "id"), str(body.get("mappingAttributeValue", "")).lower()
        user = next((user for user in USERS.values()
                     if str(user["id"] if attribute == "id" else user.get(attribute, "")).lower() == value), None)
        if not user:
            return send_json(handler, 404, {"schemas": [SCIM_ERROR], "status": "404", "detail": "No such user"})
    doc = scim_user(user)
    doc["schemas"] = [(body.get("schemas") or ["urn:ietf:params:scim:schemas:oracle:idcs:Asserter"])[0]] + doc["schemas"]
    doc.update({"mappingAttribute": body.get("mappingAttribute", "userName"), "mappingAttributeValue": user["id"],
                "includeMemberships": True, "appRoles": [], "userToken": None})
    return send_json(handler, 200, doc)


def status(handler):
    return send_json(handler, 200, {"tenant": TENANT, "issuer": ISSUER, "base": BASE, "users": sorted(USERS),
                                    "groups": sorted(GROUPS), "clients": sorted(CLIENTS), "token_ttl": TOKEN_TTL,
                                    "token_endpoint": "/oauth2/v1/token", "users_file": USERS_FILE})


def handles(path):
    return path.startswith(PREFIXES)


def handle(handler, method, raw):
    """Answers an identity request. Returns after sending the response; errors are answered as 500 and logged."""
    parsed = urllib.parse.urlparse(handler.path)
    path, query = parsed.path.rstrip("/") or "/", urllib.parse.parse_qs(parsed.query)
    content_type = (handler.headers.get("Content-Type") or "").lower()
    form = urllib.parse.parse_qs(raw.decode(errors="replace")) if "x-www-form-urlencoded" in content_type else {}
    preview = raw[:200].decode(errors="replace").replace("\n", " ")
    if "password=" in preview:
        preview = "(form with a password)"
    log("%s %s%s  who=%s%s" % (method, path, ("?" + parsed.query) if parsed.query and "password" not in parsed.query else "",
                               who(handler), ("  body=" + preview) if preview else ""))
    try:
        if path in ("/.well-known/openid-configuration", "/.well-known/idcs-configuration"):
            return discovery(handler, path.endswith("idcs-configuration"))
        if path == "/admin/v1/SigningCert/jwk":
            return send_json(handler, 200, JWKS)
        if path == "/oauth2/v1/token" and method == "POST":
            return token(handler, form)
        params = {key: values[0] for key, values in (form if method == "POST" else query).items()}
        if path == "/oauth2/v1/authorize":
            return authorize(handler, method, params)
        if path == "/oauth2/v1/device" and method == "POST":
            return device(handler, form)
        if path == "/oauth2/v1/device/verify":
            return device_verify(handler, method, params)
        if path == "/oauth2/v1/userinfo":
            user, _ = bearer_user(handler)
            if not user:
                return send_json(handler, 401, {"error": "invalid_token"}, {"WWW-Authenticate": "Bearer"})
            return send_json(handler, 200, {"sub": user["userName"], "name": user["displayName"], "given_name": user.get("givenName", ""),
                                            "family_name": user.get("familyName", ""), "email": user.get("email", ""),
                                            "email_verified": True, "preferred_username": user["userName"], "user_id": user["id"],
                                            "user_displayname": user["displayName"], "user.tenant.name": TENANT, "groups": user["groups"]})
        if path == "/oauth2/v1/introspect" and method == "POST":
            claims = claims_of(form.get("token", [""])[0])
            if not claims or claims.get("exp", 0) < now():
                return send_json(handler, 200, {"active": False})
            return send_json(handler, 200, dict(claims, active=True, username=claims.get("sub"), token_type="Bearer"))
        if path in ("/oauth2/v1/revoke", "/oauth2/v1/userlogout"):
            return send_json(handler, 200, {})
        if path.startswith("/admin/v1/"):
            return scim(handler, method, path, query, raw)
        if path in ("/sso/v1/sdk/assert", "/sso/v1/sdk/authenticate") and method == "POST":
            return sdk(handler, path, raw)
        if path == "/identity":
            return status(handler)
        log("  !! no handler for %s" % path)
        return send_json(handler, 404, {"error": "not_found", "path": path})
    except (BrokenPipeError, ConnectionResetError):
        return None
    except Exception as error:  # keep serving; the log tells the story
        log("  !! error %r on %s %s" % (error, method, path))
        try:
            return send_json(handler, 500, {"error": "server_error", "detail": repr(error)})
        except OSError:
            return None
