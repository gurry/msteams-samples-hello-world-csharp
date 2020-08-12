namespace Tachyon.SDK.Consumer.Models.Send
{
    /// <summary>
    /// Model representing an instruction set with the count of definitions that belong to that set
    /// </summary>
    public class InstructionSetWithCounts
    {
        /// <summary>
        /// Set Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Set Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Icon in binary form
        /// </summary>
        public byte[] Icon { get; set; }
        /// <summary>
        /// Number of definitions that belong to this set
        /// </summary>
        public int CountOfDefinitions { get; set; }
    }
}
