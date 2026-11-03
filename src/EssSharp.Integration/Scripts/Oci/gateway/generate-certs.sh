#!/bin/sh
#
# Issues, into the stack's certificate volume, the certificate authority, the server certificate the OCI gateway presents
# for its two Oracle Cloud host names (the GenAI inference host and the identity tenant), and the key that signs the
# identity service's tokens. The AI database and Essbase's WebLogic trust ca.crt; the keys never leave the volume.
#
# The authority and the signing key are issued once per volume, so the trust and the tokens' key stay stable. The server
# certificate is issued again whenever it does not name the current hosts (OCI_REGION or the tenant changed).

set -eu

CERT_DIR=/opt/oci-certs
WORK_DIR="$CERT_DIR/.issuing"
INFERENCE="inference.generativeai.${OCI_REGION:-us-chicago-1}.oci.oraclecloud.com"
IDENTITY="${IDCS_TENANT:-idcs-essbase}.${IDCS_HOST:-identity.local}"

rm -rf "$WORK_DIR"
mkdir -p "$WORK_DIR"
cd "$WORK_DIR"

if [ ! -f "$CERT_DIR/ca.crt" ] || [ ! -f "$CERT_DIR/ca.key" ]; then
  openssl req -x509 -quiet -newkey rsa:2048 -nodes -sha256 -days 3650 -subj "/CN=Essbase local OCI CA" \
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
   openssl x509 -in "$CERT_DIR/server.crt" -noout -checkhost "$INFERENCE" | grep -q 'does match' &&
   openssl x509 -in "$CERT_DIR/server.crt" -noout -checkhost "$IDENTITY" | grep -q 'does match'; then
  echo "Using the existing certificate for $INFERENCE and $IDENTITY"
else
  openssl req -quiet -newkey rsa:2048 -nodes -sha256 -subj "/CN=$IDENTITY" -keyout server.key -out server.csr 2>/dev/null
  printf 'basicConstraints=critical,CA:FALSE\nkeyUsage=critical,digitalSignature,keyEncipherment\nextendedKeyUsage=serverAuth\nsubjectAltName=DNS:%s,DNS:%s,DNS:localhost\n' \
    "$INFERENCE" "$IDENTITY" > server.ext
  openssl x509 -req -in server.csr -CA ca.crt -CAkey ca.key -CAcreateserial -sha256 -days 3650 -extfile server.ext \
    -out server.crt 2>/dev/null
  chmod 600 server.key
  chmod 644 server.crt
  mv server.key server.crt "$CERT_DIR/"
  echo "Issued a certificate for $INFERENCE and $IDENTITY"
fi

if [ ! -f "$CERT_DIR/signing.key" ] || [ ! -f "$CERT_DIR/signing.crt" ]; then
  openssl req -x509 -quiet -newkey rsa:2048 -nodes -sha256 -days 3650 -subj "/CN=$IDENTITY token signing" \
    -keyout "$CERT_DIR/signing.key" -out "$CERT_DIR/signing.crt" 2>/dev/null
  chmod 600 "$CERT_DIR/signing.key"
  chmod 644 "$CERT_DIR/signing.crt"
  echo "Issued a token signing key"
fi

# ca.crt is moved into place last, so the database and Essbase, which wait for it, never see a half-written set.
if [ ! -f "$CERT_DIR/ca.crt" ]; then
  mv ca.key "$CERT_DIR/"
  mv ca.crt "$CERT_DIR/"
fi

cd "$CERT_DIR"
rm -rf "$WORK_DIR"
