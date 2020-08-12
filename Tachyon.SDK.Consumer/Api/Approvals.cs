namespace Tachyon.SDK.Consumer.Api
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Receive;
    using Tachyon.SDK.Consumer.Models.Send;

    /// <summary>
    /// Abstracts Approvals controller of Consumer API
    /// </summary>
    public class Approvals : ApiBase
    {
        private readonly string coreEndpoint = "Approvals";
        private readonly string instructionApprovalEndpoint = "Approvals/Instruction";
        private readonly string scheduledInstructionApprovalEndpoint = "Approvals/ScheduledInstruction";
        private readonly string canApproveEndpoint = "Approvals/canapprove/{0}";
        private readonly string canApproveInstructionEndpoint = "Approvals/canapprove/instruction/{0}";
        private readonly string canApproveScheduledInstructionEndpoint = "Approvals/canapprove/scheduledinstruction/{0}";
        private readonly string notificationsEndpoint = "Approvals/getnotifications/";
        private readonly string instructionNotificationsEndpoint = "Approvals/notifications/instructions";
        private readonly string scheduledInstructionNotificationsEndpoint = "Approvals/notifications/scheduledinstructions";
        private readonly string allNotificationsEndpoint = "Approvals/notifications";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public Approvals(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Sends an instruction approval/rejection 
        /// </summary>
        /// <param name="approval">An instruction <see cref="InstructionApprovalRequest"/> request</param>
        /// <returns>No specific return value. Only success indication</returns>
        /// <remarks>This method is obsolete since v3.1 to support scheduled instruction. Use <see cref="ApproveInstruction"/> instead.</remarks>
        [Obsolete("This method is obsolete since v3.1 to support scheduled instruction. Use ApproveInstruction instead.", true)]
        public ApiCallResponse<object> SendApproval(InstructionApprovalRequest approval)
        {
            var apiCallResult = this.transportProxy.Post<InstructionApprovalRequest, object>(approval, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Approvals Api 'SendApproval' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Sends an instruction approval/rejection
        /// </summary>
        /// <param name="approval">An instruction <see cref="InstructionApprovalRequest"/> request</param>
        /// <returns>No specific return value. Only success indication</returns>
        /// <remarks>Calls POST on apiRoot/Approvals/Instruction</remarks>
        public ApiCallResponse<object> ApproveInstruction(InstructionApprovalRequest approval)
        {
            var apiCallResult = this.transportProxy.Post<InstructionApprovalRequest, object>(approval, this.instructionApprovalEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError(string.Format("Approvals Api '{0}' call failed", MethodBase.GetCurrentMethod().Name));
            }

            return apiCallResult;
        }

        /// <summary>
        /// Sends a scheduled instruction approval/rejection
        /// </summary>
        /// <param name="approval">A scheduled instruction <see cref="ScheduledInstructionApprovalRequest"/> request</param>
        /// <returns>No specific return value. Only success indication</returns>
        /// <remarks>Calls POST on apiRoot/Approvals/ScheduledInstruction</remarks>
        public ApiCallResponse<object> ApproveScheduledInstruction(ScheduledInstructionApprovalRequest approval)
        {
            var apiCallResult = this.transportProxy.Post<ScheduledInstructionApprovalRequest, object>(approval, this.scheduledInstructionApprovalEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError(string.Format("Approvals Api '{0}' call failed", MethodBase.GetCurrentMethod().Name));
            }

            return apiCallResult;
        }

        /// <summary>
        /// Checks if calling user can approve given instruction
        /// </summary>
        /// <param name="id">Id of the instruction to check</param>
        /// <returns>True if calling user can approve. False if they can't</returns>
        /// <remarks>This method is obsolete since v3.1 to support scheduled instruction. Use <see cref="CanIApproveInstruction" /> instead.</remarks>
        [Obsolete("This method is obsolete since v3.1 to support scheduled instruction. Use CanIApproveInstruction instead.", true)]
        public ApiCallResponse<bool> CanIApprove(int id)
        {
            var apiCallResult = this.transportProxy.Get<bool>(string.Format(this.canApproveEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Approvals Api 'CanIApprove' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Checks if calling user can approve given instruction
        /// </summary>
        /// <param name="id">Id of the instruction to check</param>
        /// <returns>True if calling user can approve. False if they can't</returns>
        /// <remarks>Calls GET on apiRoot/Approvals/canapprove/instruction/{instructionId}</remarks>
        public ApiCallResponse<bool> CanIApproveInstruction(int id)
        {
            var apiCallResult = this.transportProxy.Get<bool>(string.Format(this.canApproveInstructionEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError(string.Format("Approvals Api '{0}' call failed", MethodBase.GetCurrentMethod().Name));
            }

            return apiCallResult;
        }

        /// <summary>
        /// Checks if calling user can approve given scheduled instruction
        /// </summary>
        /// <param name="id">Id of the scheduled instruction to check</param>
        /// <returns>True if calling user can approve. False if they can't</returns>
        /// <remarks>Calls GET on apiRoot/Approvals/canapprove/scheduledinstruction/{id}</remarks>
        public ApiCallResponse<bool> CanIApproveScheduledInstruction(int id)
        {
            var apiCallResult = this.transportProxy.Get<bool>(string.Format(this.canApproveScheduledInstructionEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError(string.Format("Approvals Api '{0}' call failed", MethodBase.GetCurrentMethod().Name));
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns all instructions pending approval that calling user can approve
        /// </summary>
        /// <returns>List of instructions pending approval</returns>
        /// <remarks>This method is obsolete since v3.1 to support scheduled instruction. Use <see cref="GetInstructionNotificationsAwaitingApproval"/> instead.</remarks>
        /// <seealso cref="Models.Receive.Instruction"/>
        [Obsolete("This method is obsolete since v3.1 to support scheduled instruction. Use GetInstructionNotificationsAwaitingApproval instead.", true)]
        public ApiCallResponse<IEnumerable<Models.Receive.Instruction>> GetNotifications()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<Models.Receive.Instruction>>(this.notificationsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Approvals Api 'GetNotifications' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns all instructions pending approval that calling user can approve
        /// </summary>
        /// <returns>List of instructions pending approval</returns>
        /// <remarks>Calls GET on apiRoot/Approvals/notifications/instructions</remarks>
        /// <seealso cref="Models.Receive.Instruction"/>
        public ApiCallResponse<IEnumerable<Models.Receive.Instruction>> GetInstructionNotificationsAwaitingApproval()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<Models.Receive.Instruction>>(this.instructionNotificationsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError(string.Format("Approvals Api '{0}' call failed", MethodBase.GetCurrentMethod().Name));
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns all scheduled instructions pending approval that calling user can approve
        /// </summary>
        /// <returns>List of scheduled instructions pending approval</returns>
        /// <remarks>Calls GET on apiRoot/Approvals/notifications/scheduledinstructions</remarks>
        /// <seealso cref="Models.Receive.ScheduledInstruction"/>
        public ApiCallResponse<IEnumerable<Models.Receive.ScheduledInstruction>> GetScheduledInstructionNotificationsAwaitingApproval()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<Models.Receive.ScheduledInstruction>>(this.scheduledInstructionNotificationsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError(string.Format("Approvals Api '{0}' call failed", MethodBase.GetCurrentMethod().Name));
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns all pending approval requests that calling user can approve.
        /// These include instructions, scheduled instructions and push message device authorization.
        /// </summary>
        /// <returns>
        ///    A <see cref="PendingApprovalRequests"/> instance that wraps all pending approval requests for instructions, scheduled instructions and device authorization.
        /// </returns>
        /// <seealso cref="PendingApprovalRequests"/>
        public ApiCallResponse<PendingApprovalRequests> GetAllNotificationsAwaitingApproval()
        {
            var apiCallResult = this.transportProxy.Get<PendingApprovalRequests>(this.allNotificationsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError(string.Format("Approvals Api '{0}' call failed", MethodBase.GetCurrentMethod().Name));
            }

            return apiCallResult;
        }
    }
}
