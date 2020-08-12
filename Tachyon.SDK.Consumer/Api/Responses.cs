namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Receive;

    /// <summary>
    /// Abstracts Responses controller of Consumer API
    /// </summary>
    public class Responses : ApiBase
    {
        private readonly string endpoint = "Responses/";
        private readonly string aggregatedEndpoint = "Responses/{0}/Aggregate/";
        private readonly string allFqdnsEndpoint = "Responses/{0}/UniqueFQDNs/";
        private readonly string startFromBeginningToken = "0;0";
        private readonly string processedEndpoint = "Responses/Processed/{0}";
        private readonly string specificValue = "Responses/{0}/Shard/{1}/Row/{2}/Column/{3}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public Responses(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets responses for given instruction
        /// </summary>
        /// <param name="id">Id of the instruction</param>
        /// <param name="request">Request object</param>
        /// <returns>Responses container with responses and token to be used as start token during next call</returns>
        /// <remarks>Calls POST on apiRoot/Responses/{id}</remarks>
        public ApiCallResponse<ResponsesContainer> GetResponses(int id, Models.Send.Responses request)
        {
            string endpointToCall = string.Concat(this.endpoint, id);
            var apiCallResult = this.transportProxy.Post<Models.Send.Responses, ResponsesContainer>(request, endpointToCall);
            if (!apiCallResult.Success)
            {
                LogError("Responses Api 'GetResponses' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets the FQDNs for the devices that responded to a given instruction
        /// </summary>
        /// <param name="id">Id of the instruction</param>
        /// <param name="request">Request object</param>
        /// <returns>Array of strings containing the FQDns</returns>
        /// <remarks>Calls POST on apiRoot/Responses/{id}/UniqueFQDNs/</remarks>
        public ApiCallResponse<IEnumerable<string>> GetDistinctFqdns(int id, Models.Send.Responses request)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.Responses, IEnumerable<string>>(request, string.Format(this.allFqdnsEndpoint, id));
            if (!apiCallResult.Success)
            {
                LogError("Responses Api 'GetDistinctFqdns' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets responses for given instruction
        /// </summary>
        /// <param name="id">Id of the instruction</param>
        /// <param name="startToken">start token</param>
        /// <param name="pageSize">how many responses to get</param>
        /// <returns>Responses container with responses and token to be used as start token during next call</returns>
        /// <remarks>Calls POST on apiRoot/Responses/{id}</remarks>
        public ApiCallResponse<ResponsesContainer> GetResponses(int id, string startToken, int pageSize)
        {
            string endpointToCall = string.Concat(this.endpoint, id);
            var payload = new Models.Send.Responses
            {
                Start = startToken,
                PageSize = pageSize
            };
            var apiCallResult = this.transportProxy.Post<Models.Send.Responses, ResponsesContainer>(payload, endpointToCall);
            if (!apiCallResult.Success)
            {
                LogError("Responses Api 'GetResponses' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets responses for given instruction starting from the first response
        /// </summary>
        /// <param name="id">Id of the instruction</param>
        /// <returns>Responses container with responses and token to be used as start token during next call</returns>
        /// <remarks>Calls POST on apiRoot/Responses/{id}</remarks>
        public ApiCallResponse<ResponsesContainer> GetAllResponses(int id)
        {
            return GetResponses(id, this.startFromBeginningToken, int.MaxValue);
        }

        /// <summary>
        /// Gets aggregated responses for given instruction
        /// </summary>
        /// <param name="id">Id of the instruction</param>
        /// <param name="request">Request object</param>
        /// <returns>Responses container with responses and token to be used as start token during next call</returns>
        /// <remarks>Calls POST on apiRoot/Responses/{id}</remarks>
        public ApiCallResponse<ResponsesContainer> GetAggregatedResponses(int id, Models.Send.Responses request)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.Responses, ResponsesContainer>(request, string.Format(this.aggregatedEndpoint, id));
            if (!apiCallResult.Success)
            {
                LogError("Responses Api 'GetAggregatedResponses' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets aggregated responses for given instruction
        /// </summary>
        /// <param name="id">Instruction Id</param>
        /// <param name="startToken">Start token</param>
        /// <param name="pageSize">Number of responses to get</param>
        /// <returns>Responses container with responses and token to be used as start token during next call</returns>
        /// <remarks>Calls POST on apiRoot/Responses/{id}/Aggregate/</remarks>
        public ApiCallResponse<ResponsesContainer> GetAggregatedResponses(int id, string startToken, int pageSize)
        {
            var payload = new Models.Send.Responses
            {
                Start = startToken,
                PageSize = pageSize
            };
            var apiCallResult = this.transportProxy.Post<Models.Send.Responses, ResponsesContainer>(payload, string.Format(this.aggregatedEndpoint, id));
            if (!apiCallResult.Success)
            {
                LogError("Responses Api 'GetAggregatedResponses' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets aggregated responses for given instruction
        /// </summary>
        /// <param name="id">Instruction Id</param>
        /// <returns>Responses container with responses and token to be used as start token during next call</returns>
        /// <remarks>Calls POST on apiRoot/Responses/{id}/Aggregate/</remarks>
        public ApiCallResponse<ResponsesContainer> GetAllAggregatedResponses(int id)
        {
            return GetAggregatedResponses(id, this.startFromBeginningToken, int.MaxValue);
        }

        /// <summary>
        /// Gets processed responses for given instruction.
        /// Only valid for instructions that define response templates. 
        /// </summary>
        /// <param name="id">Id of the instruction</param>
        /// <returns>Dictionary where the template id is the key and processed data is the value</returns>
        /// <remarks>This functionality was originally developed and intended for Tachyon Explorer UI</remarks>
        /// <remarks>Calls GET on apiRoot/Responses/Processed/{id}</remarks>
        public ApiCallResponse<Dictionary<string, object>> GetProcessedResponses(int id)
        {
            var apiCallResult = this.transportProxy.Get<Dictionary<string, object>>(string.Format(this.processedEndpoint, id));
            if (!apiCallResult.Success)
            {
                LogError("Responses Api 'GetProcessedResponses' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets a single specific value from a response.
        /// Will look for given instruction in a specific shard, then it will look for the row with given id and
        /// return the value of specified column form that response.
        /// This field should be used to retrieve CLOB fields.
        /// </summary>
        /// <param name="instructionId">In of the instruction whose response to look for</param>
        /// <param name="shardId">Id of the shard to get the response from</param>
        /// <param name="rowId">Id of the row (specific response for given instruction within given shard)</param>
        /// <param name="columnName">Name of the column whose value should be returned</param>
        /// <returns>Value of the column as CLR object class. This will usually be a string.</returns>
        /// <remarks>Calls GET on apiRoot/Responses/{instructionId:int}/Shard/{shardId:int}/Row/{rowId:int}/Column/{columnName}</remarks>
        public ApiCallResponse<ResponseValue> GetSpecificValue(int instructionId, int shardId, int rowId, string columnName)
        {
            var address = string.Format(this.specificValue, instructionId, shardId, rowId, columnName);
            var apiCallResult = this.transportProxy.Get<ResponseValue>(address);
            if (!apiCallResult.Success)
            {
                LogError("Responses Api 'GetSpecificValue' call failed.");
            }

            return apiCallResult;
        }
    }
}
