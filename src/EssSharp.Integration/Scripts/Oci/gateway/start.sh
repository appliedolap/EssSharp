#!/bin/sh
#
# Starts the OCI gateway: issues its certificates (once per stack), serves, configures Essbase AI whenever the stack's AI
# database is up, and, in Identity Cloud mode, keeps the identity users' Essbase roles applied.
#
# The gateway runs all the time (it is the stack's identity service), while the AI database comes and goes with
# "essbase ai on" and "essbase ai off". So Essbase AI is configured each time the database comes up: a configuration
# that fails while the database is still opening is tried again two minutes later, and a database that goes away and
# comes back gets Essbase's connections to it warmed up again.

set -eu

sh /opt/oci-gateway/generate-certs.sh

python /opt/oci-gateway/gateway.py &
server=$!
trap 'kill -TERM "$server" 2>/dev/null' TERM INT

database_up() {
  python -c 'import socket, sys; socket.create_connection((sys.argv[1], 1521), 5).close()' "${AI_DATABASE_HOST:-ai-database}" 2>/dev/null
}

(
  while true; do
    until database_up; do sleep 30; done
    echo "The AI database is up; configuring Essbase AI"
    until python /opt/oci-gateway/configure-essbase-ai.py; do
      echo "Essbase AI configuration failed; trying again in two minutes"
      sleep 120
      database_up || break
    done
    while database_up; do sleep 60; done
    echo "The AI database is down; Essbase AI is configured again when it comes back"
  done
) &

if [ "${IDENTITY_PROVIDER:-EMBEDDED}" = "IDCS" ]; then
  python /opt/oci-gateway/provision-essbase-users.py &
fi

wait "$server"
