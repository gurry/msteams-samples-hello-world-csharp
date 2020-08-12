namespace Tachyon.SDK.Consumer.Models.Common
{
    /// <summary>
    /// Model representing a consumer of the Tachyon platform
    /// </summary>
    public class Consumer
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
        /// Target for offloading
        /// </summary>
        public string OffloadTargetUrl { get; set; }
        /// <summary>
        /// Timer for offloading
        /// </summary>
        public int? OffloadPostTimeoutSeconds { get; set; }
        /// <summary>
        /// Flag defining if windows authentication is to be used for offloading
        /// </summary>
        public bool? OffloadUseWindowsAuth { get; set; }
        /// <summary>
        /// Url where the Consumer can be reached. Valid mostly for web based consumers
        /// </summary>
        public string ApplicationUrl { get; set; }
        /// <summary>
        /// Flag defining if this consumer is enabled (ie can communicate with the Tachyon platform)
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// Flag defining if this is a system consumer. Returned from the api but ignored if sent to the api.
        /// </summary>
        public bool SystemConsumer { get; set; }
        /// <summary>
        /// Maximum number of instructions issued by this consumer can be in flight at the same time
        /// </summary>
        public int? MaximumSimultaneousInFlightInstructions { get; set; }
    }
}
