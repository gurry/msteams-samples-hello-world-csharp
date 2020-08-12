namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Abstracts Roles controller of Consumer API
    /// </summary>
    public class Roles : ApiBase
    {
        private readonly string coreEndpoint = "Roles";
        private readonly string byIdEndpoint = "Roles/{0}";
        private readonly string getForPrincipal = "Roles/Principal/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public Roles(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets all the roles in the system
        /// </summary>
        /// <returns>Collection of roles</returns>
        /// <remarks>Calls GET on apiRoot/Roles</remarks>
        public ApiCallResponse<IEnumerable<Role>> GetAll()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<Role>>(this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Roles Api 'GetAll' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets a specific role
        /// </summary>
        /// <param name="id">Id of the role</param>
        /// <returns>Role object if found. Null otherwise</returns>
        /// <remarks>Calls GET on apiRoot/Roles/{id}</remarks>
        public ApiCallResponse<Role> Get(int id)
        {
            var apiCallResult = this.transportProxy.Get<Role>(string.Format(this.byIdEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Roles Api 'Get' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets all roles for a specific principal
        /// </summary>
        /// <param name="principalId">Principal Id</param>
        /// <returns>List of roles given principal has</returns>
        /// <remarks>Calls GET on apiRoot/Roles/Principal/{principalId}</remarks>
        public ApiCallResponse<IEnumerable<PrincipalRole>> GetForPrincipal(int principalId)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<PrincipalRole>>(string.Format(this.getForPrincipal, principalId));
            if (!apiCallResult.Success)
            {
                this.LogError("Roles Api 'GetForPrincipal' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Adds a new role
        /// </summary>
        /// <param name="role">Role to add</param>
        /// <returns>Added role</returns>
        /// <remarks>Calls POST on apiRoot/Roles</remarks>
        public ApiCallResponse<Role> Add(Role role)
        {
            var apiCallResult = this.transportProxy.Post<Role, Role>(role, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Roles Api 'Add' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Updates an existing role
        /// </summary>
        /// <param name="role">Role to update</param>
        /// <returns>Updated role</returns>
        /// <remarks>Calls PUT on apiRoot/Roles</remarks>
        public ApiCallResponse<Role> Update(Role role)
        {
            var apiCallResult = this.transportProxy.Put<Role, Role>(role, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Roles Api 'Update' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Deletes a role
        /// </summary>
        /// <param name="id">Role id</param>
        /// <returns>True if deleted successfully. False otherwise</returns>
        /// <remarks>Calls DELETE on apiRoot/Roles/{id}</remarks>
        public ApiCallResponse<bool> Delete(int id)
        {
            var apiCallResult = this.transportProxy.Delete(string.Format(this.byIdEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Roles Api 'Delete' call failed");
            }

            return apiCallResult;
        }
    }
}
