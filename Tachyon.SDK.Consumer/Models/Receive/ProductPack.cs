namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;
    /// <summary>
    /// Model representing a product pack
    /// </summary>
    public class ProductPack
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
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Icon in binary form
        /// </summary>
        public byte[] Icon { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// Timestamp of when the product pack was uploaded
        /// </summary>
        public DateTime UploadTimestampUtc { get; set; }
        /// <summary>
        /// Flag indicating if product pack has content
        /// </summary>
        public bool HasContent { get; set; }
    }
}
