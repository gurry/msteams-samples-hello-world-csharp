namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Model representing aggregated error entry
    /// </summary>
    public class ResponseAggregatedError
    {
        /// <summary>
        /// Error data
        /// </summary>
        public string ErrorData { get; set; }
        /// <summary>
        /// Number of occurrences of the error
        /// </summary>
        public int Count { get; set; }
    }
}
