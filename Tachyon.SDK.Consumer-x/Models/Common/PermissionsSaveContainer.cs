namespace Tachyon.SDK.Consumer.Models.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// Model representing a container for permissions that have been added, modified or deleted
    /// </summary>
    public class PermissionsSaveContainer
    {
        /// <summary>
        /// Collection of new or modified permissions
        /// </summary>
        public IEnumerable<AggregatedPermission> PermissionsToSaveOrUpdate { get; set; }
        /// <summary>
        /// Collection of deleted permissions
        /// </summary>
        public IEnumerable<AggregatedPermission> PermissionsToDelete { get; set; }
    }
}
