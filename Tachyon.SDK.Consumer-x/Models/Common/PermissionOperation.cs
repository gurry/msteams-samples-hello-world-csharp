namespace Tachyon.SDK.Consumer.Models.Common
{
    using System;

    /// <summary>
    /// Model defining a link between a permission and an operation
    /// </summary>
    public class PermissionOperation
    {
        /// <summary>
        /// Permission Id
        /// </summary>
        public int PermissionId { get; set; }
        /// <summary>
        /// Operation Id
        /// </summary>
        public int OperationId { get; set; }
        /// <summary>
        /// Operation name
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// Timestamp of when this object was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
        /// <summary>
        /// Timestamp of when this object was last modified
        /// </summary>
        public DateTime ModifiedTimestampUtc { get; set; }
    }
}
