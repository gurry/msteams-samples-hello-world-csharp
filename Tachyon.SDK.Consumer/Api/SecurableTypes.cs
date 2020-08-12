namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Abstracts SecurableTypes controller of Consumer API
    /// </summary>
    public class SecurableTypes : ApiBase
    {
        private readonly string coreEndpoint = "SecurableTypes";
        private readonly string byIdEndpoint = "SecurableTypes/{0}";
        private readonly string byNameEndpoint = "SecurableTypes/Name/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public SecurableTypes(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Get all securable types
        /// </summary>
        /// <returns>Collection of securable type objects</returns>
        /// <remarks>Calls GET on apiRoot/SecurableTypes</remarks>
        public ApiCallResponse<IEnumerable<SecurableType>> GetAll()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<SecurableType>>(this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("SecurableTypes Api 'GetAll' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Get a securable type
        /// </summary>
        /// <param name="id">Id of the securable type</param>
        /// <returns>Securable type object</returns>
        /// <remarks>Calls GET on apiRoot/SecurableTypes/{id}</remarks>
        public ApiCallResponse<SecurableType> Get(int id)
        {
            var apiCallResult = this.transportProxy.Get<SecurableType>(string.Format(this.byIdEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("SecurableTypes Api 'Get' (id) call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Get a securable type
        /// </summary>
        /// <param name="name">Name of the securable type</param>
        /// <returns>Securable type object</returns>
        /// <remarks>Calls GET on apiRoot/SecurableTypes/Name/{name}</remarks>
        public ApiCallResponse<SecurableType> Get(string name)
        {
            var apiCallResult = this.transportProxy.Get<SecurableType>(string.Format(this.byNameEndpoint, name));
            if (!apiCallResult.Success)
            {
                this.LogError("SecurableTypes Api 'Get' (name) call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Adds a securable type
        /// </summary>
        /// <param name="securableType">Securable type to add</param>
        /// <returns>Added object</returns>
        /// <remarks>Calls POST on apiRoot/SecurableTypes</remarks>
        public ApiCallResponse<SecurableType> Add(SecurableType securableType)
        {
            var apiCallResult = this.transportProxy.Post<SecurableType, SecurableType>(securableType, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("SecurableTypes Api 'Add' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Updates a securable type
        /// </summary>
        /// <param name="securableType">Securable type to update. This securable type must already exist in the database</param>
        /// <returns>Updated object</returns>
        /// <remarks>Calls PUT on apiRoot/SecurableTypes</remarks>
        public ApiCallResponse<SecurableType> Update(SecurableType securableType)
        {
            var apiCallResult = this.transportProxy.Put<SecurableType, SecurableType>(securableType, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("SecurableTypes Api 'Update' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Deleted a securable type
        /// </summary>
        /// <param name="id">Id of the securable type to delete</param>
        /// <returns>True if deleted successfully. False otherwise.</returns>
        /// <remarks>Calls DELETE on apiRoot/SecurableTypes/{id}</remarks>
        public ApiCallResponse<bool> Delete(int id)
        {
            var apiCallResult = this.transportProxy.Delete(string.Format(this.byIdEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("SecurableTypes Api 'Delete' call failed");
            }

            return apiCallResult;
        }
    }
}
