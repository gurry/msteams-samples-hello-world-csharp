namespace Tachyon.SDK.Consumer.Models.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// Model representing a container for aggregated device data
    /// </summary>
    public class AggregatedDeviceStatisticsContainer
    {
        /// <summary>
        /// Number of devices that are online
        /// </summary>
        public int TotalDevicesOnline { get; set; }
        /// <summary>
        /// Number of devices that are offline
        /// </summary>
        public int TotalDevicesOffline { get; set; }
        /// <summary>
        /// Statistics aggregated by device type
        /// </summary>
        public IEnumerable<AggregatedDeviceStatistics> ByDeviceType { get; set; }
        /// <summary>
        /// Statistics aggregated by operating system type
        /// </summary>
        public IEnumerable<AggregatedDeviceStatistics> ByOsType { get; set; }
    }
}
