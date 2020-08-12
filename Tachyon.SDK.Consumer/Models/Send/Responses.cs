namespace Tachyon.SDK.Consumer.Models.Send
{
    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Model to be used when requesting responses
    /// </summary>
    public class Responses
    {
        /// <summary>
        /// Filter expression
        /// </summary>
        public ExpressionObject Filter { get; set; }
        /// <summary>
        /// Start token
        /// </summary>
        public string Start { get; set; }
        /// <summary>
        /// Number of results returned
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// DO NOT USE. Reserved for internal use only
        /// </summary>
        public int[] LimitDataStores { get; set; }
    }
}
