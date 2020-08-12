namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;
    /// <summary>
    /// Model representing an audit log entry
    /// </summary>
    public class AuditLog
    {
        /// <summary>
        /// Id of the entry
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Component this entry pertains to
        /// </summary>
        public string Component { get; set; }
        /// <summary>
        /// Name of the principal that made the entry
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Message headline
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Detailed message
        /// </summary>
        public string DetailMessage { get; set; }
        /// <summary>
        /// Comment attached to this entry
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Timestamp of when this entry was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
    }
}
