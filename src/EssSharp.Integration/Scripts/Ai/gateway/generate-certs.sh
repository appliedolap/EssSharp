#!/bin/sh
#
# Issues the certificate authority and the server certificate the AI gateway presents for the inference host name,
# into the stack's certificate volume. The database trusts ca.crt; the keys never leave the volume.
#
# The authority is issued once per volume, so the database keeps trusting it. The server certificate is issued again
# whenever it does not name the current host (OCI_REGION changed), from the same authority.

set -eu

CERT_DIR=/opt/ai-certs
WORK_DIR="$CERT_DIR/.issuing"
HOST="inference.generativeai.${OCI_REGION:-us-chicago-1}.oci.oraclecloud.com"

rm -rf "$WORK_DIR"
mkdir -p "$WORK_DIR"
cd "$WORK_DIR"

if [ ! -f "$CERT_DIR/ca.crt" ] || [ ! -f "$CERT_DIR/ca.key" ]; then
  openssl req -x509 -quiet -newkey rsa:2048 -nodes -sha256 -days 3650 -subj "/CN=Essbase local AI CA" \
    -addext "basicConstraints=critical,CA:TRUE" -addext "keyUsage=critical,keyCertSign,cRLSign" \
    -keyout ca.key -out ca.crt 2>/dev/null
  chmod 600 ca.key
  chmod 644 ca.crt
  rm -f "$CERT_DIR/ca.crt" "$CERT_DIR/server.crt" "$CERT_DIR/server.key"
  echo "Issued a certificate authority"
else
  cp "$CERT_DIR/ca.key" "$CERT_DIR/ca.crt" .
fi

if [ -f "$CERT_DIR/server.crt" ] && [ -f "$CERT_DIR/server.key" ] &&
   openssl x509 -in "$CERT_DIR/server.crt" -noout -checkhost "$HOST" | grep -q 'does match'; then
  echo "Using the existing certificate for $HOST"
else
  openssl req -quiet -newkey rsa:2048 -nodes -sha256 -subj "/CN=$HOST" -keyout server.key -out server.csr 2>/dev/null
  printf 'basicConstraints=critical,CA:FALSE\nkeyUsage=critical,digitalSignature,keyEncipherment\nextendedKeyUsage=serverAuth\nsubjectAltName=DNS:%s\n' \
    "$HOST" > server.ext
  openssl x509 -req -in server.csr -CA ca.crt -CAkey ca.key -CAcreateserial -sha256 -days 3650 -extfile server.ext \
    -out server.crt 2>/dev/null
  chmod 600 server.key
  chmod 644 server.crt
  mv server.key server.crt "$CERT_DIR/"
  echo "Issued a certificate for $HOST"
fi

# ca.crt is moved into place last, so the database, which waits for it, never sees a half-written set.
if [ ! -f "$CERT_DIR/ca.crt" ]; then
  mv ca.key "$CERT_DIR/"
  mv ca.crt "$CERT_DIR/"
fi

cd "$CERT_DIR"
rm -rf "$WORK_DIR"
