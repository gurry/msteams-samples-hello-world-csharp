namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Model representing one value from a single specific response
    /// </summary>
    public class ResponseValue
    {
        /// <summary>
        /// Id of the instruction thie response belongs to
        /// </summary>
        public int InstructionId { get; set; }
        /// <summary>
        /// Id of the shard the response was retrieved from
        /// </summary>
        public int ShardId { get; set; }
        /// <summary>
        /// Id of the row this response comes from
        /// </summary>
        public int RowId { get; set; }
        /// <summary>
        /// Name of the response column 
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// Value of the response.
        /// </summary>
        public object Value { get; set; }
    }
}
