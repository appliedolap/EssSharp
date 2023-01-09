namespace EssSharp
{
    public interface IEssAboutInstance
    {
        /// <summary>
        /// Gets or Sets ProvisioningSupported
        /// </summary>
        public bool ProvisioningSupported { get; }
        /// <summary>
        /// Gets or Sets ResetPasswordSupported
        /// </summary>
        public bool ResetPasswordSupported { get; }
        /// <summary>
        /// Gets or Sets EasInstalled
        /// </summary>
        public bool EasInstalled { get; }

    }
}