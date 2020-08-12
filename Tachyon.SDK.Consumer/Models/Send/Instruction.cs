namespace Tachyon.SDK.Consumer.Models.Send
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Model to be used when issuing an instruction
    /// </summary>
    public class Instruction
    {
        /// <summary>
        /// Id of the instruction definition. You must supply either Id or Name of the definition but not both.
        /// </summary>
        public int? DefinitionId { get; set; }
        /// <summary>
        /// Name of the instruction definition. You must supply either Id or Name of the definition but not both.
        /// </summary>
        public string DefinitionName { get; set; }
        /// <summary>
        /// Scope expression for the instruction
        /// </summary>
        public ExpressionObject Scope { get; set; }
        /// <summary>
        /// Parameters for the instruction
        /// </summary>
        public List<InstructionParameter> Parameters { get; set; }
        /// <summary>
        /// Instruction's time to live. This defines for how long the system will gather responses.
        /// </summary>
        public int InstructionTtlMinutes { get; set; }
        /// <summary>
        /// Response's time to live. This defined for how long the system will keep responses after it has stopped gathering them.
        /// </summary>
        public int ResponseTtlMinutes { get; set; }
        /// <summary>
        /// Id of the parent instruction. Valid only for follow-up instructions.
        /// </summary>
        public int? ParentInstructionId { get; set; }
        /// <summary>
        /// Flag defining if responses should be stored as aggregated data or raw and only aggregated when reading the responses.
        /// Set to true if you wish to store raw data, which will allow you to retrieve either aggregated or non-aggregated responses.
        /// Set to false if you wish to store aggregated data.
        /// </summary>
        public bool? KeepRaw { get; set; }
        /// <summary>
        /// Flag indicating if the responses should be exported before they expire
        /// </summary>
        public bool? Export { get; set; }
        /// <summary>
        /// Location where the responses will be exported. Required when 'Export' flag is set to true. Used only if that flag is set to true.
        /// </summary>
        public string ExportLocation { get; set; }
        /// <summary>
        /// Expression defining the results filter
        /// </summary>
        public ExpressionObject ResultsFilter { get; set; }
        /// <summary>
        /// Expression defining the previous results filter
        /// </summary>
        public ExpressionObject PreviousResultsFilter { get; set; }
        /// <summary>
        /// List of device's Fqdns to send this instruction to. 
        /// </summary>
        public List<string> Devices { get; set; }
        /// <summary>
        /// Custom data in string form you wish to store with this instruction
        /// </summary>
        public string ConsumerCustomData { get; set; }
        /// <summary>
        /// Flag indicating if the responses should be offloaded to another consumer
        /// </summary>
        public bool OffloadResponses { get; set; }
        /// <summary>
        /// Field used by consumers to store information about their own internal user who issued instruction. (ie when different than Tachyon principal)
        /// </summary>
        public string RequestedFor { get; set; }
        /// <summary>
        /// Comments for the instruction
        /// </summary>
        public string Comments { get; set; }
    }
}
