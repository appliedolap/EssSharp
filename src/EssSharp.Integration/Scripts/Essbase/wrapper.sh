#!/usr/bin/env bash
#
# Starts an Essbase test container the way the scoop essbase stack's wrapper.sh does, and then runs the image's start
# command: the arguments, or createAndStartDomain.sh without any.
#
# Every container: Essbase's managed server gets ESSBASE_MANAGED_SERVER_HEAP (4g unless set) instead of the image's 1 GB,
# below which Essbase refuses drill-through reports when the heap is busy.
#
# Identity Cloud mode (IDENTITY_PROVIDER=IDCS), with the harness's OCI gateway as its identity provider: waits for the
# gateway's certificate authority, makes the image's own IDCS step trust it in the domain right after it runs
# (trustOciGateway.sh), and drops the LOGOUT_URL the step sets (it sends the web UI's login page to Oracle's Cloud Gate,
# which this environment has none of).
set -u

script_dir="$(dirname "$(readlink -f "$0")")"

# Essbase's managed server (essbase_server1) gets a 1 GB heap when the image creates the domain, and Essbase refuses to
# run a drill-through report while less than a tenth of the heap is free, which a 1 GB heap often is. The
# heap is raised to ESSBASE_MANAGED_SERVER_HEAP (4g unless it is set) in the domain's startup settings: at every start
# for a domain that exists, and for a new one right after it is created, before its servers first start.
essbase_heap="${ESSBASE_MANAGED_SERVER_HEAP:-4g}"
if ! [[ "$essbase_heap" =~ ^[0-9]+[kKmMgG]$ ]]; then
  echo "ESSBASE_MANAGED_SERVER_HEAP=$essbase_heap is not a heap size such as 4g; using 4g" >&2
  essbase_heap=4g
fi
cat > "$script_dir/setEssbaseHeap.sh" <<HEAP
#!/usr/bin/env bash
# Written by wrapper.sh: sets the heap of Essbase's managed server (the ESSBASE-MAN-SVR startup group) to ${essbase_heap}.
for f in /u01/config/domains/*/bin/setStartupEnv.sh; do
  [ -e "\$f" ] && sed -i -E '/STARTUP_GROUP\}" = "ESSBASE-MAN-SVR"/,/^fi/ s/-Xmx[0-9]+[kKmMgG]?/-Xmx${essbase_heap}/g' "\$f"
done
exit 0
HEAP
chmod +x "$script_dir/setEssbaseHeap.sh"
"$script_dir/setEssbaseHeap.sh"
if ! grep -q setEssbaseHeap.sh "$script_dir/createAndStartDomain.sh"; then
  sed -i '/esstools\/bin\/start\.sh </i "${SCRIPT_DIR}/setEssbaseHeap.sh"' "$script_dir/createAndStartDomain.sh"
fi

identity_provider="${IDENTITY_PROVIDER:-EMBEDDED}"
if [ "${identity_provider^^}" = "IDCS" ] && [ -e "$script_dir/trustOciGateway.sh" ]; then
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
