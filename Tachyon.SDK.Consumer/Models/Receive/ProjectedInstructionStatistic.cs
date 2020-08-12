namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Model representing projected statistics for a given instruction
    /// </summary>
    public class ProjectedInstructionStatistic
    {
        /// <summary>
        /// Id of the instruction definition
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Number of times an instruction based of specified definition was run
        /// </summary>
        public int TotalRuns { get; set; }
        /// <summary>
        /// Number of devices that have responded
        /// </summary>
        public int TotalDevices { get; set; }
        /// <summary>
        /// Estimated count of machines that will response based on the scope expression given when requesting the statistics
        /// </summary>
        public int EstimatedCount { get; set; }
        /// <summary>
        /// Estimated number of bytes received
        /// </summary>
        public long EstimatedBytesReceived { get; set; }
        /// <summary>
        /// Estimated average execution time
        /// </summary>
        public long EstimatedAvgExecTime { get; set; }
        /// <summary>
        /// Estimated number of bytes sent
        /// </summary>
        public long EstimatedBytesSent { get; set; }
        /// <summary>
        /// Estimated number of database rows inserted
        /// </summary>
        public int EstimatedRowInserts { get; set; }
        /// <summary>
        /// Estimated number of rows to process
        /// </summary>
        public int EstimatedRowProcessed { get; set; }
        /// <summary>
        /// Estimated number of respondents reporting success with data
        /// </summary>
        public int EstimatedSuccessRespondents { get; set; }
        /// <summary>
        /// Estimated number of respondents reporting success without data
        /// </summary>
        public int EstimatedSuccessNoDataRespondents { get; set; }
        /// <summary>
        /// Estimated number of respondents reporting an error
        /// </summary>
        public int EstimatedErrorRespondents { get; set; }
        /// <summary>
        /// Estimated number of respondents reporting method not implemented
        /// </summary>
        public int EstimatedNotImplementedRespondents { get; set; }
        /// <summary>
        /// Estimated number of respondents reporting the response was too large
        /// </summary>
        public int EstimatedResponseTooLargeRespondents { get; set; }
        /// <summary>
        /// Id of an instruction
        /// </summary>
        public int InstructionId { get; set; }
    }
}
