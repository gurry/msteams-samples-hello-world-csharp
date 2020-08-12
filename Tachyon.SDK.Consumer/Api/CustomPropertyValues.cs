namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Abstracts CustomPropertyValues controller of Consumer API
    /// </summary>
    public class CustomPropertyValues : ApiBase
    {
        private readonly string coreEndpoint = "CustomPropertyValues";
        private readonly string getForPropertyEndpoint = "CustomPropertyValues/Property/{0}";
        private readonly string getById = "CustomPropertyValues/Id/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public CustomPropertyValues(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets all the values for given custom property
        /// </summary>
        /// <param name="id">Custom property id</param>
        /// <returns>Collection of custom property value objects for given custom property id</returns>
        /// <remarks>Calls GET on apiRoot/CustomPropertyValues/Property/{propertyId}</remarks>
        public ApiCallResponse<IEnumerable<CustomPropertyValue>> GetForProperty(int id)
        {
            var apiCallReturn = this.transportProxy.Get<IEnumerable<CustomPropertyValue>>(string.Format(this.getForPropertyEndpoint, id));
            if (!apiCallReturn.Success)
            {
                this.LogError("CustomPropertyValues Api 'GetForProperty' call failed");
            }

            return apiCallReturn;
        }

        /// <summary>
        /// Gets custom property value
        /// </summary>
        /// <param name="id">Id of the value</param>
        /// <returns>Custom property object if found</returns>
        /// <remarks>Calls GET on apiRoot/CustomPropertyValues/Id/{Id}</remarks>
        public ApiCallResponse<CustomPropertyValue> Get(int id)
        {
            var apiCallReturn = this.transportProxy.Get<CustomPropertyValue>(string.Format(this.getById, id));
            if (!apiCallReturn.Success)
            {
                this.LogError("CustomPropertyValues Api 'Get' call failed");
            }

            return apiCallReturn;
        }

        /// <summary>
        /// Adds new custom property value
        /// </summary>
        /// <param name="propertyValue">Custom property value to add</param>
        /// <returns>Newly added custom property value object</returns>
        /// <remarks>Calls POST on apiRoot/CustomPropertyValues</remarks>
        public ApiCallResponse<CustomPropertyValue> Add(CustomPropertyValue propertyValue)
        {
            var apiCallReturn = this.transportProxy.Post<CustomPropertyValue, CustomPropertyValue>(propertyValue, this.coreEndpoint);
            if (!apiCallReturn.Success)
            {
                this.LogError("CustomPropertyValues Api 'Add' call failed");
            }

            return apiCallReturn;
        }

        /// <summary>
        /// Deletes an custom property value
        /// </summary>
        /// <param name="id">Id of custom property value to delete</param>
        /// <returns>True if successful. False otherwise.</returns>
        /// <remarks>Calls DELETE on apiRoot/CustomPropertyValues/{Id}</remarks>
        public ApiCallResponse<bool> Delete(int id)
        {
            var apiCallReturn = this.transportProxy.Delete(this.coreEndpoint + "/" + id);
            if (!apiCallReturn.Success)
            {
                this.LogError("CustomPropertyValues Api 'Delete' call failed");
            }

            return apiCallReturn;
        }
    }
}
