namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// Model representing a single successful response that contains data.
    /// </summary>
    public class Response
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
        /// Time it took to execute the instruction
        /// </summary>
        public int ExecTime { get; set; }
        /// <summary>
        /// Timestamp of when this response was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
        /// <summary>
        /// Timestamp of when this response was sent
        /// </summary>
        public DateTime ResponseTimestampUtc { get; set; }
        /// <summary>
        /// Collection of values returned as the response. Values will follow the instruction's schema
        /// </summary>
        public Dictionary<string, object> Values { get; set; }
        /// <summary>
        /// Raw value returned from the client. Only valid for instructions that have no schema.
        /// </summary>
        public string Blob { get; set; }
    }
}
