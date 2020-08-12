namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;
    using Tachyon.SDK.Consumer.Tools;

    /// <summary>
    /// Abstracts Consumers controller of Consumer API
    /// </summary>
    /// <remarks>
    /// You do not have to use a registered or enabled consumer in Tachyon in order to use methods in this class.
    /// </remarks>
    public class Consumers : ApiBase
    {
        private readonly string coreEndpoint = "Consumers";
        private readonly string byIdEndpoint = "Consumers/Id/{0}";
        private readonly string byNameEndpoint = "Consumers/Name/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public Consumers(ITransportProxy transportProxy, ILogProxy logProxy) : base(transportProxy, logProxy)
        {
        }
        /// <summary>
        /// Get all consumers in the system
        /// </summary>
        /// <returns>Collection of consumers</returns>
        /// <remarks>Calls GET on apiRoot/Consumers</remarks>
        public ApiCallResponse<IEnumerable<Consumer>> GetAll()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<Consumer>>(this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Consumers Api 'GetAll' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets a consumer
        /// </summary>
        /// <param name="id">Id of the consumer</param>
        /// <returns>Consumer object if found. Null otherwise</returns>
        /// <remarks>Calls GET on apiRoot/Consumers/Id/{Id}</remarks>
        public ApiCallResponse<Consumer> Get(int id)
        {
            var endpoint = string.Format(this.byIdEndpoint, id);
            var apiCallResult = this.transportProxy.Get<Consumer>(endpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Consumers Api 'Get' (Id) call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Gets a consumer
        /// </summary>
        /// <param name="name">Name of the consumer</param>
        /// <returns>Consumer object if found. Null otherwise</returns>
        /// <remarks>Calls GET on apiRoot/Consumers/Name/{Name}</remarks>
        public ApiCallResponse<Consumer> Get(string name)
        {
            var endpoint = string.Format(this.byNameEndpoint, Base64Utility.ToUrlSafeBase64(name));
            var apiCallResult = this.transportProxy.Get<Consumer>(endpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Consumers Api 'Get' (name) call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Add a new consumer
        /// </summary>
        /// <param name="consumer">Consumer to add</param>
        /// <returns>Added consumer</returns>
        /// <remarks>Calls POST on apiRoot/Consumers</remarks>
        public ApiCallResponse<Consumer> Add(Consumer consumer)
        {
            var apiCallResult = this.transportProxy.Post<Consumer, Consumer>(consumer, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Consumers Api 'Add' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Update an existing consumer
        /// </summary>
        /// <param name="consumer">Consumer to update</param>
        /// <returns>Updated consumer</returns>
        /// <remarks>Calls PUT on apiRoot/Consumers</remarks>
        public ApiCallResponse<Consumer> Update(Consumer consumer)
        {
            var apiCallResult = this.transportProxy.Put<Consumer, Consumer>(consumer, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Consumers Api 'Update' call failed");
            }

            return apiCallResult;
        }
        /// <summary>
        /// Deletes an existing consumer
        /// </summary>
        /// <param name="id">Id of the Consumer to delete</param>
        /// <returns>True if successful</returns>
        /// <remarks>Calls DELETE on apiRoot/Consumers/{id}</remarks>
        public ApiCallResponse<bool> Delete(int id)
        {
            var apiCallResult = this.transportProxy.Delete(this.coreEndpoint + "/" + id);
            if (!apiCallResult.Success)
            {
                this.LogError("Consumers Api 'Delete' call failed");
            }

            return apiCallResult;
        }
    }
}
