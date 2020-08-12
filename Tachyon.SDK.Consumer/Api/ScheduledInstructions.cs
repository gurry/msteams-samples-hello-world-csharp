namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;
    using Tachyon.SDK.Consumer.Models.Receive;

    /// <summary>
    /// Abstracts ScheduledInstructions controller of Consumer API
    /// </summary>
    public class ScheduledInstructions : ApiBase
    {
        private readonly string endpoint = "ScheduledInstructions";
        private readonly string targetedEndpoint = "ScheduledInstructions/Targeted";
        private readonly string updateEndpoint = "ScheduledInstructions/Update/{0}";
        private readonly string updateTargetedEndpoint = "ScheduledInstructions/Targeted/{0}";
        private readonly string cancelEndpoint = "ScheduledInstructions/{0}/cancel";
        private readonly string searchEndpoint = "ScheduledInstructions/Search";
        private readonly string describeEndpoint = "ScheduledInstructions/Describe";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public ScheduledInstructions(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets all the scheduled instructions
        /// </summary>
        /// <returns>Collection of scheduled instructions</returns>
        /// <remarks>Calls GET on apiRoot/ScheduledInstructions</remarks>
        public ApiCallResponse<IEnumerable<Models.Receive.ScheduledInstruction>> GetAll()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<Models.Receive.ScheduledInstruction>>(this.endpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Scheduled Instruction Api 'GetAll' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns a single entry from the scheduling table
        /// </summary>
        /// <param name="id">Id of the scheduledInstruction to look for</param>
        /// <returns>ScheduledInstruction object</returns>
        /// <remarks>Calls GET on apiRoot/ScheduledInstructions/{id}</remarks>
        public ApiCallResponse<Models.Receive.ScheduledInstruction> GetScheduledInstruction(int id)
        {
            var idEndpoint = this.endpoint + "/" + id;
            var apiCallResult = this.transportProxy.Get<Models.Receive.ScheduledInstruction>(idEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError(string.Format("Unable to retrieve scheduled instruction {0}", id));
            }

            return apiCallResult;
        }

        /// <summary>
        /// Describe a scheduled instruction
        /// </summary>
        /// <param name="scheduledInstruction">Scheduled Instruction to evaluate</param>
        /// <returns>Returns the same ScheduledInstruction that was received, where the ScheduleReadableFrequency field has been filled.</returns>
        /// <remarks>Calls POST on apiRoot/ScheduledInstructions/Describe</remarks>
        public ApiCallResponse<Models.Receive.ScheduledInstruction> Describe(Models.Send.ScheduledInstruction scheduledInstruction)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.ScheduledInstruction, Models.Receive.ScheduledInstruction>(scheduledInstruction, this.describeEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Scheduled Instructions Api 'Describe' call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Sends a new scheduled instruction
        /// </summary>
        /// <param name="scheduledInstruction">Scheduled Instruction to send</param>
        /// <returns>Newly created entry if successful. Null otherwise.</returns>
        /// <remarks>Calls POST on apiRoot/ScheduledInstructions</remarks>
        public ApiCallResponse<Models.Receive.ScheduledInstruction> SendScheduledInstruction(Models.Send.ScheduledInstruction scheduledInstruction)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.ScheduledInstruction, Models.Receive.ScheduledInstruction>(scheduledInstruction, this.endpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Scheduled Instructions Api 'SendScheduledInstruction' call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Sends a new targeted scheduled instruction
        /// </summary>
        /// <param name="scheduledInstruction">Scheduled Instruction to send</param>
        /// <returns>Newly created entry if successful. Null otherwise.</returns>
        /// <remarks>Calls POST on apiRoot/ScheduledInstructions/Targeted</remarks>
        public ApiCallResponse<Models.Receive.ScheduledInstruction> SendTargetedScheduledInstruction(Models.Send.ScheduledInstruction scheduledInstruction)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.ScheduledInstruction, Models.Receive.ScheduledInstruction>(scheduledInstruction, this.targetedEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Scheduled Instructions Api 'SendTargetedScheduledInstruction' call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Update a scheduled instruction
        /// </summary>
        /// <param name="scheduledInstructionId">The id of the old schedule that we want to replace</param>
        /// <param name="scheduledInstruction">Scheduled Instruction to save instead of the old one</param>
        /// <returns>Newly saved entry.</returns>
        /// <remarks>Calls PUT on apiRoot/ScheduledInstructions/Update/{id}</remarks>
        public ApiCallResponse<Models.Receive.ScheduledInstruction> UpdateScheduledInstruction(int scheduledInstructionId, Models.Send.ScheduledInstruction scheduledInstruction)
        {
            var apiCallResult = this.transportProxy.Put<Models.Send.ScheduledInstruction, Models.Receive.ScheduledInstruction>(scheduledInstruction, string.Format(this.updateEndpoint, scheduledInstructionId));
            if (!apiCallResult.Success)
            {
                this.LogError("Scheduled Instructions Api 'UpdateScheduledInstruction' call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Update a targeted scheduled instruction
        /// </summary>
        /// <param name="scheduledInstructionId">The id of the old schedule that we want to replace</param>
        /// <param name="scheduledInstruction">Scheduled Instruction to save instead of the old one</param>
        /// <returns>Newly saved entry.</returns>
        /// <remarks>Calls PUT on apiRoot/ScheduledInstructions/Targeted/{id}</remarks>
        public ApiCallResponse<Models.Receive.ScheduledInstruction> UpdateTargetedScheduledInstruction(int scheduledInstructionId, Models.Send.ScheduledInstruction scheduledInstruction)
        {
            var apiCallResult = this.transportProxy.Put<Models.Send.ScheduledInstruction, Models.Receive.ScheduledInstruction>(scheduledInstruction, string.Format(this.updateTargetedEndpoint, scheduledInstructionId));
            if (!apiCallResult.Success)
            {
                this.LogError("Scheduled Instructions Api 'UpdateTargetedScheduledInstruction' call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Search for instances of instructions generated from a scheduled instruction
        /// </summary>
        /// <param name="scheduledSearch">Model containing the search parameters</param>
        /// <returns>SearchResult ScheduledSearchReturn object</returns>
        /// <remarks>Calls POST on apiRoot/ScheduledInstructions/Search</remarks>
        public ApiCallResponse<SearchResult<ScheduledSearchResult>> Search(Models.Send.ScheduledSearch scheduledSearch)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.ScheduledSearch, Models.Receive.SearchResult<ScheduledSearchResult>>(scheduledSearch, this.searchEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Scheduled Instruction Api 'Search' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Deletes a scheduled instruction
        /// </summary>
        /// <param name="id">Scheduled Instruction id</param>
        /// <returns>True if deleted successfully. False otherwise.</returns>
        /// <remarks>Calls DELETE on apiRoot/ScheduledInstructions/{scheduleId}</remarks>
        public ApiCallResponse<bool> Delete(int id)
        {
            var apiCallResult = this.transportProxy.Delete(string.Format("{0}/{1}", this.endpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Scheduled Instruction Api 'Delete' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Cancels given schedule
        /// </summary>
        /// <param name="scheduleId">Id of the schedule to cancel</param>
        /// <returns>True if successful. False otherwise</returns>
        /// <remarks>Calls POST on apiRoot/ScheduledInstructions/{scheduleId}/Cancel</remarks>
        public ApiCallResponse<bool> CancelSchedule(int scheduleId)
        {
            var apiCallResult = this.transportProxy.PostEmpty<object>(string.Format(this.cancelEndpoint, scheduleId));
            if (!apiCallResult.Success)
            {
                this.LogError("Scheduled Instructions Api 'CancelSchedule' call failed.");
            }

            return new ApiCallResponse<bool>(apiCallResult.Success, apiCallResult.Success, apiCallResult.Errors, apiCallResult.ResponseStatusCode);
        }
    }
}
