namespace Tachyon.SDK.Consumer.Models.Common
{
    using System;

    /// <summary>
    /// Model representing a single device for an Agent Deployment Job
    /// </summary>
    public class AgentDeploymentDevice
    {
        /// <summary>
        /// Device id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id of the job this device belongs to
        /// </summary>
        public int JobId { get; set; }
        /// <summary>
        /// Devices address. This can be an IP address or FQDN
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Operating system of this device
        /// </summary>
        public int OperatingSystem { get; set; }
        /// <summary>
        /// Operating system architecture (32 or 64 bit)
        /// </summary>
        public int OperatingSystemArchitecture { get; set; }
        /// <summary>
        /// Version of the Tachyon agent to be installer on the device
        /// </summary>
        public string TachyonAgentVersion { get; set; }
        /// <summary>
        /// Current status of the installation process
        /// </summary>
        public int InstallStatus { get; set; }
        /// <summary>
        /// Messages received during installation
        /// </summary>
        public string StatusMessage { get; set; }
        /// <summary>
        /// Any custom data the calling consumer wishes to store with this device. Not used by Tachyon platform.
        /// </summary>
        public string ConsumerCustomData { get; set; }
    }
}