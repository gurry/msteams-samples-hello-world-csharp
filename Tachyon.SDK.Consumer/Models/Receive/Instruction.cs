namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.Enums;
    using Tachyon.SDK.Consumer.Models.Common;
    /// <summary>
    /// Model representing an instruction
    /// </summary>
    public class Instruction
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Sequence number
        /// </summary>
        public int Sequence { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Instruction type
        /// </summary>
        public InstructionType InstructionType { get; set; }
        /// <summary>
        /// Payload in readable / UI friendly form
        /// </summary>
        public string ReadablePayload { get; set; }
        /// <summary>
        /// Described how the instruction was send. SendAll mean it was send to all devices, subject to scope. SendList means it was sent to a specific list of Fqdns
        /// </summary>
        public string Cmd { get; set; }
        /// <summary>
        /// Schema for the responses
        /// </summary>
        public List<SchemaObject> Schema { get; set; }
        /// <summary>
        /// Definition of how to aggregate responses
        /// </summary>
        public Aggregation Aggregation { get; set; }
        /// <summary>
        /// Flag defining if responses should be stored as aggregated data or raw and only aggregated when reading the responses.
        /// </summary>
        public bool? KeepRaw { get; set; }
        /// <summary>
        /// Expression defining the scope for this instruction
        /// </summary>
        public ExpressionObject Scope { get; set; }
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
        /// Status
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Workflow state
        /// </summary>
        public int WorkflowState { get; set; }
        /// <summary>
        /// Timestamp of when the status was last changed
        /// </summary>
        public DateTime? StatusTimestampUtc { get; set; }
        /// <summary>
        /// Flag indicating if responses should be exported automatically upon expiry
        /// </summary>
        public bool Export { get; set; }
        /// <summary>
        /// Path to where the results of this instruction should be exported upon expiry. Only used if Export is set to true
        /// </summary>
        public string ExportLocation { get; set; }
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
        /// Expression defining the results filter
        /// </summary>
        public ExpressionObject ResultsFilter { get; set; }
        /// <summary>
        /// Expression defining the previous results filter
        /// </summary>
        public ExpressionObject PreviousResultsFilter { get; set; }
        /// <summary>
        /// Id of the consumer that issued this instruction
        /// </summary>
        public int? ConsumerId { get; set; }
        /// <summary>
        /// Name of the consumer that issued this instruction
        /// </summary>
        public string ConsumerName { get; set; }
        /// <summary>
        /// Custom consumer data
        /// </summary>
        public string ConsumerCustomData { get; set; }
        /// <summary>
        /// Raw Json defining the paramteres and their values
        /// </summary>
        public string ParameterJson { get; set; }
        /// <summary>
        /// Flag indicating if the responses should be offloaded
        /// </summary>
        public bool OffloadResponses { get; set; }
        /// <summary>
        /// Field used by consumers to store information about their own internal user who issued instruction. (ie when different than Tachyon principal)
        /// </summary>
        public string RequestedFor { get; set; }
        /// <summary>
        /// The Id of a row from the ResponseTemplate table
        /// </summary>
        public int? ResponseTemplateId { get; set; }
        /// <summary>
        /// JSON indicating the mapping from the instruction results into the response template
        /// </summary>
        public ResponseTemplateConfigurations ResponseTemplateConfiguration { get; set; }
        /// <summary>
        /// Workflow used
        /// </summary>
        public string Workflow { get; set; }
        /// <summary>
        /// Comments for the instruction
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// Id of the scheduled instruction
        /// </summary>
        public int? ScheduledInstructionId { get; set; }
        /// <summary>
        /// User who actioned the instruction
        /// </summary>
        public string ActionedBy { get; set; }
        /// <summary>
        /// Reason for approval/rejection
        /// </summary>
        public string ActionReason { get; set; }
        /// <summary>
        /// Flag indicating that the approval responses should be offloaded
        /// </summary>
        public bool ApprovalOffloaded { get; set; }
    }
}
