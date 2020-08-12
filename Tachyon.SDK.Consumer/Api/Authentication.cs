namespace Tachyon.SDK.Consumer.Api
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;
    using Tachyon.SDK.Consumer.Models.Receive;
    using Tachyon.SDK.Consumer.Models.Send;

    /// <summary>
    /// Abstracts Authentication controller of Consumer API
    /// </summary>
    public class Authentication : ApiBase
    {
        private readonly string coreEndpoint = "Authentication";
        private const string tokenValidationEndpoint = "Authentication/Token";
        private const string instructionTokenValidationEndpoint = "Authentication/Instruction/Token";
        private const string scheduledInstructionTokenValidationEndpoint = "Authentication/ScheduledInstruction/Token";
        private const string deviceAuthEndpoint = "Authentication/AuthorizeDevice";
        private const string authRequestsEndpoint = "Authentication/AuthorizationRequests";
        private const string authRequestsByIdEndpoint = "Authentication/AuthorizationRequests/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public Authentication(ITransportProxy transportProxy, ILogProxy logProxy) : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Authenticates user with given credentials
        /// </summary>
        /// <param name="credentials">Credentials to check</param>
        /// <returns>Authentication data if it was successful</returns>
        /// <remarks>Calls POST on apiRoot/Authentication</remarks>
        public ApiCallResponse<Models.Receive.Authentication> AuthenticateCredentials(Credentials credentials)
        {
            var apiCallResult = this.transportProxy.Post<Credentials, Models.Receive.Authentication>(credentials, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Authentication Api 'AuthenticateCredentials' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Validates token issued for an instruction for the purpose of two-factor authentication
        /// </summary>
        /// <param name="instructionToken">an InstructionToken object which contains instruction id and token</param>
        /// <returns>Returns success or failure wrapped in an Authentication object</returns>
        /// <remarks>This method is obsolete since v3.1 to support scheduled instruction. Use <see cref="ValidateInstructionToken"/> instead.</remarks>
        [Obsolete("This method is obsolete since v3.1 to support scheduled instruction. Use ValidateInstructionToken instead.", true)]
        public ApiCallResponse<Models.Receive.Authentication> ValidateToken(InstructionToken instructionToken)
        {
            var apiCallResult = this.transportProxy.Post<InstructionToken, Models.Receive.Authentication>(instructionToken, tokenValidationEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Authentication Api 'ValidateToken' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Validates token issued for an instruction for the purpose of two-factor authentication
        /// </summary>
        /// <param name="authenticationToken">an <see cref="AuthenticationToken"/> object which contains instruction id and token</param>
        /// <returns>Returns success or failure wrapped in an <see cref="Models.Receive.Authentication"/> object</returns>
        /// <remarks>Calls POST on apiRoot/Authentication/Instruction/Token</remarks>
        public ApiCallResponse<Models.Receive.Authentication> ValidateInstructionToken(
            AuthenticationToken authenticationToken)
        {
            var apiCallResult =
                this.transportProxy.Post<AuthenticationToken, Models.Receive.Authentication>(
                    authenticationToken,
                    instructionTokenValidationEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError(string.Format("Authentication Api '{0}' call failed", MethodBase.GetCurrentMethod().Name));
            }

            return apiCallResult;
        }

        /// <summary>
        /// Validates token issued for a scheduled instruction for the purpose of two-factor authentication
        /// </summary>
        /// <param name="authenticationToken">an <see cref="AuthenticationToken"/> object which contains scheduled instruction id and token</param>
        /// <returns>Returns success or failure wrapped in an <see cref="Models.Receive.Authentication"/> object</returns>
        /// <remarks>Calls POST on apiRoot/Authentication/ScheduledInstruction/Token</remarks>
        public ApiCallResponse<Models.Receive.Authentication> ValidateScheduledInstructionToken(
            AuthenticationToken authenticationToken)
        {
            var apiCallResult =
                this.transportProxy.Post<AuthenticationToken, Models.Receive.Authentication>(
                    authenticationToken,
                    scheduledInstructionTokenValidationEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError(string.Format("Authentication Api '{0}' call failed", MethodBase.GetCurrentMethod().Name));
            }

            return apiCallResult;
        }

        /// <summary>
        /// Sends a response for a device authorization request
        /// </summary>
        /// <param name="result">Result</param>
        /// <returns>Nothing</returns>
        /// <remarks>Calls POST on apiRoot/Authentication/AuthorizeDevice</remarks>
        public ApiCallResponse<object> SendAuthorizationResponse(PushMessageAuthorizationResult result)
        {
            var apiCallResult = this.transportProxy.Post<PushMessageAuthorizationResult, object>(result, deviceAuthEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Authentication Api 'SendAuthorizationResponse' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns all pending device authorization requests
        /// </summary>
        /// <returns>List of authorization requests</returns>
        /// <remarks>Calls GET on apiRoot/Authentication/AuthorizationRequests</remarks>
        public ApiCallResponse<IEnumerable<PushMessageAuthorizationRequest>> GetPendingAuthorizationRequests()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<PushMessageAuthorizationRequest>>(authRequestsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Authentication Api 'GetPendingAuthorizationRequests' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns all device authorization requests, regardless of state
        /// </summary>
        /// <returns>List of authorization requests</returns>
        /// <remarks>Calls GET on apiRoot/Authentication/AuthorizationRequests?getAll=true</remarks>
        public ApiCallResponse<IEnumerable<PushMessageAuthorizationRequest>> GetAllAuthorizationRequests()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<PushMessageAuthorizationRequest>>(authRequestsEndpoint + "?getAll=true");
            if (!apiCallResult.Success)
            {
                this.LogError("Authentication Api 'GetAllAuthorizationRequests' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns specific authorization request by id
        /// </summary>
        /// <param name="id">Id of the request</param>
        /// <returns>Single authorization requests</returns>
        /// <remarks>Calls GET on apiRoot/Authentication/AuthorizationRequests/{id}</remarks>
        public ApiCallResponse<PushMessageAuthorizationRequest> GetAuthorizationRequest(int id)
        {
            var apiCallResult = this.transportProxy.Get<PushMessageAuthorizationRequest>(string.Format(authRequestsByIdEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Authentication Api 'GetAuthorizationRequest' call failed");
            }

            return apiCallResult;
        }
    }
}
