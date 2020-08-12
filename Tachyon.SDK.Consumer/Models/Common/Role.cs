namespace Tachyon.SDK.Consumer.Models.Common
{
    using System;

    /// <summary>
    /// Model defining a role
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Id of the role
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the role
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description of the role
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Timestamp of when this role was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
        /// <summary>
        /// Timestamp of when this role was last modified
        /// </summary>
        public DateTime ModifiedTimestampUtc { get; set; }
        /// <summary>
        /// Flag indicating if this is a system role
        /// </summary>
        public bool SystemRole { get; set; }
    }
}
