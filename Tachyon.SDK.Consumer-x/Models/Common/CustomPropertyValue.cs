namespace Tachyon.SDK.Consumer.Models.Common
{
    /// <summary>
    /// Model representing an custom property value
    /// </summary>
    public class CustomPropertyValue
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Id of the custom property this value belongs to
        /// </summary>
        public int PropertyId { get; set; }
    }
}
