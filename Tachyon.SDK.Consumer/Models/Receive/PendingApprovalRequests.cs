namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a model that wraps all pending approval requests.
    /// These include instructions, scheduled instructions and push message device authorization.
    /// </summary>
    public class PendingApprovalRequests
    {
        /// <summary>
        /// All pending instruction approval requests
        /// </summary>
        public IEnumerable<Instruction> Instructions { get; set; }

        /// <summary>
        /// All pending scheduled instruction approvals requests
        /// </summary>
        public IEnumerable<ScheduledInstruction> ScheduledInstructions { get; set; }

        /// <summary>
        /// All pending push message device approval requests
        /// </summary>
        public IEnumerable<PushMessageAuthorizationRequest> Devices { get; set; }
    }
}
