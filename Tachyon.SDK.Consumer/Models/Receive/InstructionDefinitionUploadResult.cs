namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Represents the result of a successfully uploaded instruction definition.
    /// </summary>
    public class InstructionDefinitionUploadResult
    {
        /// <summary>
        /// Name of the instruction definition
        /// </summary>
        public string InstructionDefinitionName { get; set; }

        /// <summary>
        /// Name of the instruction set this
        /// instruction definition currently belongs to.
        /// </summary>
        public string InstructionSetName { get; set; }

        /// <summary>
        /// Upload status. Probable values are New, Modified and Unmodified.
        /// </summary>
        public string Status { get; set; }
    }
}
