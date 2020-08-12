namespace Tachyon.SDK.Consumer.Models.Common
{
    using System;

    /// <summary>
    /// Model defining a permission
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// Id of this permission
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id of an instance of a securable object this permission pertains to.
        /// For example, if the securable type is 'ProductPack' the this Id would be the Id of an instruction from a product pack.
        /// </summary>
        public int? SecurableId { get; set; }
        /// <summary>
        /// Id of the securable type this permission pertains to
        /// </summary>
        public int? SecurableTypeId { get; set; }
        /// <summary>
        /// Name of the securable type this permission pertains to
        /// </summary>
        public string SecurableTypeName { get; set; }
        /// <summary>
        /// Role Id
        /// </summary>
        public int? RoleId { get; set; }
        /// <summary>
        /// Role Name
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// Operation Id
        /// </summary>
        public int? OperationId { get; set; }
        /// <summary>
        /// Operation name
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// Flag defining if this permissions is allowed (granted)
        /// </summary>
        public bool Allowed { get; set; }
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
