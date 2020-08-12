namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System.Collections.Generic;
    /// <summary>
    /// Model representing a search results
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchResult<T>
    {
        /// <summary>
        /// Number of items returned by the search
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// Items returned as a result of the search
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}
