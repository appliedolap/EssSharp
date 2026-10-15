#!/bin/bash
#
# Prepares an Oracle AI Database Free container for the Essbase AI features (AI Query and friends).
#
# Essbase does not talk to Oracle's cloud itself; it asks this database to, through DBMS_CLOUD_AI and DBMS_VECTOR_CHAIN.
# On Autonomous Database the pieces below are already in place, which is why Oracle's Essbase prerequisites do not list
# them. On a customer-managed database they are not:
#
#   * the full image flavor is required (the slim flavor has no Oracle Text, so DBMS_VECTOR_CHAIN does not exist)
#   * DBMS_CLOUD, installed with Oracle's catcon.pl
#   * an SSL wallet trusting the AI gateway's certificate authority, named by the SSL_WALLET database property (set in the root container)
#   * network access for the DBMS_CLOUD schema owner, and for the Essbase database user
#   * the privileges Essbase's credential and profile calls need
#   * MAX_STRING_SIZE=EXTENDED, because Essbase creates VARCHAR2(32767) columns
#
# Every step checks before it acts, so the script can be run again against a container it has already prepared.
# Expects ORACLE_PASSWORD and APP_USER in the environment (the container's own settings) and the certificate authority
# certificate at CA_CERT (by default, next to this script).

exec 2>&1
set -euo pipefail

: "${ORACLE_PASSWORD:?ORACLE_PASSWORD must be set}"
: "${APP_USER:?APP_USER must be set}"

APP_USER=$(echo "$APP_USER" | tr '[:lower:]' '[:upper:]')
SCRIPT_DIR=$(cd "$(dirname "$0")" && pwd)
WALLET_DIR=/opt/oracle/wallets/ssl
CA_CERT="${CA_CERT:-$SCRIPT_DIR/ca.crt}"

sysdba() { sqlplus -s -L / as sysdba; }

echo "==> SSL wallet trusting the AI gateway's certificate authority"
if [ ! -f "$WALLET_DIR/cwallet.sso" ]; then
  mkdir -p "$WALLET_DIR"
  "$ORACLE_HOME/bin/orapki" wallet create -wallet "$WALLET_DIR" -auto_login_only
fi
# Adding a certificate the wallet already holds is an error, and a harmless one on a re-run, so do not stop on it - but
# do check that the wallet really does trust the CA afterwards.
"$ORACLE_HOME/bin/orapki" wallet add -wallet "$WALLET_DIR" -trusted_cert -cert "$CA_CERT" -auto_login_only || true
ca_subject=$("$ORACLE_HOME/bin/orapki" cert display -cert "$CA_CERT" -summary | sed -n 's/^Subject:[[:space:]]*//p')
wallet_contents=$("$ORACLE_HOME/bin/orapki" wallet display -wallet "$WALLET_DIR")
[ -n "$ca_subject" ] && grep -qF "$ca_subject" <<<"$wallet_contents" || { echo "The SSL wallet does not trust the AI gateway's certificate authority"; exit 1; }

echo "==> DBMS_CLOUD"
cloud_ready() {
  sysdba <<'SQL' | tr -d '[:space:]'
set heading off feedback off pagesize 0
alter session set container = FREEPDB1;
select count(*) from dba_objects
 where owner = 'C##CLOUD$SERVICE' and object_name = 'DBMS_CLOUD_INTERNAL' and object_type = 'PACKAGE BODY' and status = 'VALID';
SQL
}

if [ "$(cloud_ready)" != "1" ]; then
  for script in catclouduser.sql dbms_cloud_install.sql; do
    "$ORACLE_HOME/perl/bin/perl" "$ORACLE_HOME/rdbms/admin/catcon.pl" -u "sys/$ORACLE_PASSWORD" \
      -force_pdb_mode 'READ WRITE' -b dbms_cloud_install -d "$ORACLE_HOME/rdbms/admin/" -l /tmp "$script"
  done
fi
[ "$(cloud_ready)" = "1" ] || { echo "DBMS_CLOUD did not install cleanly; see /tmp/dbms_cloud_install*.log"; exit 1; }

echo "==> Wallet property, network access and privileges"
{
  echo "define app_user = $APP_USER"
  cat <<'SQL'
whenever sqlerror exit failure rollback
set feedback off
alter database property set ssl_wallet = '/opt/oracle/wallets/ssl';
begin
  dbms_network_acl_admin.append_host_ace(host => '*', lower_port => 443, upper_port => 443,
    ace => xs$ace_type(privilege_list => xs$name_list('http', 'http_proxy'),
                       principal_name => 'C##CLOUD$SERVICE', principal_type => xs_acl.ptype_db));
end;
/
alter session set container = FREEPDB1;
begin
  dbms_network_acl_admin.append_host_ace(host => '*', lower_port => 443, upper_port => 443,
    ace => xs$ace_type(privilege_list => xs$name_list('http', 'http_proxy'),
                       principal_name => 'C##CLOUD$SERVICE', principal_type => xs_acl.ptype_db));
  dbms_network_acl_admin.append_host_ace(host => '*', lower_port => 443, upper_port => 443,
    ace => xs$ace_type(privilege_list => xs$name_list('http'),
                       principal_name => '&app_user', principal_type => xs_acl.ptype_db));
end;
/
grant create credential to &app_user;
grant execute on dbms_cloud to &app_user;
grant execute on dbms_vector to &app_user;
grant execute on dbms_cloud_ai to &app_user;
grant execute on dbms_cloud_ai_agent to &app_user;
grant execute on dbms_cloud_pipeline to &app_user;
SQL
} | sysdba

echo "==> MAX_STRING_SIZE"
string_size() {
  sysdba <<'SQL' | tr -d '[:space:]'
set heading off feedback off pagesize 0
alter session set container = FREEPDB1;
select value from v$parameter where name = 'max_string_size';
SQL
}

# The parameter reads EXTENDED as soon as it has been set, before the conversion has run and the PDB has been opened
# normally again, so a run that stopped half way must also be recognised by the PDB not being open for read and write.
pdb_mode() {
  sysdba <<'SQL' | tr -d '[:space:]'
set heading off feedback off pagesize 0
select open_mode from v$pdbs where name = 'FREEPDB1';
SQL
}

if [ "$(string_size)" != "EXTENDED" ] || [ "$(pdb_mode)" != "READWRITE" ]; then
  sysdba <<'SQL'
whenever sqlerror exit failure rollback
alter pluggable database FREEPDB1 close immediate;
alter pluggable database FREEPDB1 open upgrade;
alter session set container = FREEPDB1;
alter system set max_string_size = extended;
@?/rdbms/admin/utl32k.sql
alter session set container = CDB$ROOT;
alter pluggable database FREEPDB1 close immediate;
alter pluggable database FREEPDB1 open;
SQL
fi
[ "$(string_size)" = "EXTENDED" ] && [ "$(pdb_mode)" = "READWRITE" ] || { echo "MAX_STRING_SIZE is not EXTENDED, or FREEPDB1 is not open for read and write"; exit 1; }

echo "AI database ready"
