namespace Tachyon.SDK.Consumer.Models.Common
{
    /// <summary>
    /// Model representing aggregated device statistics
    /// </summary>
    public class AggregatedDeviceStatistics
    {
        /// <summary>
        /// Aggregated value
        /// </summary>
        public string AggregateValue { get; set; }
        /// <summary>
        /// Number of offline machines
        /// </summary>
        public int CountOffline { get; set; }
        /// <summary>
        /// Number of online machines
        /// </summary>
        public int CountOnline { get; set; }
    }
}
