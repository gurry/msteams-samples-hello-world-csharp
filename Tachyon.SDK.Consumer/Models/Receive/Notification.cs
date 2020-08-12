namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;

    using Tachyon.SDK.Consumer.Enums;
    /// <summary>
    /// Model representing a notification
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Id of the notification
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id of the instruction this notification is about
        /// </summary>
        public int? InstructionId { get; set; }
        /// <summary>
        /// Component
        /// </summary>
        public string Component { get; set; }
        /// <summary>
        /// Component Id
        /// </summary>
        public int? ComponentId { get; set; }
        /// <summary>
        /// Notification type
        /// </summary>
        public NotificationType Type { get; set; }
        /// <summary>
        /// Operation
        /// </summary>
        public string Operation { get; set; }
        /// <summary>
        /// Notification message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Additional data
        /// </summary>
        public string Additional { get; set; }
        /// <summary>
        /// Timestamp of when this notification was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
    }
}
