#!/bin/sh
#
# Issues the certificate the gateway presents for the inference host name (once per stack), serves, and configures
# Essbase to use the AI database beside the gateway. The configuration runs on every start, because this
# service restarts with the database, and a restarted database needs Essbase's connections to it warmed up.

set -eu

sh /opt/ai-gateway/generate-certs.sh

python /opt/ai-gateway/gateway.py &
server=$!
trap 'kill -TERM "$server" 2>/dev/null' TERM INT

# A failed configuration is reported in the log; the service keeps serving.
python /opt/ai-gateway/configure-essbase-ai.py &

wait "$server"
