namespace Tachyon.SDK.Consumer.Models.Common
{
    /// <summary>
    /// Model defining an instruction set
    /// </summary>
    public class InstructionSet
    {
        /// <summary>
        /// Set Id
        /// </summary>
        public int Id { get; set; }
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
    }
}
