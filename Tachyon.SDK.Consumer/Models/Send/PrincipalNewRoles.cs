namespace Tachyon.SDK.Consumer.Models.Send
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model to be used when adding or updating links between roles and principals
    /// </summary>
    public class PrincipalNewRoles
    {
        /// <summary>
        /// Id of the principal
        /// </summary>
        public int PrincipalId { get; set; }
        /// <summary>
        /// List of role Ids
        /// </summary>
        public List<int> RoleIdList { get; set; }
        /// <summary>
        /// Timestamp of the add/update operation
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
    }
}
