using System.Collections.Generic;

namespace Tachyon.SDK.Consumer.Api
{
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Receive;

    /// <summary>
    /// Abstracts ResponseErrors controller of Consumer API
    /// </summary>
    public class ResponseErrors : ApiBase
    {
        private readonly string endpoint = "ResponseErrors/";
        private readonly string aggregatedEndpoint = "ResponseErrors/{0}/Aggregate/";
        private readonly string allFqdnsEndpoint = "ResponseErrors/{0}/UniqueFQDNs/";
        private readonly string startFromBeginningToken = "0;0";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public ResponseErrors(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets errors and other responses for given instruction
        /// </summary>
        /// <param name="id">Id of the instruction</param>
        /// <param name="request">Request object</param>
        /// <returns>Responses container with responses and token to be used as start token during next call</returns>
        /// <remarks>Calls POST on apiRoot/ResponseErrors/{id}</remarks>
        public ApiCallResponse<ResponseErrorContainer> GetResponseErrors(int id, Models.Send.Responses request)
        {
            string endpointToCall = string.Concat(this.endpoint, id);
            var apiCallResult = this.transportProxy.Post<Models.Send.Responses, ResponseErrorContainer>(request, endpointToCall);
            if (!apiCallResult.Success)
            {
                LogError("ResponseErrors Api 'GetResponses' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets the FQDNs for the devices that reported errors and other responses for given instruction
        /// </summary>
        /// <param name="id">Id of the instruction</param>
        /// <param name="request">Request object</param>
        /// <returns>Array of strings containing the FQDns</returns>
        /// <remarks>Calls POST on apiRoot/ResponseErrors/{id}/UniqueFQDNs/</remarks>
        public ApiCallResponse<IEnumerable<string>> GetDistinctFqdns(int id, Models.Send.Responses request)
        {
            string endpointToCall = string.Format(this.allFqdnsEndpoint, id);
            var apiCallResult = this.transportProxy.Post<Models.Send.Responses, IEnumerable<string>>(request, endpointToCall);
            if (!apiCallResult.Success)
            {
                LogError("ResponseErrors Api 'GetDistinctFqdns' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets errors and other responses for given instruction
        /// </summary>
        /// <param name="id">Id of the instruction</param>
        /// <param name="startToken">start token</param>
        /// <param name="pageSize">how many responses to get</param>
        /// <returns>Responses container with responses and token to be used as start token during next call</returns>
        /// <remarks>Calls POST on apiRoot/Responses/{id}</remarks>
        public ApiCallResponse<ResponseErrorContainer> GetResponseErrors(int id, string startToken, int pageSize)
        {
            string endpointToCall = string.Concat(this.endpoint, id);
            var payload = new Models.Send.Responses
            {
                Start = startToken,
                PageSize = pageSize
            };
            var apiCallResult = this.transportProxy.Post<Models.Send.Responses, ResponseErrorContainer>(payload, endpointToCall);
            if (!apiCallResult.Success)
            {
                LogError("ResponseErrors Api 'GetResponseErrors' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets errors and other responses for given instruction starting from the first response
        /// </summary>
        /// <param name="id">Id of the instruction</param>
        /// <returns>Responses container with responses and token to be used as start token during next call</returns>
        /// <remarks>Calls POST on apiRoot/Responses/{id}</remarks>
        public ApiCallResponse<ResponseErrorContainer> GetAllResponseErrors(int id)
        {
            return GetResponseErrors(id, this.startFromBeginningToken, int.MaxValue);
        }

        /// <summary>
        /// Gets aggregated errors and other responses for given instruction
        /// </summary>
        /// <param name="id">Id of the instruction</param>
        /// <param name="request">Request object</param>
        /// <returns>Responses container with responses and token to be used as start token during next call</returns>
        /// <remarks>Calls POST on apiRoot/Responses/{id}</remarks>
        public ApiCallResponse<IEnumerable<ResponseAggregatedError>> GetAggregatedResponseErrors(int id, Models.Send.Responses request)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.Responses, IEnumerable<ResponseAggregatedError>>(request, string.Format(this.aggregatedEndpoint, id));
            if (!apiCallResult.Success)
            {
                LogError("ResponseErrors Api 'GetAggregatedResponseErrors' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets aggregated errors and other responses for given instruction
        /// </summary>
        /// <param name="id">Instruction Id</param>
        /// <param name="startToken">Start token</param>
        /// <param name="pageSize">Number of responses to get</param>
        /// <returns>Responses container with responses and token to be used as start token during next call</returns>
        /// <remarks>Calls POST on apiRoot/Responses/{id}/Aggregate/</remarks>
        public ApiCallResponse<IEnumerable<ResponseAggregatedError>> GetAggregatedResponseErrors(int id, string startToken, int pageSize)
        {
            var payload = new Models.Send.Responses
            {
                Start = startToken,
                PageSize = pageSize
            };
            var apiCallResult = this.transportProxy.Post<Models.Send.Responses, IEnumerable<ResponseAggregatedError>>(payload, string.Format(this.aggregatedEndpoint, id));
            if (!apiCallResult.Success)
            {
                LogError("ResponseErrors Api 'GetAggregatedResponseErrors' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets aggregated errors and other responses for given instruction
        /// </summary>
        /// <param name="id">Instruction Id</param>
        /// <returns>Responses container with responses and token to be used as start token during next call</returns>
        /// <remarks>Calls POST on apiRoot/Responses/{id}/Aggregate/</remarks>
        public ApiCallResponse<IEnumerable<ResponseAggregatedError>> GetAllAggregatedResponseErrors(int id)
        {
            return GetAggregatedResponseErrors(id, this.startFromBeginningToken, int.MaxValue);
        }
    }
}
