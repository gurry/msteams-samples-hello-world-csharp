namespace Tachyon.SDK.Consumer.Models.Common
{
    using System.Collections.Generic;
    /// <summary>
    /// Model defining aggregation settings
    /// </summary>
    public class Aggregation
    {
        /// <summary>
        /// List of schema objects
        /// </summary>
        public List<SchemaObject> Schema { get; set; }
        /// <summary>
        /// Group by clause
        /// </summary>
        public string GroupBy { get; set; }
        /// <summary>
        /// List of aggregation operations
        /// </summary>
        public List<AggregationOperation> Operations { get; set; }
    }
}
