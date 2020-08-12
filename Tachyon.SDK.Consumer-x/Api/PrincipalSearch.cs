namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;
    using Tachyon.SDK.Consumer.Models.Receive;
    using Tachyon.SDK.Consumer.Tools;

    /// <summary>
    /// Abstracts PrincipalSearch controller of Consumer API
    /// </summary>
    public class PrincipalSearch : ApiBase
    {
        private readonly string searchEndpoint = "PrincipalSearch/{0}";
        private readonly string getGroupMembersEndpoint = "PrincipalSearch/GetMembers/{0}";
        private readonly string getUserDisplayNameEndpoint = "PrincipalSearch/DisplayName/{0}";
        private readonly string getCurrentlyLoggedInUser = "PrincipalSearch/whoami";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public PrincipalSearch(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Searches for principals in AD based on string in the search term
        /// </summary>
        /// <param name="searchTerm">String to search for</param>
        /// <returns>Collection of principals matching the search term</returns>
        /// <remarks>Calls GET on apiRoot/PrincipalSearch/{searchTerm}</remarks>
        public ApiCallResponse<IEnumerable<PrincipalSearchItem>> SearchForPrincipals(string searchTerm)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<PrincipalSearchItem>>(string.Format(this.searchEndpoint, searchTerm));
            if (!apiCallResult.Success)
            {
                this.LogError("PrincipalSearch Api 'SearchForPrincipals' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets members of an AD group
        /// </summary>
        /// <param name="groupName">Name of the group</param>
        /// <returns>Collection of principals that belong to given group</returns>
        /// <remarks>Calls GET on apiRoot/PrincipalSearch/GetMembers/{groupName}</remarks>
        public ApiCallResponse<IEnumerable<PrincipalSearchItem>> GetGroupMembers(string groupName)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<PrincipalSearchItem>>(string.Format(this.getGroupMembersEndpoint, Base64Utility.ToUrlSafeBase64(groupName)));
            if (!apiCallResult.Success)
            {
                this.LogError("PrincipalSearch Api 'GetGroupMembers' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets display name for a user
        /// </summary>
        /// <param name="accountName">Account name</param>
        /// <returns>Display name</returns>
        /// <remarks>Calls GET on apiRoot/PrincipalSearch/DisplayName/{accountName}</remarks>
        public ApiCallResponse<string> GetUserDisplayName(string accountName)
        {
            var apiCallResult = this.transportProxy.Get<string>(string.Format(this.getUserDisplayNameEndpoint, Base64Utility.ToUrlSafeBase64(accountName)));
            if (!apiCallResult.Success)
            {
                this.LogError("PrincipalSearch Api 'GetUserDisplayName' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets currently logged in user info
        /// </summary>
        /// <returns>Currently logged in user info</returns>
        /// <remarks>Calls GET on apiRoot/PrincipalSearch/whoami</remarks>
        public ApiCallResponse<User> GetCurrentlyLoggedInUser()
        {
            var apiCallResult = this.transportProxy.Get<User>(this.getCurrentlyLoggedInUser);
            if (!apiCallResult.Success)
            {
                this.LogError("PrincipalSearch Api 'CurrentlyLoggedInUser' call failed");
            }

            return apiCallResult;
        }
    }
}
