namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System.Collections.Generic;
    /// <summary>
    /// Model representing instruction definitions related to given instruction definition
    /// </summary>
    public class RelatedInstructionDefinitions
    {
        /// <summary>
        /// Id of the instruction
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Definitions of instructions related to the instruction definition with the Id specified in the request and in the the Id field
        /// </summary>
        public IEnumerable<InstructionDefinition> InstructionDefinitions { get; set; }
    }
}
