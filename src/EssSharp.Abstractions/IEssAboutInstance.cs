namespace EssSharp
{
    public interface IEssAboutInstance
    {
        /// <summary>
        /// Returns whether ProvisioningSupported
        /// </summary>
        public bool ProvisioningSupported { get; }
        /// <summary>
        /// Returns whether ResetPasswordSupported
        /// </summary>
        public bool ResetPasswordSupported { get; }
        /// <summary>
        /// Returns whether EasInstalled
        /// </summary>
        public bool EasInstalled { get; }

    }
}