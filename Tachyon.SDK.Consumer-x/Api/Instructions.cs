namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;
    using Tachyon.SDK.Consumer.Models.Receive;

    /// <summary>
    /// Abstracts Instructions controller of Consumer API
    /// </summary>
    public class Instructions : ApiBase
    {
        private readonly string endpoint = "Instructions";
        private readonly string targetedEndpoint = "Instructions/Targeted";
        private readonly string instructionSearchEndpoint = "Instructions/search";
        private readonly string cancelEndpoint = "Instructions/{0}/cancel/{1}";
        private readonly string targetsByInstructionId = "Instructions/{0}/targetlist";
        private readonly string rerunInstructionEndpoint = "Instructions/{0}/rerun";
        private readonly string pendingInstructionsBasedOnInstructionSetEndpoint = "Instructions/InFlight/InstructionSet/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public Instructions(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Finds all instructions that match given parameters
        /// </summary>
        /// <param name="searchParameters">Search terms</param>
        /// <returns>collection of instructions</returns>
        /// <remarks>Calls apiRoot/Instructions/search</remarks>>
        public ApiCallResponse<SearchResult<InstructionSummary>> FindInstructions(Models.Send.Search searchParameters)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.Search, SearchResult<InstructionSummary>>(searchParameters, this.instructionSearchEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Instructions Api 'FindInstructions' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns a single instruction
        /// </summary>
        /// <param name="id">Id of the instruction to look for</param>
        /// <returns>Instruction object</returns>
        /// <remarks>Calls apiRoot/Instructions/{id}</remarks>
        public ApiCallResponse<Models.Receive.Instruction> GetInstruction(int id)
        {
            var idEndpoint = this.endpoint + "/" + id;
            var apiCallResult = this.transportProxy.Get<Models.Receive.Instruction>(idEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError(string.Format("Unable to retrieve instruction {0}", id));
            }

            return apiCallResult;
        }

        /// <summary>
        /// Sends a new instruction
        /// </summary>
        /// <param name="definitionName">Name of the instruction definition</param>
        /// <param name="parameters">Parameters for the instruction</param>
        /// <param name="InstructionTtl">Instruction time to live</param>
        /// <param name="ResponseTtl">Responses time to live</param>
        /// <returns>Newly created instruction if successful. Null otherwise</returns>
        /// <remarks>Calls apiRoot/Instructions</remarks>
        public ApiCallResponse<Models.Receive.Instruction> SendInstruction(string definitionName, Dictionary<string, string> parameters, int InstructionTtl, int ResponseTtl)
        {
            var postPayload = new Models.Send.Instruction
            {
                DefinitionName = definitionName,
                Export = false,
                KeepRaw = false,
                ParentInstructionId = null,
                InstructionTtlMinutes = InstructionTtl,
                ResponseTtlMinutes = ResponseTtl,
                Parameters = CreateParametersPayload(parameters)
            };
            var apiCallResult = this.transportProxy.Post<Models.Send.Instruction, Models.Receive.Instruction>(postPayload, this.endpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Instructions Api 'SendInstruction' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Sends a new instruction
        /// </summary>
        /// <param name="definitionId">Id of the instruction definition</param>
        /// <param name="parameters">Parameters for the instruction</param>
        /// <param name="InstructionTtl">Instruction time to live</param>
        /// <param name="ResponseTtl">Responses time to live</param>
        /// <returns>Newly created instruction if successful. Null otherwise</returns>
        /// <remarks>Calls apiRoot/Instructions</remarks>
        public ApiCallResponse<Models.Receive.Instruction> SendInstruction(int definitionId, Dictionary<string, string> parameters, int InstructionTtl, int ResponseTtl)
        {
            var postPayload = new Models.Send.Instruction
            {
                DefinitionId = definitionId,
                Export = false,
                KeepRaw = false,
                ParentInstructionId = null,
                InstructionTtlMinutes = InstructionTtl,
                ResponseTtlMinutes = ResponseTtl,
                Parameters = CreateParametersPayload(parameters)
            };
            var apiCallResult = this.transportProxy.Post<Models.Send.Instruction, Models.Receive.Instruction>(postPayload, this.endpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Instructions Api 'SendInstruction' call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Sends a new instruction
        /// </summary>
        /// <param name="instruction">Instruction to send</param>
        /// <returns>Newly created instruction if successful. Null otherwise</returns>
        /// <remarks>Calls apiRoot/Instructions</remarks>
        public ApiCallResponse<Models.Receive.Instruction> SendInstruction(Models.Send.Instruction instruction)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.Instruction, Models.Receive.Instruction>(instruction, this.endpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Instructions Api 'SendInstruction' call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Sends a new targeted definition
        /// </summary>
        /// <param name="definitionName">Name of the instruction definition</param>
        /// <param name="parameters">Parameters for the instruction</param>
        /// <param name="InstructionTtl">Instruction time to live</param>
        /// <param name="ResponseTtl">Responses time to live</param>
        /// <param name="devices">List of endpoint fqdn to send instruction to</param>
        /// <returns>Newly created instruction if successful. Null otherwise</returns>
        /// <remarks>Calls apiRoot/Instructions/Targeted</remarks>
        public ApiCallResponse<Models.Receive.Instruction> SendTargetedInstruction(string definitionName, Dictionary<string, string> parameters, int InstructionTtl, int ResponseTtl, List<string> devices)
        {
            var postPayload = new Models.Send.Instruction
            {
                DefinitionName = definitionName,
                Export = false,
                KeepRaw = false,
                ParentInstructionId = null,
                InstructionTtlMinutes = InstructionTtl,
                ResponseTtlMinutes = ResponseTtl,
                Parameters = CreateParametersPayload(parameters),
                Devices = devices
            };
            var apiCallResult = this.transportProxy.Post<Models.Send.Instruction, Models.Receive.Instruction>(postPayload, this.targetedEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Instructions Api 'SendTargetedInstruction' call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Sends a new targeted definition
        /// </summary>
        /// <param name="definitionId">Id of the instruction definition</param>
        /// <param name="parameters">Parameters for the instruction</param>
        /// <param name="InstructionTtl">Instruction time to live</param>
        /// <param name="ResponseTtl">Responses time to live</param>
        /// <param name="devices">List of endpoint fqdn to send instruction to</param>
        /// <returns>Newly created instruction if successful. Null otherwise</returns>
        /// <remarks>Calls apiRoot/Instructions</remarks>
        public ApiCallResponse<Models.Receive.Instruction> SendTargetedInstruction(int definitionId, Dictionary<string, string> parameters, int InstructionTtl, int ResponseTtl, List<string> devices)
        {
            var postPayload = new Models.Send.Instruction
            {
                DefinitionId = definitionId,
                Export = false,
                KeepRaw = false,
                ParentInstructionId = null,
                InstructionTtlMinutes = InstructionTtl,
                ResponseTtlMinutes = ResponseTtl,
                Parameters = CreateParametersPayload(parameters),
                Devices = devices
            };
            var apiCallResult = this.transportProxy.Post<Models.Send.Instruction, Models.Receive.Instruction>(postPayload, this.targetedEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Instructions Api 'SendTargetedInstruction' call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Sends a new targeted definition
        /// </summary>
        /// <param name="instruction">Instruction to be sent</param>
        /// <returns>Newly created instruction if successful. Null otherwise</returns>
        /// <remarks>Calls apiRoot/Instructions</remarks>
        public ApiCallResponse<Models.Receive.Instruction> SendTargetedInstruction(Models.Send.Instruction instruction)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.Instruction, Models.Receive.Instruction>(instruction, this.targetedEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Instructions Api 'SendTargetedInstruction' call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Cancels given instruction
        /// </summary>
        /// <param name="instructionId">Id of the instruction to cancel</param>
        /// <param name="keepData">Boolean flag to indicate if data already received for this instruction should be kept or deleted</param>
        /// <returns>True if successful. False otherwise</returns>
        /// <remarks>Calls apiRoot/Instructions/{instructionId}/Cancel/{keepData}</remarks>
        public ApiCallResponse<bool> CancelInstruction(int instructionId, bool keepData)
        {
            var apiCallResult = this.transportProxy.PostEmpty<object>(string.Format(this.cancelEndpoint, instructionId, keepData));
            if (!apiCallResult.Success)
            {
                this.LogError("Instructions Api 'CancelInstruction' call failed.");
            }

            return new ApiCallResponse<bool>(apiCallResult.Success, apiCallResult.Success, apiCallResult.Errors, apiCallResult.ResponseStatusCode);
        }

        /// <summary>
        /// Re-runs given instruction
        /// </summary>
        /// <param name="instructionId">Id of the instruction to be rerun</param>
        /// <returns>Newly created instruction if successful. Null otherwise</returns>
        /// <remarks>Calls apiRoot/Instructions/{instructionId}/rerun</remarks>
        public ApiCallResponse<Models.Receive.Instruction> RerunInstruction(int instructionId)
        {
            var apiCallResult = this.transportProxy.PostEmpty<Models.Receive.Instruction>(string.Format(this.rerunInstructionEndpoint, instructionId));
            if (!apiCallResult.Success)
            {
                this.LogError("Instructions Api 'RerunInstruction' call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Gets the list of target Fqdns for a given instruction
        /// </summary>
        /// <param name="instructionId">Id of an instruction you wish to obtain targets for</param>
        /// <returns>Gets the list of fqdns given instruction was sent to</returns>
        /// <remarks>Calls GET on apiRoot/Instructions/{instructionId}/targetlist</remarks>
        public ApiCallResponse<IEnumerable<string>> GetTargetsByInstructionId(int instructionId)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<string>>(string.Format(this.targetsByInstructionId, instructionId));
            if (!apiCallResult.Success)
            {
                this.LogError("Instructions Api 'GetTargetsByInstructionId' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Finds In-Flight instructions from an Instruction Set
        /// </summary>
        /// <param name="instructionSetId">The ID of the Instruction Set</param>
        /// <returns>collection of instructions</returns>
        /// <remarks>Calls apiRoot/Instructions/InFlight/InstructionSet/{instructionSetId}</remarks>>
        public ApiCallResponse<SearchResult<InstructionSummary>> GetPendingInstructionsFromInstructionSet(int instructionSetId)
        {
            var idEndpoint = string.Format(this.pendingInstructionsBasedOnInstructionSetEndpoint, instructionSetId);
            var apiCallResult = this.transportProxy.Get<SearchResult<InstructionSummary>>(idEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Instructions Api 'GetPendingInstructionsFromInstructionSet' call failed.");
            }

            return apiCallResult;
        }

        private List<InstructionParameter> CreateParametersPayload(Dictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return null;
            }

            List<InstructionParameter> retVal = new List<InstructionParameter>();
            foreach (var parameter in parameters)
            {
                retVal.Add(new InstructionParameter
                {
                    Name = parameter.Key,
                    Value = parameter.Value
                });
            }
            return retVal;
        }
    }
}
