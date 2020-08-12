namespace Tachyon.SDK.Consumer.Models.Common
{
    using System;

    /// <summary>
    /// Data model defining a principal
    /// </summary>
    public class Principal
    {
        /// <summary>
        /// Principal Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Principal external Id (SID)
        /// </summary>
        public string ExternalId { get; set; }
        /// <summary>
        /// Principal name
        /// </summary>
        public string PrincipalName { get; set; }
        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Flag indicating if this principal is enabled
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// Timestamp of when this principal was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
        /// <summary>
        /// Timestamp of when this principal was last modified
        /// </summary>
        public DateTime ModifiedTimestampUtc { get; set; }
        /// <summary>
        /// Flag defining if this principal is a system principal
        /// </summary>
        public bool SystemPrincipal { get; set; }
        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Flag defining if this principal is a group
        /// </summary>
        public bool IsGroup { get; set; }
    }
}
