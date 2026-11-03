#!/usr/bin/env bash
#
# Starts an Essbase test container in Identity Cloud mode (IDENTITY_PROVIDER=IDCS), with the harness's OCI gateway as
# its identity provider, the way the scoop essbase stack does: waits for the gateway's certificate authority, makes
# the image's own IDCS step trust it in the domain right after it runs (trustOciGateway.sh), drops the LOGOUT_URL the
# step sets (it sends the web UI's login page to Oracle's Cloud Gate, which this environment has none of), and then
# runs the image's start command: the arguments, or createAndStartDomain.sh without any.
set -u

script_dir="$(dirname "$(readlink -f "$0")")"

if [ "${IDENTITY_PROVIDER^^}" = "IDCS" ] && [ -e "$script_dir/trustOciGateway.sh" ]; then
  for _ in $(seq 1 150); do [ -s /opt/oci-certs/ca.crt ] && break; sleep 2; done
  sed -i "/dictionary\['LOGOUT_URL'\] = logoutUrl/d" "$script_dir/wlst/enable_idcs_mode.py"
  if ! grep -q trustOciGateway.sh "$script_dir/_createDomain.sh"; then
    awk '{ print } /enable_idcs_mode.py/ { pending = 1 } pending && /^EOF$/ { print "  \"${SCRIPT_DIR}/trustOciGateway.sh\""; pending = 0 }' \
      "$script_dir/_createDomain.sh" > "$script_dir/_createDomain.sh.oci" && cat "$script_dir/_createDomain.sh.oci" > "$script_dir/_createDomain.sh" && rm -f "$script_dir/_createDomain.sh.oci"
  fi
  "$script_dir/trustOciGateway.sh"
fi

if [ $# -gt 0 ]; then
  exec "$@"
fi
exec "$script_dir/createAndStartDomain.sh"
