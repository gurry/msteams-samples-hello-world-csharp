using Tachyon.SDK.Consumer.Models.Receive;
using Tachyon.SDK.Consumer.Models.Send;

namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Abstracts PushMessageUsers controller of Consumer API
    /// </summary>
    public class PushMessageUsers : ApiBase
    {
        private readonly string coreEndpoint = "PushMessageUsers";
        private readonly string byIdEndpoint = "PushMessageUsers/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public PushMessageUsers(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Returns all users registered for push message notifications
        /// </summary>
        /// <returns>Collection of all users</returns>
        /// <remarks>Calls GET on apiRoot/PushMessageUsers</remarks>
        public ApiCallResponse<IEnumerable<PushMessageUser>> GetAll()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<PushMessageUser>>(this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Push Message Users Api 'GetAll' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns a single user registered for push message notifications
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <returns>User</returns>
        /// <remarks>Calls GET on apiRoot/PushMessageUsers/{id}</remarks>
        public ApiCallResponse<PushMessageUser> Get(int id)
        {
            string endpoint = string.Format(this.byIdEndpoint, id);
            var apiCallResult = this.transportProxy.Get<PushMessageUser>(endpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Push Message Users Api 'Get' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Updates selected user. Object passed in must have Id of a valid (existing) user
        /// </summary>
        /// <param name="user">User to update</param>
        /// <returns>Updated user</returns>
        /// <remarks>Calls PUT on apiRoot/PushMessageUsers</remarks>
        public ApiCallResponse<PushMessageUser> Update(PushMessageUser user)
        {
            var apiCallResult = this.transportProxy.Put<PushMessageUser, PushMessageUser>(user, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Push Message Users Api 'Update' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Searches for users that fulfil given parameters.
        /// </summary>
        /// <param name="searchParameters">Search parameters</param>
        /// <returns>Collection of users that match the parameters requested</returns>
        /// <remarks>Calls POST on apiRoot/PushMessageUsers</remarks>
        public ApiCallResponse<SearchResult<PushMessageUser>> FindUsers(Search searchParameters)
        {
            var apiCallResult = this.transportProxy.Post<Search, SearchResult<PushMessageUser>>(searchParameters, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Push Message Users Api 'FindUsers' call failed.");
            }

            return apiCallResult;
        }
    }
}
