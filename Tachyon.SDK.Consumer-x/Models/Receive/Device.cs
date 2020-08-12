namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// Model representing a single device connected to the Tachyon system through a Tachyon Agent
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Device Fqdn
        /// </summary>
        public string Fqdn { get; set; }
        /// <summary>
        /// Device status
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Operating system type
        /// </summary>
        public string OsType { get; set; }
        /// <summary>
        /// Operating system version in numeric form
        /// </summary>
        public long? OsVerNum { get; set; }
        /// <summary>
        /// Operating system version in textual form
        /// </summary>
        public string OsVerTxt { get; set; }
        /// <summary>
        /// Version of the agent installed on this device
        /// </summary>
        public long? AgentVersion { get; set; }
        /// <summary>
        /// Device's manufacturer
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// Device's chassis type (laptop, desktop, server etc.)
        /// </summary>
        public int? ChassisType { get; set; }
        /// <summary>
        /// Device type
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// Cpu type
        /// </summary>
        public string CpuType { get; set; }
        /// <summary>
        /// Cpu architecture
        /// </summary>
        public string CpuArchitecture { get; set; }
        /// <summary>
        /// Operating system architecture
        /// </summary>
        public string OsArchitecture { get; set; }
        /// <summary>
        /// Amount of RAM memory im MB
        /// </summary>
        public int? RamMB { get; set; }
        /// <summary>
        /// SMBios Guid
        /// </summary>
        public Guid? SMBiosGuid { get; set; }
        /// <summary>
        /// Tachyon Guid
        /// </summary>
        public Guid TachyonGuid { get; set; }
        /// <summary>
        /// Bios version
        /// </summary>
        public string BiosVersion { get; set; }
        /// <summary>
        /// Timestamp of when this device has booted up
        /// </summary>
        public DateTime? LastBootUTC { get; set; }
        /// <summary>
        /// Timestamp of when this device last connected
        /// </summary>
        public DateTime LastConnUtc { get; set; }
        /// <summary>
        /// Timestamp of when this device first connected
        /// </summary>
        public DateTime? CreatedUtc { get; set; }
        /// <summary>
        /// Virtualization platform
        /// </summary>
        public string VrPlatform { get; set; }
        /// <summary>
        /// Time zone
        /// </summary>
        public int? TimeZone { get; set; }
        /// <summary>
        /// Certificate type
        /// </summary>
        public string CertType { get; set; }
        /// <summary>
        /// Certificate expiry date
        /// </summary>
        public DateTime? CertExpiryUtc { get; set; }
        /// <summary>
        /// Push token
        /// </summary>
        public string PushToken { get; set; }
        /// <summary>
        /// Device's model
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Domain this device is connected to
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// Tags assigned to the device
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// Connection state
        /// </summary>
        public List<string> ConnectionState { get; set; }
        /// <summary>
        /// Local ip address
        /// </summary>
        public string LocalIpAddress { get; set; }
        /// <summary>
        /// Time zone Id
        /// </summary>
        public string TimeZoneId { get; set; }
        /// <summary>
        /// Coverage tags applied on the device
        /// </summary>
        public Dictionary<string, string> CoverageTags { get; set; }
    }
}
