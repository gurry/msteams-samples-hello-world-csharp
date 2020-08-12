namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Model representing a post processor
    /// </summary>
    public class PostProcessor
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Build-in function definition
        /// </summary>
        public string Function { get; set; }
        /// <summary>
        /// Custom C# script
        /// </summary>
        public string Script { get; set; }
        /// <summary>
        /// Flag indicating if this processor uses aggregated data
        /// </summary>
        public bool? AggregatedData { get; set; }
    }
}
