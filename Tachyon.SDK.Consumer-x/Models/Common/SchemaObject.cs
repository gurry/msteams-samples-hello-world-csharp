namespace Tachyon.SDK.Consumer.Models.Common
{
    /// <summary>
    /// Model defining a schema column
    /// </summary>
    public class SchemaObject
    {
        /// <summary>
        /// Column name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Column type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Data length. Valid for string columns
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// Indication on how to render this data in a UI
        /// </summary>
        public string RenderAs { get; set; }
    }
}
