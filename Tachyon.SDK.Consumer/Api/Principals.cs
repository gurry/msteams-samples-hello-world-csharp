namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Abstracts Principals controller of Consumer API
    /// </summary>
    public class Principals : ApiBase
    {
        private readonly string coreEndpoint = "Principals";
        private readonly string byIdEndpoint = "Principals/{0}";
        private readonly string getSecurityAdminsEndpoint = "Principals/PermissionsAdmins";
        private readonly string getForRoleEndpoint = "Principals/Role/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public Principals(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets all principals in the system
        /// </summary>
        /// <returns>Collection of principals</returns>
        /// <remarks>Calls GET on apiRoot/Principals</remarks>
        public ApiCallResponse<IEnumerable<Principal>> GetAll()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<Principal>>(this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Principals Api 'GetAll' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets a principal
        /// </summary>
        /// <param name="id">Id of the principal.</param>
        /// <returns>Principal object</returns>
        /// <remarks>Calls GET on apiRoot/Principals/{id}</remarks>
        public ApiCallResponse<Principal> Get(int id)
        {
            var apiCallResult = this.transportProxy.Get<Principal>(string.Format(this.byIdEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Principals Api 'Get' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets all permissions administrators
        /// </summary>
        /// <returns>Collection of principals that have permissions administrator role</returns>
        /// <remarks>Calls GET on apiRoot/Principals/PermissionsAdmins</remarks>
        public ApiCallResponse<IEnumerable<Principal>> GetAllPermissionsAdmins()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<Principal>>(this.getSecurityAdminsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Principals Api 'GetAllPermissionsAdmins' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets all principals that are assigned to given role
        /// </summary>
        /// <param name="roleId">Id of the role.</param>
        /// <returns>Collection of principals</returns>
        /// <remarks>Calls GET on apiRoot/Principals/Role/{roleId}</remarks>
        public ApiCallResponse<IEnumerable<Principal>> GetForRole(int roleId)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<Principal>>(string.Format(this.getForRoleEndpoint, roleId));
            if (!apiCallResult.Success)
            {
                this.LogError("Principals Api 'GetForRole' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Add a new principal
        /// </summary>
        /// <param name="principal">New principal to add</param>
        /// <returns>Added principal</returns>
        /// <remarks>Calls POST on apiRoot/Principals</remarks>
        public ApiCallResponse<Principal> Add(Principal principal)
        {
            var apiCallResult = this.transportProxy.Post<Principal, Principal>(principal, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Principals Api 'Add' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Update an existing principal
        /// </summary>
        /// <param name="principal">Principal to update</param>
        /// <returns>Updated principal</returns>
        /// <remarks>Calls PUT on apiRoot/Principals</remarks>
        public ApiCallResponse<Principal> Update(Principal principal)
        {
            var apiCallResult = this.transportProxy.Put<Principal, Principal>(principal, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Principals Api 'Update' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Deleted a principal
        /// </summary>
        /// <param name="id">Id of the principal to delete</param>
        /// <returns>True if deleted successfully. False otherwise</returns>
        /// <remarks>Calls DELETE on apiRoot/Principals/{id}</remarks>
        public ApiCallResponse<bool> Delete(int id)
        {
            var apiCallResult = this.transportProxy.Delete(string.Format(this.byIdEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Principals Api 'Delete' call failed");
            }

            return apiCallResult;
        }
    }
}
