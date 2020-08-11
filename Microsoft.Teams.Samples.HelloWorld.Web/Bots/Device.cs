using System;

namespace Microsoft.Teams.Samples.HelloWorld.Web
{
    internal class Device
    {    
        /// <summary>
        /// Gets the NetBIOS name of the device
        /// </summary>
        public string Name { get; set; }
        public string Fqdn { get; set; }

        /// <summary>
        /// BIOS GUID of the device
        /// </summary>
        public Guid SmBiosGuid { get; set; }

        public int Status { get; set; }

        /// <summary>
        /// The device's chassis type
        /// </summary>
        public AgentChassisType ChassisType { get; set; }

        /// <summary>
        /// The device's CPU architecture: x86 or x64
        /// </summary>
        public string CpuArchitecture { get; set; }

        /// <summary>
        /// The device's CPU model
        /// </summary>
        public string CpuType { get; set; }

        /// <summary>
        /// The device's current timezone offset in minutes
        /// </summary>
        public string TimeZoneId { get; set; }

        /// <summary>
        /// Type of device as far as the Agent is concerned
        /// </summary>
        public AgentDeviceType.Type DeviceType { get; set; }

        /// <summary>
        /// The device's (full) domain
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Whether a device is (apparently) online or not
        /// </summary>
        public bool IsActive() => Status == 1;

        /// <summary>
        /// Whether a VM or not
        /// </summary>
        public bool IsVm { get; private set; }

        /// <summary>
        /// The last reported time the client was active, from Client Health
        /// </summary>
        public DateTime LastActiveTime { get; private set; }

        /// <summary>
        /// When the O/S was booted
        /// </summary>
        public DateTime LastBootUTC { get; set; }

        public string LocalIpAddress { get; set; }

        /// <summary>
        /// The device's (chassis's) manufacturer
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// The device's model name
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Name of the O/S
        /// </summary>
        public string OsVerTxt { get; set; }

        /// <summary>
        /// The device's O/S architecture: x86 or x64
        /// </summary>
        public string OsArchitecture { get; set; }

        /// <summary>
        /// The broad O/S type
        /// </summary>
        public string OsType { get; set; }

        /// <summary>
        /// The O/S's version number in the Tachyon version format
        /// </summary>
        public ulong OsVerNum { get; set; }

        /// <summary>
        /// CM resource ID of the device
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// Unique CM ID of the client, or null
        /// </summary>
        public Guid? SmsId { get; set; }

        /// <summary>
        /// The device's physical memory in MB
        /// </summary>
        public int RamMb { get; set; }

        public string SerialNumber { get; set; }
    }
}