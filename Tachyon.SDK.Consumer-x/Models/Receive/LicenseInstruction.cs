namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Licensed instruction entry
    /// </summary>
    public class LicenseInstruction
    {
        /// <summary>
        /// Thumbprint (SHA) of the certificated used to sign the instruction
        /// </summary>
        public string Thumbprint { get; set; }
        /// <summary>
        /// Pattern for the instruction's name
        /// </summary>
        public string Pattern { get; set; }
    }
}
