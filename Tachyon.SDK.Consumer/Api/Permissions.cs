namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;
    using Tachyon.SDK.Consumer.Models.Receive;
    using Tachyon.SDK.Consumer.Tools;

    /// <summary>
    /// Abstracts Permissions controller of Consumer API
    /// </summary>
    public class Permissions : ApiBase
    {
        private readonly string getByIdEndpoint = "Permissions/{0}";
        private readonly string getForRoleEndpoint = "Permissions/Role/{0}";
        private readonly string getForRoleAndTypeEndpoint = "Permissions/Role/{0}/Type/{1}";
        private readonly string getForRoleAndTypeAndInstanceEndpoint = "Permissions/Role/{0}/Type/{1}/{2}";
        private readonly string getForPrincipalEndpoint = "Permissions/Principal/{0}";
        private readonly string getForPrincipalAndTypeEndpoint = "Permissions/Principal/{0}/Type/{1}";
        private readonly string getForPrincipalAndTypeAndInstanceEndpoint = "Permissions/Principal/{0}/Type/{1}/{2}";
        private readonly string getForSecurableLongEndpoint = "Permissions/Securable/{0}/{1}";
        private readonly string getForSecurableShortEndpoint = "Permissions/Securable/{0}";
        private readonly string getIsRbacEnabledEndpoint = "Permissions/rbac/enabled";
        private readonly string getInstructionAccessEndpoint = "Permissions/InstructionAccess/{0}";
        private readonly string getForInstructionSetEndpoint = "Permissions/InstructionSet/{0}";
        private readonly string getIsAuthorizedFullEndpoint = "Permissions/Type/{0}/Operation/{1}/{2}/{3}";
        private readonly string getIsAuthorizedShortEndpoint = "Permissions/Type/{0}/Operation/{1}";
        private readonly string coreEndpoint = "Permissions";
        private readonly string singleEndpoint = "Permissions/single";
        private readonly string refreshCacheEndpoint = "Permissions/refresh";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public Permissions(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }
        /// <summary>
        /// Gets a specific permission
        /// </summary>
        /// <param name="id">Permission Id</param>
        /// <returns>Permissions</returns>
        /// <remarks>Calls GET on apiRoot/Permissions/{id}</remarks>
        public ApiCallResponse<Permission> GetById(int id)
        {
            var apiCallResult = this.transportProxy.Get<Permission>(string.Format(this.getByIdEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'GetById' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets permissions for given role
        /// </summary>
        /// <param name="roleId">Role Id</param>
        /// <returns>Permissions</returns>
        /// <remarks>Calls GET on apiRoot/Permissions/Role/{roleId}</remarks>
        public ApiCallResponse<IEnumerable<AggregatedPermission>> GetForRole(int roleId)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<AggregatedPermission>>(string.Format(this.getForRoleEndpoint, roleId));
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'GetForRole' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets permissions given role has to given securable type
        /// </summary>
        /// <param name="roleId">Role Id</param>
        /// <param name="securableTypeName">Securable type name</param>
        /// <returns>Permissions</returns>
        /// <remarks>Calls GET on apiRoot/Permissions/Role/{roleId}/Type/{securableTypeName}</remarks>
        public ApiCallResponse<IEnumerable<AggregatedPermission>> GetForRoleAndType(int roleId, string securableTypeName)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<AggregatedPermission>>(string.Format(this.getForRoleAndTypeEndpoint, roleId, securableTypeName));
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'GetForRoleAndType' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets permissions given role has to specific instance of given securable type
        /// </summary>
        /// <param name="roleId">Role Id</param>
        /// <param name="securableTypeName">Securable type name</param>
        /// <param name="instanceId">Id of the instance of the securable type object</param>
        /// <returns>Permissions</returns>
        /// <remarks>Calls GET on apiRoot/Permissions/Role/{roleId}/Type/{securableTypeName}/{instanceId}</remarks>
        public ApiCallResponse<IEnumerable<AggregatedPermission>> GetForRoleAndTypeAndInstance(int roleId, string securableTypeName, int instanceId)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<AggregatedPermission>>(string.Format(this.getForRoleAndTypeAndInstanceEndpoint, roleId, securableTypeName, instanceId));
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'GetForRoleAndTypeAndInstance' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets permissions given user / principal has
        /// </summary>
        /// <param name="accountName">Principal name</param>
        /// <returns>Permissions</returns>
        /// <remarks>Calls GET on apiRoot/Permissions/Principal/{accountName}</remarks>
        public ApiCallResponse<IEnumerable<AggregatedPermission>> GetForPrincipal(string accountName)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<AggregatedPermission>>(string.Format(this.getForPrincipalEndpoint, Base64Utility.ToUrlSafeBase64(accountName)));
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'GetForPrincipal' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets permissions given user / principal has for specific securable type
        /// </summary>
        /// <param name="accountName">Principal name</param>
        /// <param name="securableTypeName">Name of the securable type</param>
        /// <returns>Permissions</returns>
        /// <remarks>Calls GET on apiRoot/Permissions/Principal/{accountName}/Type/{securableTypeName}</remarks>
        public ApiCallResponse<IEnumerable<AggregatedPermission>> GetForPrincipalAndType(string accountName, string securableTypeName)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<AggregatedPermission>>(string.Format(this.getForPrincipalAndTypeEndpoint, Base64Utility.ToUrlSafeBase64(accountName), securableTypeName));
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'GetForPrincipalAndSpecificType' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets permissions given user / principal has for specific instance of a specific securable type
        /// </summary>
        /// <param name="accountName">Principal name</param>
        /// <param name="securableTypeName">Name of the securable type</param>
        /// <param name="instanceId">Id of the instance of a securable object</param>
        /// <returns>Permissions</returns>
        /// <remarks>Calls GET on apiRoot/Permissions/Principal/{accountName}/Type/{securableTypeName}/{instanceId}</remarks>
        public ApiCallResponse<IEnumerable<AggregatedPermission>> GetForPrincipalAndTypeAndInstance(string accountName, string securableTypeName, int instanceId)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<AggregatedPermission>>(string.Format(this.getForPrincipalAndTypeAndInstanceEndpoint, accountName, securableTypeName, instanceId));
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'GetForPrincipalAndSpecificTypeAndInstance' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets permissions for specific instance of a specific securable type
        /// </summary>
        /// <param name="typeId">Id of the securable type</param>
        /// <param name="instanceId">Id of the instance of a securable object</param>
        /// <returns>Permissions</returns>
        /// <remarks>Calls GET on apiRoot/Permissions/Securable/{typeId}/{instanceId:int?}</remarks>
        public ApiCallResponse<IEnumerable<AggregatedPermission>> GetForSecurableTypeAndInstance(int typeId, int instanceId)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<AggregatedPermission>>(string.Format(this.getForSecurableLongEndpoint, typeId, instanceId));
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'GetForSecurableTypeAndInstance' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets permissions for specified securable type
        /// </summary>
        /// <param name="typeId">Id of the securable type</param>
        /// <returns>Permissions</returns>
        /// <remarks>Calls GET on apiRoot/Permissions/Securable/{typeId}</remarks>
        public ApiCallResponse<IEnumerable<AggregatedPermission>> GetForSecurableType(int typeId)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<AggregatedPermission>>(string.Format(this.getForSecurableShortEndpoint, typeId));
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'GetForSecurableType' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Checks if role based access is enabled
        /// </summary>
        /// <returns>True if RBAC is enabled. False otherwise</returns>
        /// <remarks>Calls GET on apiRoot/Permissions/rbac/enabled</remarks>
        public ApiCallResponse<string> IsRbacEnabled()
        {
            var apiCallResult = this.transportProxy.Get<string>(this.getIsRbacEnabledEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'IsRbacEnabled' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Checks if calling user / principal can view specific instruction
        /// </summary>
        /// <param name="id">Id of the instruction</param>
        /// <returns>"True" if the user / principal can view that instruction</returns>
        /// <remarks>Calls GET on apiRoot/Permissions/InstructionAccess/{Id}</remarks>
        public ApiCallResponse<string> CanIAccessInstruction(int id)
        {
            var apiCallResult = this.transportProxy.Get<string>(string.Format(this.getInstructionAccessEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'CanIAccessInstruction' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Check if calling user / principal has specific permission on any of the product packs
        /// </summary>
        /// <param name="operation">Operation to which permission you want to check</param>
        /// <returns>True if the user / principal has the permission</returns>
        /// <remarks>Calls GET on apiRoot/Permissions/InstructionSet/{operation}</remarks>
        public ApiCallResponse<bool> DoIHaveSpecificPermissionOnAnyInstructionSet(string operation)
        {
            var apiCallResult = this.transportProxy.Get<bool>(string.Format(this.getForInstructionSetEndpoint, operation));
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'DoIHaveSpecificPermissionOnAnyInstructionSet' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Check if calling user / principal is authorized to perform given operation for given securable type
        /// </summary>
        /// <param name="securableType">Securable type name</param>
        /// <param name="operation">Operation on securable type</param>
        /// <param name="idProperty">Id of the property</param>
        /// <param name="id">Instance Id</param>
        /// <returns>True if calling user / principal is authorized. False otherwise</returns>
        /// <remarks>Calls GET on apiRoot/Permissions/Type/{securableType}/Operation/{operation}/{idProperty}/{id}</remarks>
        public ApiCallResponse<bool> AmIAuthorized(string securableType, string operation, string idProperty, int id)
        {
            var apiCallResult = this.transportProxy.Get<bool>(string.Format(this.getIsAuthorizedFullEndpoint, securableType, operation, idProperty, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'AmIAuthorized' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Check if calling user / principal is authorized to perform given operation for given securable type
        /// </summary>
        /// <param name="securableType">Securable type name</param>
        /// <param name="operation">Operation on securable type</param>
        /// <returns>True if calling user / principal is authorized. False otherwise</returns>
        /// <remarks>Calls GET on apiRoot/Permissions/Type/{securableType}/Operation/{operation}</remarks>
        public ApiCallResponse<bool> AmIAuthorized(string securableType, string operation)
        {
            var apiCallResult = this.transportProxy.Get<bool>(string.Format(this.getIsAuthorizedShortEndpoint, securableType, operation));
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'AmIAuthorized' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Adds, updates or deleted a number of permissions
        /// </summary>
        /// <param name="permissions">Permission to add / update / delete</param>
        /// <returns>Modified / added / deleted permission</returns>
        /// <remarks>Calls POST on apiRoot/Permissions</remarks>
        public ApiCallResponse<IEnumerable<AggregatedPermission>> AddOrUpdate(PermissionsSaveContainer permissions)
        {
            var apiCallResult = this.transportProxy.Post<PermissionsSaveContainer, IEnumerable<AggregatedPermission>>(permissions, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'AddOrUpdate' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Adds a new permission
        /// </summary>
        /// <param name="permission">Permission to add</param>
        /// <returns>Added permission</returns>
        /// <remarks>Calls POST on apiRoot/Permissions/single</remarks>
        public ApiCallResponse<Permission> AddNew(Permission permission)
        {
            var apiCallResult = this.transportProxy.Post<Permission, Permission>(permission, this.singleEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'AddNew' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Deletes a permission
        /// </summary>
        /// <param name="id">Id of the permission</param>
        /// <returns>True if deleted successfully. False otherwise</returns>
        /// <remarks>Calls DELETE on apiRoot/Permissions/{id}</remarks>
        public ApiCallResponse<bool> Delete(int id)
        {
            var apiCallResult = this.transportProxy.Delete(this.coreEndpoint + "/" + id);
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'Delete' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Forces the API to invalidate and refresh its permissions cache
        /// </summary>
        /// <returns></returns>
        /// <remarks>Calls PUT on apiRoot/Permissions/refresh</remarks>
        public ApiCallResponse<Permission> RefreshPermissionsCache()
        {
            var apiCallResult = this.transportProxy.Put<string, Permission>(this.refreshCacheEndpoint, string.Empty);
            if (!apiCallResult.Success)
            {
                this.LogError("Permissions Api 'RefreshPermissionsCache' call failed");
            }

            return apiCallResult;
        }
    }
}
