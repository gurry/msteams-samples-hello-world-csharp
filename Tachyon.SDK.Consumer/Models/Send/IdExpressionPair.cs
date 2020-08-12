namespace Tachyon.SDK.Consumer.Models.Send
{
    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Model used to correlate an expression with an Id
    /// Example:
    /// Used to indicate what scope expression should be used for which instruction when getting projected statistics.
    /// </summary>
    public class IdExpressionPair
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Expression tree
        /// </summary>
        public ExpressionObject Expression { get; set; }
    }
}
