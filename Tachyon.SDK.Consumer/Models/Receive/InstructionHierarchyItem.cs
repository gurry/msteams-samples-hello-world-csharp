namespace Tachyon.SDK.Consumer.Models.Receive
{
    using Tachyon.SDK.Consumer.Enums;
    /// <summary>
    /// Model representing a single item in instruction hierarchy
    /// </summary>
    public class InstructionHierarchyItem
    {
        /// <summary>
        /// Instruction's Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id of the parent instruction
        /// </summary>
        public int? ParentInstructionId { get; set; }
        /// <summary>
        /// Name of the instruction
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Instruction type
        /// </summary>
        public InstructionType InstructionType { get; set; }
        /// <summary>
        /// Instruction's readable payload.
        /// </summary>
        public string ReadablePayload { get; set; }
    }
}
