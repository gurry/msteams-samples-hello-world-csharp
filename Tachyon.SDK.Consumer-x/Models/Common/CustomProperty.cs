namespace Tachyon.SDK.Consumer.Models.Common
{
    /// <summary>
    /// Model representing an custom property
    /// </summary>
    public class CustomProperty
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Id of the custom property type
        /// </summary>
        public int? TypeId { get; set; }
        /// <summary>
        /// Name of the custom property tyle
        /// </summary>
        public string TypeName { get; set; }
    }
}
