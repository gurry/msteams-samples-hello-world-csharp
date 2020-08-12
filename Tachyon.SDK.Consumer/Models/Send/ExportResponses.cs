namespace Tachyon.SDK.Consumer.Models.Send
{
    /// <summary>
    /// Models used when requesting responses export
    /// </summary>
    public class ExportResponses
    {
        /// <summary>
        /// Id of the instruction whose responses you wish to export
        /// </summary>
        public int InstructionId { get; set; }
        /// <summary>
        /// Path where the responses should be exported
        /// </summary>
        public string ExportPath { get; set; }
    }
}
