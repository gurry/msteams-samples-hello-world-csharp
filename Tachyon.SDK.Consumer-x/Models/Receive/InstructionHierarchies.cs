namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System.Collections.Generic;
    /// <summary>
    /// Model representing hierarchy of instructions
    /// </summary>
    public class InstructionHierarchies
    {
        /// <summary>
        /// "Parent" or "Up the chain" instructions
        /// </summary>
        public List<InstructionHierarchyItem> UpperLevels { get; set; }
        /// <summary>
        /// "Child" or "Down the chain" instructions
        /// </summary>
        public List<InstructionHierarchyItem> Children { get; set; }
    }
}
