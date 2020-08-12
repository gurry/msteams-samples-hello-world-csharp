namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Model representing high level system statistics
    /// </summary>
    public class HighLevelSystemStatistics
    {
        /// <summary>
        /// Number of connected devices
        /// </summary>
        public long ConnectedDeviceCount { get; set; }
        /// <summary>
        /// Number of devices that were connected in the last 7 days
        /// </summary>
        public long ConnectedDeviceCountLastSevenDays { get; set; }
        /// <summary>
        /// Number of instructions in progress
        /// </summary>
        public long InstructionInProgressCount { get; set; }
        /// <summary>
        /// Number of instruction that are pending approval
        /// </summary>
        public int InstructionsPendingApproval { get; set; }
        /// <summary>
        /// Number of pending device authorizations
        /// </summary>
        public long DeviceAuthorizationRequests { get; set; }
        /// <summary>
        /// Licensing information about expiry of TachyonExplorer feature in Tachyon product.
        /// If you wish to find out expiry information for other features, please use the Licensing API.
        /// </summary>
        public FeatureExpiry License { get; set; }
        /// <summary>
        /// Number of scheduled instruction pending approval
        /// </summary>
        public int ScheduledInstructionsPendingApproval { get; set; }
        /// <summary>
        /// Status of the Tachyon product license
        /// </summary>
        public string LicenseStatus { get; set; }
    }
}
