using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace EssSharp.Integration.Setup
{
    /// <summary>
    /// A throwaway certificate authority that issues the server certificate for the fake OCI Generative AI service.
    /// </summary>
    /// <remarks>
    /// The database only calls an HTTPS endpoint whose certificate chains to a certificate it trusts. The harness makes
    /// the database trust this authority's certificate, and gives the fake service a certificate for the real OCI
    /// hostname issued by it. Nothing here is persisted: every run creates a new authority and discards it afterwards.
    /// The certificates last for years, because the containers that hold them are kept for as long as an Essbase
    /// container that outlives a run, and a certificate that lapsed in a container nobody rebuilt would break the tests
    /// without any change to explain it. They are only trusted inside those containers.
    /// </remarks>
    internal sealed class TestCertificateAuthority : IDisposable
    {
        #region Private Data

        private readonly X509Certificate2 _certificate;
        private readonly RSA              _key;

        #endregion

        #region Construction

        private TestCertificateAuthority( X509Certificate2 certificate, RSA key )
        {
            _certificate = certificate;
            _key         = key;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The authority's certificate, PEM encoded; this is what the database is made to trust.
        /// </summary>
        public string CertificatePem => _certificate.ExportCertificatePem();

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new, self-signed authority.
        /// </summary>
        /// <param name="commonName">The common name of the authority.</param>
        public static TestCertificateAuthority Create( string commonName )
        {
            // The authority keeps the key for as long as it issues certificates, so the key is not disposed here.
            var key = RSA.Create(2048);

            var request = new CertificateRequest($@"CN={commonName}", key, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            request.CertificateExtensions.Add(new X509BasicConstraintsExtension(certificateAuthority: true, hasPathLengthConstraint: false, pathLengthConstraint: 0, critical: true));
            request.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.KeyCertSign | X509KeyUsageFlags.CrlSign, critical: true));
            request.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(request.PublicKey, critical: false));

            // Back-date the start so that a container whose clock runs slightly behind still accepts the certificate.
            var certificate = request.CreateSelfSigned(DateTimeOffset.UtcNow.AddDays(-1), DateTimeOffset.UtcNow.AddYears(5));

            return new TestCertificateAuthority(certificate, key);
        }

        /// <summary>
        /// Issues a server certificate for the given DNS name.
        /// </summary>
        /// <param name="dnsName">The host name the certificate is valid for.</param>
        /// <returns>The certificate and its unencrypted private key, both PEM encoded.</returns>
        public (string CertificatePem, string PrivateKeyPem) IssueServerCertificate( string dnsName )
        {
            using var key = RSA.Create(2048);

            var request = new CertificateRequest($@"CN={dnsName}", key, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            var names = new SubjectAlternativeNameBuilder();
            names.AddDnsName(dnsName);

            request.CertificateExtensions.Add(names.Build());
            request.CertificateExtensions.Add(new X509BasicConstraintsExtension(certificateAuthority: false, hasPathLengthConstraint: false, pathLengthConstraint: 0, critical: true));
            request.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.KeyEncipherment, critical: true));
            request.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(new OidCollection { new Oid("1.3.6.1.5.5.7.3.1") }, critical: false));
            request.CertificateExtensions.Add(X509AuthorityKeyIdentifierExtension.CreateFromCertificate(_certificate, includeKeyIdentifier: true, includeIssuerAndSerial: false));

            // A serial number must be positive, so clear the sign bit.
            var serialNumber = RandomNumberGenerator.GetBytes(16);
            serialNumber[0] &= 0x7F;

            using var certificate = request.Create(_certificate, DateTimeOffset.UtcNow.AddDays(-1), DateTimeOffset.UtcNow.AddYears(2), serialNumber);

            return (certificate.ExportCertificatePem(), key.ExportPkcs8PrivateKeyPem());
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _certificate.Dispose();
            _key.Dispose();
        }

        #endregion
    }
}
