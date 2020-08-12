namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Model representing an installer
    /// </summary>
    public class AgentDeploymentInstaller
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Operating system type ie Windows
        /// </summary>
        [DisplayName("Operating System Type")]
        public Enums.OperatingSystem OperatingSystemType { get; set; }
        /// <summary>
        /// Operating system version. Used by operating systems where different versions have to have different installers
        /// </summary>
        [DisplayName("Operating System Version")]
        public string OperatingSystemVersion { get; set; }
        /// <summary>
        /// Architecture (x86 or x64)
        /// </summary>
        [DisplayName("Operating System Architecture")]
        public Enums.OperatingSystemArchitecture OperatingSystemArchitecture { get; set; }
        /// <summary>
        /// Version of the Tachyon Agent
        /// </summary>
        [DisplayName("Tachyon Agent Version")]
        public string TachyonAgentVersion { get; set; }
        /// <summary>
        /// Location of the file, relative to the installers folder in the background channel
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Hash checksum of the installer binary.
        /// </summary>
        [Browsable(false)]
        public byte[] SHA256Checksum { get; set; }

        /// <summary>
        /// This override of the ToString method provides a human-readable description of the installer
        /// </summary>
        /// <returns>Textual description for the installer</returns>
        public override string ToString()
        {
            return string.Format("{0} - {1} ({2}/{3}) {4}", Id, OperatingSystemType, OperatingSystemVersion, OperatingSystemArchitecture, TachyonAgentVersion);
        }
    }
}
