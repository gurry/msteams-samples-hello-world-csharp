namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model representing Scheduled Search results
    /// </summary>
    public class ScheduledSearchResult
    {
        /// <summary>
        /// Id of scheduled instruction.
        /// This is the ID for scheduling, NOT the issued instruction.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of scheduled instruction
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Creation Timestamp of scheduled instruction
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
        /// <summary>
        /// Created by
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Id of the consumer that has created the scheduled instruction
        /// </summary>
        public int? ConsumerId { get; set; }
        /// <summary>
        /// Name of the consumer that has created the scheduled instruction
        /// </summary>
        public string ConsumerName { get; set; }
        /// <summary>
        /// Consumer Custom Data attached to the instruction
        /// </summary>
        public string ConsumerCustomData { get; set; }
        /// <summary>
        /// Parameter JSON
        /// </summary>
        public string ParameterJson { get; set; }
        /// <summary>
        /// Comments attached to the instruction
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// Next scheduled execution date time
        /// </summary>
        public DateTime? ScheduleNextExecution { get; set; }
        /// <summary>
        /// Schedule frequency
        /// </summary>
        public string ScheduleReadableFrequency { get; set; }
        /// <summary>
        /// Readable payload of the instruction
        /// </summary>
        public string ReadablePayload { get; set; }
        /// <summary>
        /// For previously executed schedules, this will be the ID of the Instruction that was generated. It will be null for predicted future schedules.
        /// </summary>
        public int? InstructionId { get; set; }
    }
}
