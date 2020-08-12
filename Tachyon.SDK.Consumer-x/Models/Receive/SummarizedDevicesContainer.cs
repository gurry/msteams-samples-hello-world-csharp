namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System.Collections.Generic;

    /// <summary>
    /// Model representing a container for summarized device information
    /// </summary>
    public class SummarizedDevicesContainer
    {
        /// <summary>
        /// Operating system type
        /// </summary>
        public IEnumerable<Item> OsType { get; set; }
        /// <summary>
        /// Installed version of Tachyon Agent
        /// </summary>
        public IEnumerable<Item> AgentVersion { get; set; }
        /// <summary>
        /// Status of the device
        /// </summary>
        public IEnumerable<Item> Status { get; set; }
        /// <summary>
        /// Type of the device
        /// </summary>
        public IEnumerable<Item> DeviceType { get; set; }
        /// <summary>
        /// Time zone
        /// </summary>
        public IEnumerable<Item> TimeZone { get; set; }
        /// <summary>
        /// Virtualization platform
        /// </summary>
        public IEnumerable<Item> VrPlatform { get; set; }

        /// <summary>
        /// Model representing a single device summary item
        /// </summary>
        public class Item
        {
            /// <summary>
            /// Caption
            /// </summary>
            public string Caption { get; set; }
            /// <summary>
            /// Count of items with given caption
            /// </summary>
            public int Count { get; set; }
        }
    }
}
