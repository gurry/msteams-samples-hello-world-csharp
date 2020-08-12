namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Licensing service information
    /// </summary>
    public class LicenseService
    {
        /// <summary>
        /// Name of the signer certificate
        /// </summary>
        public string CertificateSignerName { get; set; }
        /// <summary>
        /// Name of the activating certificate
        /// </summary>
        public string ActivateCertificateName { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Alternative Url
        /// </summary>
        public string AlternateUrl { get; set; }
    }
}
