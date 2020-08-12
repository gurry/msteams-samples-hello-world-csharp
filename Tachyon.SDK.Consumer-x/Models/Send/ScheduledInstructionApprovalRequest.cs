namespace Tachyon.SDK.Consumer.Models.Send
{
    /// <summary>
    /// Model used when handling scheduled instruction approval
    /// </summary>
    public class ScheduledInstructionApprovalRequest : ApprovalRequest
    {
        /// <summary>
        /// Id of scheduled instruction
        /// </summary>
        public int ScheduledInstructionId { get; set; }
    }
}