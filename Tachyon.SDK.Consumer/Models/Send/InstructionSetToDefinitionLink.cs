namespace Tachyon.SDK.Consumer.Models.Send
{
    using System.Collections.Generic;

    /// <summary>
    /// Model representing a link between an instruction set an a collection of instruction definitions represented by their Ids.
    /// </summary>
    public class InstructionSetToDefinitionLink
    {
        /// <summary>
        /// Id of an instruction set. Null value means 'unassigned' set.
        /// </summary>
        public int? SetId { get; set; }
        /// <summary>
        /// List of instruction definition ids.
        /// </summary>
        public IEnumerable<int> InstructionDefinitionIds { get; set; }
    }
}
