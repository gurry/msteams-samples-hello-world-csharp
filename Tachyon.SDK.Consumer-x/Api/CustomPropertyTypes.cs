namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;

    /// <summary>
    /// Abstracts CustomPropertyTypes controller of Consumer API
    /// </summary>
    public class CustomPropertyTypes : ApiBase
    {
        private readonly string coreEndpoint = "CustomPropertyTypes";
        private readonly string getTypeEndpointById = "CustomPropertyTypes/Id/{0}";
        private readonly string getTypeEndpointByName = "CustomPropertyTypes/Name/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public CustomPropertyTypes(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets all types of custom properties
        /// </summary>
        /// <returns>Collection of all Custom property values</returns>
        /// <remarks>Calls GET on apiRoot/CustomPropertyTypes</remarks>
        public ApiCallResponse<IEnumerable<Models.Common.CustomPropertyType>> GetAll()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<Models.Common.CustomPropertyType>>(this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("CustomPropertyType Api 'GetAll' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets specific custom property type
        /// </summary>
        /// <param name="id">Id of the custom property type</param>
        /// <returns>Custom property object if found</returns>
        /// <remarks>Calls GET on apiRoot/CustomPropertyTypes/Id/{Id}</remarks>
        public ApiCallResponse<Models.Common.CustomPropertyType> Get(int id)
        {
            var apiCallResult = this.transportProxy.Get<Models.Common.CustomPropertyType>(string.Format(this.getTypeEndpointById, id));
            if (!apiCallResult.Success)
            {
                this.LogError("CustomPropertyType Api 'Get' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets specific custom property type
        /// </summary>
        /// <param name="name">Name of the custom property type</param>
        /// <returns>Custom property object if found</returns>
        /// <remarks>Calls GET on apiRoot/CustomPropertyTypes/Name/{Name}</remarks>
        public ApiCallResponse<Models.Common.CustomPropertyType> Get(string name)
        {
            var apiCallResult = this.transportProxy.Get<Models.Common.CustomPropertyType>(string.Format(this.getTypeEndpointByName, name));
            if (!apiCallResult.Success)
            {
                this.LogError("CustomPropertyType Api 'Get' call failed");
            }

            return apiCallResult;
        }
    }
}
