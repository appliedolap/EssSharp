#!/bin/bash
#
# Runs on every start of the Oracle AI Database, before the container reports as healthy: trusts the certificate
# authority of the AI gateway beside it, then prepares the database for the Essbase AI features. Every
# step checks before it acts.

set -euo pipefail

CA_CERT=/opt/ai-certs/ca.crt

# The AI gateway issues the certificate authority when it first starts. It may start well after the database (a test
# harness starts it once the database is running, and may have to pull its image first), so wait up to ten minutes.
for _ in $(seq 1 300); do
  [ -s "$CA_CERT" ] && break
  sleep 2
done
[ -s "$CA_CERT" ] || { echo "No certificate authority at $CA_CERT"; exit 1; }

# The vector functions trust certificates from the operating system's store, and the chat functions from the wallet
# that setup-ai-database.sh builds.
sudo -n cp "$CA_CERT" /etc/pki/ca-trust/source/anchors/essbase-local-ai-ca.crt
sudo -n update-ca-trust extract

CA_CERT="$CA_CERT" bash /opt/ai-scripts/setup-ai-database.sh
