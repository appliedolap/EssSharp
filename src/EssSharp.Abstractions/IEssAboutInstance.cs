namespace EssSharp
{
    public interface IEssAboutInstance
    {
        /// <summary>
        /// Returns whether EAS is installed for the server.
        /// </summary>
        public bool EasInstalled { get; }

        /// <summary>
        /// Returns whether provisioning is supported by the server.
        /// </summary>
        public bool ProvisioningSupported { get; }

        /// <summary>
        /// Returns whether password reset is supported by the server for the current user.
        /// </summary>
        public bool ResetPasswordSupported { get; }
    }
}