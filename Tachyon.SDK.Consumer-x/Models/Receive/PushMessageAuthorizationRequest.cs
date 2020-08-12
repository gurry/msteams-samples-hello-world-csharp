namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;

    /// <summary>
    /// Model representing Push Message Authorization Request
    /// </summary>
    public class PushMessageAuthorizationRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// User email
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// Guid of the request
        /// </summary>
        public string RequestGuid { get; set; }
        /// <summary>
        /// Device type (Android, iOS etc.)
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// Timestamp this request was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
        /// <summary>
        /// Timestamp this request was last modified
        /// </summary>
        public DateTime ModifiedTimestampUtc { get; set; }
    }
}
