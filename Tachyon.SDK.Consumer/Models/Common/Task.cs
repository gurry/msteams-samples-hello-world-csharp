namespace Tachyon.SDK.Consumer.Models.Common
{
    using System;

    using Tachyon.SDK.Consumer.Enums;
    using Tachyon.SDK.Consumer.Models.Receive;

    /// <summary>
    /// Model defining a Task
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Instruction definition this task represents
        /// </summary>
        public InstructionDefinition InstructionDefinition { get; set; }
        /// <summary>
        /// Id of the instruction definition this task represents
        /// </summary>
        public int InstructionDefinitionId { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Readable payload (taken from the Instruction definition)
        /// </summary>
        public string ReadablePayload { get; set; }
        /// <summary>
        /// Id of the Task Group this Task belongs to. Tasks must belong to a Task Group
        /// </summary>
        public int ParentTaskGroupId { get; set; }
        /// <summary>
        /// Id of the last instruction executed based on this task
        /// </summary>
        public int? LastExecutionId { get; set; }
        /// <summary>
        /// Timestamp of the last execution
        /// </summary>
        public DateTime? LastExecutionTimestampUtc { get; set; }
        /// <summary>
        /// Status of the last execution
        /// </summary>
        public int? LastExecutionStatus { get; set; }
        /// <summary>
        /// Instruction type
        /// </summary>
        public InstructionType InstructionType { get; set; }
    }
}
