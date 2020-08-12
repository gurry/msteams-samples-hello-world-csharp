namespace Tachyon.SDK.Consumer.Models.Common
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Tachyon.SDK.Consumer.Enums;

    /// <summary>
    /// Model that represents a single instruction definition hint
    /// </summary>
    public class InstructionHint
    {
        /// <summary>
        /// Instruction definition id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Instruction definition name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Instruction definition readable name
        /// </summary>
        public string ReadableName { get; set; }
        /// <summary>
        /// Instruction definition description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Instruction definition category
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// Number of words that were matched with the hint text
        /// </summary>
        public int WordMatchCount { get; set; }
        /// <summary>
        /// Type of this instruction definition (Action, Event or Question)
        /// </summary>
        public InstructionType InstructionType { get; set; }
        /// <summary>
        /// Default time to live for instructions based on this definition. Defines for how long the responses will be gathered
        /// </summary>
        public int InstructionTtlMinutes { get; set; }
        /// <summary>
        /// Default time to live for the responses to instruction based on this definition.Defines how long responses will be kept after the system has stopped gathering responses
        /// </summary>
        public int ResponseTtlMinutes { get; set; }
        /// <summary>
        /// Set of parameter hints for this instruction definition
        /// </summary>
        public IEnumerable<ParameterHint> ParameterHints { get; set; }
        /// <summary>
        /// Flag defining if this instruction definition supports data aggregation
        /// </summary>
        public bool Aggregatable { get; set; }
        /// <summary>
        /// Flag indicating if this instruction definitions is licensed.
        /// Unlicensed definitions cannot be issued.
        /// </summary>
        public bool IsLicensed { get; set; }
        /// <summary>
        /// raw Json defining the aggregation for this instruction definition
        /// </summary>
        [JsonIgnore]
        public string AggregationJson { get; set; }
        /// <summary>
        /// Raw Json defining the parameters for this instruction definition
        /// </summary>
        [JsonIgnore]
        public string ParameterJson { get; set; }
        /// <summary>
        /// Id of the product pack that this instruction definition comes from
        /// </summary>
        [JsonIgnore]
        public int? ProductPackId { get; set; }
        /// <summary>
        /// Number of times that an instruction has been executed from this instruction definition
        /// </summary>
        public int NumberOfTimesExecuted { get; set; }
    }
}
