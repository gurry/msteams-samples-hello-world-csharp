namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Abstracts CustomProperties controller of Consumer API
    /// </summary>
    public class CustomProperties : ApiBase
    {
        private readonly string coreEndpoint = "CustomProperties";
        private readonly string getByTypeIdEndpoint = "CustomProperties/TypeId/{0}";
        private readonly string getByTypeNameEndpoint = "CustomProperties/TypeName/{0}";
        private readonly string getByIdEndpoint = "CustomProperties/Id/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public CustomProperties(ITransportProxy transportProxy, ILogProxy logProxy) : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets custom properties of a specific type
        /// </summary>
        /// <param name="propertyTypeId">Id of the custom property type</param>
        /// <returns>Collection of custom properties</returns>
        /// <remarks>Calls GET on apiRoot/CustomProperties/TypeId/{typeId}</remarks>
        public ApiCallResponse<IEnumerable<CustomProperty>> GetForSpecificPropertyType(int propertyTypeId)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<CustomProperty>>(string.Format(this.getByTypeIdEndpoint, propertyTypeId));
            if (!apiCallResult.Success)
            {
                this.LogError("CustomProperties Api 'GetForSpecificPropertyType' (Id) call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets custom properties of a specific type
        /// </summary>
        /// <param name="propertyTypeName">Name of the custom property type</param>
        /// <returns>Collection of custom properties</returns>
        /// <remarks>Calls GET on apiRoot/CustomProperties/TypeName/{typeName}</remarks>
        public ApiCallResponse<IEnumerable<CustomProperty>> GetForSpecificPropertyType(string propertyTypeName)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<CustomProperty>>(string.Format(this.getByTypeNameEndpoint, propertyTypeName));
            if (!apiCallResult.Success)
            {
                this.LogError("CustomProperties Api 'GetForSpecificPropertyType' (Name) call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets an custom property
        /// </summary>
        /// <param name="id">Id of the property</param>
        /// <returns>Custom property object if found.</returns>
        /// <remarks>Calls GET on apiRoot/CustomProperties/Id/{Id}</remarks>
        public ApiCallResponse<CustomProperty> Get(int id)
        {
            var apiCallResult = this.transportProxy.Get<CustomProperty>(string.Format(this.getByIdEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("CustomProperties Api 'Get' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Adds new custom property
        /// </summary>
        /// <param name="property">Property to add</param>
        /// <returns>Newly added Custom Property</returns>
        /// <remarks>Calls POST on apiRoot/CustomProperties</remarks>
        public ApiCallResponse<CustomProperty> Add(CustomProperty property)
        {
            var apiCallResult = this.transportProxy.Post<CustomProperty, CustomProperty>(property, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("CustomProperties Api 'Add' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Updates an existing property
        /// </summary>
        /// <param name="property">Property to update</param>
        /// <returns>Update custom property</returns>
        /// <remarks>Calls PUT on apiRoot/CustomProperties</remarks>
        public ApiCallResponse<CustomProperty> Update(CustomProperty property)
        {
            var apiCallResult = this.transportProxy.Put<CustomProperty, CustomProperty>(property, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("CustomProperties Api 'Update' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Deletes an existing property
        /// </summary>
        /// <param name="id">Id of the property to delete</param>
        /// <returns>True if successful</returns>
        /// <remarks>Calls DELETE on apiRoot/CustomProperties/{id}</remarks>
        public ApiCallResponse<bool> Delete(int id)
        {
            var apiCallResult = this.transportProxy.Delete(this.coreEndpoint + "/" + id);
            if (!apiCallResult.Success)
            {
                this.LogError("CustomProperties Api 'Delete' call failed");
            }

            return apiCallResult;
        }
    }
}
