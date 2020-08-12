namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;

    using Tachyon.SDK.Consumer.Enums;
    /// <summary>
    /// Model representing a summary of information and some statistics for an instruction
    /// </summary>
    public class InstructionSummary
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Type of the instruction
        /// </summary>
        public InstructionType InstructionType { get; set; }
        /// <summary>
        /// Payload in a readable, UI friendly format
        /// </summary>
        public string ReadablePayload { get; set; }
        /// <summary>
        /// Defines if the instruction is sent to all endpoint or to a specific list of Fqdns
        /// </summary>
        public string Cmd { get; set; }
        /// <summary>
        /// Instruction scope expression
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// Instruction's time to live. This defines for how long the system will gather responses.
        /// </summary>
        public int InstructionTtlMinutes { get; set; }
        /// <summary>
        /// Response's time to live. This defined for how long the system will keep responses after it has stopped gathering them.
        /// </summary>
        public int ResponseTtlMinutes { get; set; }
        /// <summary>
        /// Timestamp of when this instruction was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
        /// <summary>
        /// Timestamp of when this instruction was sent
        /// </summary>
        public DateTime? SentTimestampUtc { get; set; }
        /// <summary>
        /// Timestamp of when status last changed
        /// </summary>
        public DateTime? StatusTimestampUtc { get; set; }
        /// <summary>
        /// Instruction status
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Id of the parent instruction
        /// </summary>
        public int? ParentInstructionId { get; set; }
        /// <summary>
        /// Id of the instruction definition that was used to create this instruction
        /// </summary>
        public int? InstructionDefinitionId { get; set; }
        /// <summary>
        /// Principal that created this instruction
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Number of all responses received
        /// </summary>
        public int ReceivedCount { get; set; }
        /// <summary>
        /// Number of agents that the instruction has been sent to
        /// </summary>
        public int SentCount { get; set; }
        /// <summary>
        /// Number of outstanding responses
        /// </summary>
        public int OutstandingResponsesCount { get; set; }
        /// <summary>
        /// Total number of respondents reporting success
        /// </summary>
        public int TotalSuccessRespondents { get; set; }
        /// <summary>
        /// Total number of respondents reporting success without data
        /// </summary>
        public int TotalSuccessNoDataRespondents { get; set; }
        /// <summary>
        /// Total number of respondents reporting an error
        /// </summary>
        public int TotalErrorRespondents { get; set; }
        /// <summary>
        /// Total number of respondents reporting method not implemented
        /// </summary>
        public int TotalNotImplementedRespondents { get; set; }
        /// <summary>
        /// Total number of respondents reporting response too large
        /// </summary>
        public int TotalResponseTooLargeRespondents { get; set; }
        /// <summary>
        /// Average execution time
        /// </summary>
        public double AverageExecTime { get; set; }
        /// <summary>
        /// Custom data set while creating the instruction
        /// </summary>
        public string ConsumerCustomData { get; set; }
        /// <summary>
        /// Field used by consumers to store information about their own internal user who issued instruction. (ie when different than Tachyon principal)
        /// </summary>
        public string RequestedFor { get; set; }
        /// <summary>
        /// Id of the consumer that issued the instruction
        /// </summary>
        public int? ConsumerId { get; set; }
        /// <summary>
        /// Name of the consumer that issued the instruction
        /// </summary>
        public string ConsumerName { get; set; }
        /// <summary>
        /// Comments for the instruction
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// User who actioned the instruction
        /// </summary>
        public string ActionedBy { get; set; }
        /// <summary>
        /// Reason for approval/rejection
        /// </summary>
        public string ActionReason { get; set; }
    }
}
