namespace Tachyon.SDK.Consumer.Models.Send
{
    /// <summary>
    /// Base class for all approval requests
    /// </summary>
    public abstract class ApprovalRequest
    {
        /// <summary>
        /// Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Approval flag. True if you wish to approve the instruction, false if you wish to reject it.
        /// </summary>
        public bool Approved { get; set; }
    }
}