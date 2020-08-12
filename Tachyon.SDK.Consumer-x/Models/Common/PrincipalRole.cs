namespace Tachyon.SDK.Consumer.Models.Common
{
    using System;

    /// <summary>
    /// Model defining a link betweek a principal and a role
    /// </summary>
    public class PrincipalRole
    {
        /// <summary>
        /// Principal Id
        /// </summary>
        public int PrincipalId { get; set; }
        /// <summary>
        /// Role Id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// Timestamp of when this link was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
        /// <summary>
        /// Role object
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// Principal object
        /// </summary>
        public Principal Principal { get; set; }
    }
}
