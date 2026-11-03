#!/usr/bin/env bash
#
# Trusts the OCI gateway's certificate authority in the Essbase domain, for Identity Cloud mode.
#
# WebLogic's Identity Cloud integrator and the OPSS identity store reach the gateway over HTTPS through WebLogic's own
# SSL stack, which trusts the file-based KSS store in the domain's keystores.xml (and the JDK's cacerts, which is
# read-only here). Nothing else counts: not the JVM trust store properties, not the IDCS certificate template, not
# imports into the DB-backed KSS stores. So the authority is written into that file, once, as a trusted certificate.
#
# Called by wrapper.sh right after the image's own IDCS step at domain creation (before the servers first start) and
# at every later start; it does nothing when there is no domain yet or the authority is already trusted.

set -u

CA=/opt/oci-certs/ca.crt
KEYSTORES="${DOMAIN_HOME:-/u01/config/domains/infra_domain}/config/fmwconfig/keystores.xml"
ALIAS=oci-gateway-ca

[ -s "$CA" ] || { echo "trustOciGateway: no certificate authority at $CA"; exit 0; }
[ -f "$KEYSTORES" ] || { echo "trustOciGateway: no domain yet"; exit 0; }
grep -q "<fks:name>$ALIAS</fks:name>" "$KEYSTORES" && exit 0

CERTIFICATE=$(grep -v CERTIFICATE "$CA" | tr -d '\n\r')
[ -n "$CERTIFICATE" ] || { echo "trustOciGateway: $CA is empty" >&2; exit 1; }

awk -v alias="$ALIAS" -v certificate="$CERTIFICATE" '
  index($0, "name=\"trust\"") { in_trust = 1 }
  in_trust && !done && index($0, "</fks:TrustedCertificates>") {
    print "                        <fks:trustedCertificateHashMap>"
    print "                           <fks:name>" alias "</fks:name>"
    print "                           <fks:value alias=\"" alias "\">"
    print "                              <fks:TrustedCertificate>" certificate "</fks:TrustedCertificate>"
    print "                           </fks:value>"
    print "                        </fks:trustedCertificateHashMap>"
    done = 1
  }
  { print }' "$KEYSTORES" > "$KEYSTORES.oci" && cat "$KEYSTORES.oci" > "$KEYSTORES" && rm -f "$KEYSTORES.oci"

if grep -q "<fks:name>$ALIAS</fks:name>" "$KEYSTORES"; then
  echo "trustOciGateway: the OCI gateway's certificate authority is trusted by the domain"
else
  echo "trustOciGateway: could not add the certificate authority to $KEYSTORES" >&2
  exit 1
fi
