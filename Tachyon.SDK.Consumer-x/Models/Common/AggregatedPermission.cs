namespace Tachyon.SDK.Consumer.Models.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// Model representing an aggregated permission
    /// </summary>
    public class AggregatedPermission
    {
        /// <summary>
        /// Id of a specific instance of a securable object
        /// </summary>
        public int? SecurableId { get; set; }
        /// <summary>
        /// Name of a specific instance of a securable object
        /// </summary>
        public string SecurableName { get; set; }
        /// <summary>
        /// Id of a securable type
        /// </summary>
        public int SecurableTypeId { get; set; }
        /// <summary>
        /// Name of a securable type
        /// </summary>
        public string SecurableTypeName { get; set; }
        /// <summary>
        /// Role Id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// Role Name
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// Flag determining if the permission is granted
        /// </summary>
        public bool Allowed { get; set; }
        /// <summary>
        /// Collection of operations
        /// </summary>
        public IEnumerable<PermissionOperation> Operations { get; set; }
    }
}
