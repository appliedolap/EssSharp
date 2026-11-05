#!/usr/bin/env bash
#
# Starts an Essbase test container the way the scoop essbase stack's wrapper.sh does, and then runs the image's start
# command: the arguments, or createAndStartDomain.sh without any.
#
# Every container: Essbase's managed server gets ESSBASE_MANAGED_SERVER_HEAP (4g unless set) instead of the image's 1 GB,
# below which Essbase refuses drill-through reports when the heap is busy. Its pool of connections to the embedded LDAP
# holds ESSBASE_IDENTITY_POOL_SIZE (100 unless set) instead of 10, which deleting a few users in a row used up.
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

# Essbase's managed server reaches the users and groups of the domain's embedded LDAP through a pool of ten connections
# (the maxPoolSize of the DefaultAuthenticator adapter in the domain's fmwconfig/ovd/default/adapters.os_xml), and each
# user it deletes holds one of them until its three-minute read timeout ends. Nine deletes within three minutes leave
# the pool empty, and until connections come back every request fails: a 400 "Error while getting group" or a 403, for
# the administrator too. The pool is raised to ESSBASE_IDENTITY_POOL_SIZE (100 unless it is set), at the same points as
# the heap, so that deleting users in bulk cannot shut the server out. The servers write the domain's adapters.os_xml
# afresh from libOVD's template for that kind of adapter (WLS_OVD in the Oracle home) each time they start, so the
# template is set, and the domain's file too.
identity_pool="${ESSBASE_IDENTITY_POOL_SIZE:-100}"
if ! [[ "$identity_pool" =~ ^[1-9][0-9]*$ ]]; then
  echo "ESSBASE_IDENTITY_POOL_SIZE=$identity_pool is not a number of connections such as 100; using 100" >&2
  identity_pool=100
fi
cat > "$script_dir/setIdentityPool.sh" <<POOL
#!/usr/bin/env bash
# Written by wrapper.sh: sets the most connections Essbase's managed server keeps to the embedded LDAP to ${identity_pool}.
for f in /u01/oracle/oracle_common/modules/oracle.ovd/templates/adapter_template.xml; do
  [ -w "\$f" ] && sed -i -E '/<ldap id="WLS_OVD"/,/<\/ldap>/ s#<maxPoolSize>[0-9]+</maxPoolSize>#<maxPoolSize>${identity_pool}</maxPoolSize>#' "\$f"
done
for f in /u01/config/domains/*/config/fmwconfig/ovd/default/adapters.os_xml; do
  [ -e "\$f" ] && sed -i -E 's#<maxPoolSize>[0-9]+</maxPoolSize>#<maxPoolSize>${identity_pool}</maxPoolSize>#g' "\$f"
done
exit 0
POOL
chmod +x "$script_dir/setIdentityPool.sh"
"$script_dir/setIdentityPool.sh"
if ! grep -q setIdentityPool.sh "$script_dir/createAndStartDomain.sh"; then
  sed -i '/esstools\/bin\/start\.sh </i "${SCRIPT_DIR}/setIdentityPool.sh"' "$script_dir/createAndStartDomain.sh"
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
