namespace Tachyon.SDK.Consumer.Api
{
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;
    using Tachyon.SDK.Consumer.Models.Send;

    /// <summary>
    /// Abstracts PrincipalRoles controller of Consumer API
    /// </summary>
    public class PrincipalRoles : ApiBase
    {
        private readonly string coreEndpoint = "PrincipalRoles";
        private readonly string saveEndpoint = "PrincipalRoles/SavePrincipal";
        private readonly string deleteEndpoint = "PrincipalRoles/role/{0}/principal/{1}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public PrincipalRoles(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Links a role with a principal.
        /// </summary>
        /// <param name="principalRoles">Object describing the link</param>
        /// <returns>Added link</returns>
        /// <remarks>Calls POST on apiRoot/PrincipalRoles</remarks>
        public ApiCallResponse<PrincipalRole> Add(PrincipalRole principalRoles)
        {
            var apiCallResult = this.transportProxy.Post<PrincipalRole, PrincipalRole>(principalRoles, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("PrincipalRoles Api 'Add' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Use to add or update roles for a principle
        /// </summary>
        /// <param name="principalRoles">Principal role object containing all the roles you wish the principal to have.
        /// After the update the principal will have only the roles you specify.
        /// Any roles it might have had previously which were not the part of RoleIdList will no longer be linked to the principal.</param>
        /// <returns>This API call does not return anything. Only success indicator is available</returns>
        /// <remarks>Calls POST on apiRoot/PrincipalRoles/SavePrincipal</remarks>
        public ApiCallResponse<object> AddOrUpdate(PrincipalNewRoles principalRoles)
        {
            var apiCallResult = this.transportProxy.Post<PrincipalNewRoles, object>(principalRoles, this.saveEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("PrincipalRoles Api 'AddOrUpdate' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Removes a role from principal by deleting a link between a principal and a role
        /// </summary>
        /// <param name="roleId">Role Id</param>
        /// <param name="principalId">Principal Id</param>
        /// <returns>True if deleted successfully. False otherwise</returns>
        /// <remarks>Calls DELETE on apiRoot/PrincipalRoles/role/{roleId}/principal/{principalId}</remarks>
        public ApiCallResponse<bool> Delete(int roleId, int principalId)
        {
            var apiCallReturn = this.transportProxy.Delete(string.Format(this.deleteEndpoint, roleId, principalId));
            if (!apiCallReturn.Success)
            {
                this.LogError("PrincipalRoles Api 'Delete' call failed");
            }

            return apiCallReturn;
        }
    }
}
