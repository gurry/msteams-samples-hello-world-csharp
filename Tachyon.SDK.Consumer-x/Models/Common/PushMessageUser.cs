namespace Tachyon.SDK.Consumer.Models.Common
{
    using System;

    /// <summary>
    /// Model for representing a single push message user and device combination
    /// </summary>
    public class PushMessageUser
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Type of registered device (Android, iOS etc.)
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Timestamp of when this entry was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
        /// <summary>
        /// Timestamp of when this entry was last modified
        /// </summary>
        public DateTime ModifiedTimestampUtc { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Device manufacturer
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// Device model
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// Operating system version
        /// </summary>
        public string OsVersion { get; set; }
    }
}
