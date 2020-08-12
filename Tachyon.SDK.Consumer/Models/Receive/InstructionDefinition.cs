namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.Enums;
    using Tachyon.SDK.Consumer.Models.Common;
    /// <summary>
    /// Model representing a definition of an instructions
    /// </summary>
    public class InstructionDefinition
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
        /// Id of the instruction set this definition belongs to
        /// </summary>
        public int? InstructionSetId { get; set; }
        /// <summary>
        /// Name of the instruction set this definition belongs to
        /// </summary>
        public string InstructionSetName { get; set; }
        /// <summary>
        /// Instruction type
        /// </summary>
        public InstructionType InstructionType { get; set; }
        /// <summary>
        /// Payload in readable / UI friendly form
        /// </summary>
        public string ReadablePayload { get; set; }
        /// <summary>
        /// Parameters used by this instruction definition
        /// </summary>
        public List<InstructionParameter> Parameters { get; set; }
        /// <summary>
        /// Schema for the responses
        /// </summary>
        public List<SchemaObject> Schema { get; set; }
        /// <summary>
        /// Definition of how to aggregate responses
        /// </summary>
        public Aggregation Aggregation { get; set; }
        /// <summary>
        /// Default value for instruction ttl for instructions based on this definition
        /// </summary>
        public int InstructionTtlMinutes { get; set; }
        /// <summary>
        /// Default value for response ttl for instructions based on this definition
        /// </summary>
        public int ResponseTtlMinutes { get; set; }
        /// <summary>
        /// Minimum value that can be used as instruction ttl for instructions based on this definition
        /// </summary>
        public int? MinimumInstructionTtlMinutes { get; set; }
        /// <summary>
        /// Maximum value that can be used as instruction ttl for instructions based on this definition
        /// </summary>
        public int? MaximumInstructionTtlMinutes { get; set; }
        /// <summary>
        /// Minimum value that can be used as response ttl for instructions based on this definition
        /// </summary>
        public int? MinimumResponseTtlMinutes { get; set; }
        /// <summary>
        /// Maximum value that can be used as response ttl for instructions based on this definition
        /// </summary>
        public int? MaximumResponseTtlMinutes { get; set; }
        /// <summary>
        /// Workflow used
        /// </summary>
        public string Workflow { get; set; }
        /// <summary>
        /// Id of he response template used, if any
        /// </summary>
        public int? ResponseTemplateId { get; set; }
        /// <summary>
        /// Name of the file this definition originally came from
        /// </summary>
        public string OriginalFileName { get; set; }
        /// <summary>
        /// Configuration for the response template specified above
        /// </summary>
        public ResponseTemplateConfigurations ResponseTemplateConfiguration { get; set; }
        /// <summary>
        /// UTC time when the instruction definition was last uploaded
        /// </summary>
        public DateTime UploadedTimestampUtc { get; set; }
        /// <summary>
        /// Version of the instruction definition
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Author of the instruction definition
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Flag indicating if this instruction definitions is licensed.
        /// Unlicensed definitions cannot be issued.
        /// </summary>
        public bool IsLicensed { get; set; }
        /// <summary>
        /// Number of times that an instruction has been executed from this instruction definition
        /// </summary>
        public int NumberOfTimesExecuted { get; set; }
    }
}
