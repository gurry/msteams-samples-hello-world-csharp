namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;
    /// <summary>
    /// Model representing a single other response / error
    /// </summary>
    public class ResponseError
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Shard Id
        /// </summary>
        public int ShardId { get; set; }
        /// <summary>
        /// Tachyon Guid of the machine sending this response
        /// </summary>
        public Guid TachyonGuid { get; set; }
        /// <summary>
        /// Fqdn of the machine sending this response
        /// </summary>
        public string Fqdn { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Time it took to execute the instruction
        /// </summary>
        public int ExecTime { get; set; }
        /// <summary>
        /// Error data
        /// </summary>
        public string ErrorData { get; set; }
        /// <summary>
        /// Timestamp of when this response was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
        /// <summary>
        /// Timestamp of when this response was sent
        /// </summary>
        public DateTime ResponseTimestampUtc { get; set; }
    }
}
