namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;
    /// <summary>
    /// Model representing statistics of an instruction
    /// </summary>
    public class InstructionStatistic
    {
        /// <summary>
        /// Id of the statistics object
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Estimated number of responses. This is how many responses we expect based on statistics calculated when the instruction was being issued.
        /// </summary>
        public int EstimatedCount { get; set; }
        /// <summary>
        /// Number of responses received
        /// </summary>
        public int ReceivedCount { get; set; }
        /// <summary>
        /// Number of agents this instruction was sent to
        /// </summary>
        public int SentCount { get; set; }
        /// <summary>
        /// Number of outstanding responses
        /// </summary>
        public int OutstandingResponsesCount { get; set; }
        /// <summary>
        /// Average time it took to execute the instruction by the agents
        /// </summary>
        public TimeSpan AverageExecTime { get; set; }
        /// <summary>
        /// Instruction running time
        /// </summary>
        public TimeSpan Runningtime { get; set; }
        /// <summary>
        /// Total number of bytes sent
        /// </summary>
        public long TotalBytesSent { get; set; }
        /// <summary>
        /// Total number of bytes received
        /// </summary>
        public long TotalBytesReceived { get; set; }
        /// <summary>
        /// Average number of bytes received per response
        /// </summary>
        public long AverageBytesReceived { get; set; }
        /// <summary>
        /// Total number of rows inserted
        /// </summary>
        public int TotalRowInserts { get; set; }
        /// <summary>
        /// Total number of rows processed
        /// </summary>
        public int TotalRowProcessed { get; set; }
        /// <summary>
        /// Number of respondents reporting success
        /// </summary>
        public int TotalSuccessRespondents { get; set; }
        /// <summary>
        /// Number of respondents reporting success without data
        /// </summary>
        public int TotalSuccessNoDataRespondents { get; set; }
        /// <summary>
        /// Number of respondents reporting an error
        /// </summary>
        public int TotalErrorRespondents { get; set; }
        /// <summary>
        /// Number of respondents reporting method not implemented
        /// </summary>
        public int TotalNotImplementedRespondents { get; set; }
        /// <summary>
        /// Number of respondents reporting response too large
        /// </summary>
        public int TotalResponseTooLargeRespondents { get; set; }
        /// <summary>
        /// Number of respondents reporting they have subscribed to the event
        /// </summary>
        public int TotalSubscribedEventsRespondents { get; set; }
        /// <summary>
        /// Id of the instruction
        /// </summary>
        public int InstructionId { get; set; }
        /// <summary>
        /// Id of the instruction definition
        /// </summary>
        public int? InstructionDefinitionId { get; set; }
        /// <summary>
        /// Instruction's sequence number
        /// </summary>
        public int? Sequence { get; set; }
    }
}
