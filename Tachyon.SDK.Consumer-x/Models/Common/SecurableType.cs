namespace Tachyon.SDK.Consumer.Models.Common
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model defining a securable type
    /// </summary>
    public class SecurableType
    {
        /// <summary>
        /// Id of the securable type
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the securable type
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Timestamp of when this securable type was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
        /// <summary>
        /// Timestamp of when this securable type was last modified
        /// </summary>
        public DateTime ModifiedTimestampUtc { get; set; }
        /// <summary>
        /// List of operation applicable to this securable type
        /// </summary>
        public List<ApplicableOperation> Operations { get; set; }
    }
}
