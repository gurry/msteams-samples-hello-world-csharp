namespace Tachyon.SDK.Consumer.Models.Send
{
    /// <summary>
    /// Model used when handling instruction approval
    /// </summary>
    public class InstructionApprovalRequest : ApprovalRequest
    {
        /// <summary>
        /// Id of instruction
        /// </summary>
        public int InstructionId { get; set; }
    }
}
