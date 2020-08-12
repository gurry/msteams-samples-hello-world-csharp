namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Abstracts ApplicableOperations controller of Consumer API
    /// </summary>
    public class ApplicableOperations : ApiBase
    {
        private readonly string getSecurableTypeByIdEndpoint = "ApplicableOperations/SecurableTypeId/{0}";
        private readonly string getSecurableTypeByNameEndpoint = "ApplicableOperations/SecurableTypeName/{0}";
        private readonly string codeEndpoint = "ApplicableOperations";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public ApplicableOperations(ITransportProxy transportProxy, ILogProxy logProxy) : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets Applicable Operation by id
        /// </summary>
        /// <param name="id">Id to look for</param>
        /// <returns>Applicable Operation object if found. Null if not found</returns>
        /// <remarks>Calls GET on apiRoot/ApplicableOperations/SecurableTypeId/{securableTypeId}</remarks>
        public ApiCallResponse<IEnumerable<ApplicableOperation>> Get(int id)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<ApplicableOperation>>(string.Format(this.getSecurableTypeByIdEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Applicable Operations Api 'Get' (id) call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets Applicable Operation by name
        /// </summary>
        /// <param name="name">Name to look for</param>
        /// <returns>Applicable Operation object if found. Null if not found</returns>
        /// <remarks>Calls GET on apiRoot/ApplicableOperations/SecurableTypeId/{securableTypeName}</remarks>
        public ApiCallResponse<IEnumerable<ApplicableOperation>> Get(string name)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<ApplicableOperation>>(string.Format(this.getSecurableTypeByNameEndpoint, name));
            if (!apiCallResult.Success)
            {
                this.LogError("Applicable Operations Api 'Get' (name) call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Adds new Applicable Operation
        /// </summary>
        /// <param name="newOperation">Applicable Operation to add</param>
        /// <returns>Newly added Applicable Operation</returns>
        /// <remarks>Calls POST on apiRoot/ApplicableOperations/</remarks>
        public ApiCallResponse<ApplicableOperation> Add(ApplicableOperation newOperation)
        {
            var apiCallResult = this.transportProxy.Post<ApplicableOperation, ApplicableOperation>(newOperation, this.codeEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Applicable Operations Api 'AddApplicableOperation' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Deleted an Applicable Operation
        /// </summary>
        /// <param name="id">Id of the Applicable Operation</param>
        /// <returns>True if successful, false otherwise</returns>
        /// <remarks>Calls DELETE on apiRoot/ApplicableOperations//{Id}</remarks>
        public ApiCallResponse<bool> Delete(int id)
        {
            var apiCallResult = this.transportProxy.Delete(this.codeEndpoint + "/" + id);
            if (!apiCallResult.Success)
            {
                this.LogError("Applicable Operations Api 'AddApplicableOperation' call failed");
            }

            return apiCallResult;
        }
    }
}
