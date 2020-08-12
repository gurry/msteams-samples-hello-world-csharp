namespace Tachyon.SDK.Consumer.Models.Common
{
    /// <summary>
    /// Model representing an applicable operation for a securable type
    /// </summary>
    public class ApplicableOperation
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// Securable type Id
        /// </summary>
        public int? SecurableTypeId { get; set; }
        /// <summary>
        /// Securable type Name
        /// </summary>
        public string SecurableTypeName { get; set; }
    }
}
