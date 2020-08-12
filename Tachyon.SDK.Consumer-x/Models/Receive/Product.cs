namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Licensed product
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Name of the product
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Executable names, comma separated
        /// </summary>
        public string ExecutableNames { get; set; }
        /// <summary>
        /// Expiry date
        /// </summary>
        public DateTime? Expiry { get; set; }
        /// <summary>
        /// Maximum allowed users
        /// </summary>
        public int? MaxCount { get; set; }
        /// <summary>
        /// Collection of licensed features for this product
        /// </summary>
        public IEnumerable<Feature> Features { get; set; }
    }
}
