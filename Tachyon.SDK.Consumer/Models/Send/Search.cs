namespace Tachyon.SDK.Consumer.Models.Send
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Model to be used with search functionality
    /// </summary>
    public class Search
    {
        /// <summary>
        /// Filter expression
        /// </summary>
        public ExpressionObject Filter { get; set; }
        /// <summary>
        /// Start index
        /// </summary>
        public int? Start { get; set; }
        /// <summary>
        /// Maximum number of entries returned
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// Sorting expression
        /// </summary>
        public List<SortSpec> Sort { get; set; }
    }
}
