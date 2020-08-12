namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Represents a model that holds information about
    /// an installer type and its associated extension
    /// </summary>
    public class InstallerType
    {
        /// <summary>
        /// Gets or Sets the name of installer type. For example, "Windows Installer Package".
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the extension of installer type. For example, .msi, .rpm.
        /// </summary>
        public string Extension { get; set; }
    }
}
