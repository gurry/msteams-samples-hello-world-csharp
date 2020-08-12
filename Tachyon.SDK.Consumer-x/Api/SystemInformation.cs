namespace Tachyon.SDK.Consumer.Api
{
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Receive;

    /// <summary>
    /// Abstracts SystemInformation controller of Consumer API
    /// </summary>
    public class SystemInformation : ApiBase
    {
        private readonly string topologyEndpoint = "SystemInformation/GetSystemTopography";
        private readonly string licenseEndpoint = "SystemInformation/License";
        private readonly string featureExpiry = "SystemInformation/LicenseExpiry/Product/{0}/Feature/{1}";
        private readonly string productExpiry = "SystemInformation/LicenseExpiry/Product/{0}";
        private readonly string reactivate = "SystemInformation/License/Reactivate";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public SystemInformation(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Retrieves the current topology of the system.
        /// Topology lists data stores, each data store lists cores connected to it,
        /// each core lists switches connected to it and each switch has the number
        /// of agents connected to it.
        /// </summary>
        /// <returns>Topology object</returns>
        /// <remarks>Calls GET on apiRoot/SystemInformation/GetSystemTopography</remarks>
        public ApiCallResponse<SystemTopology> GetSystemTopology()
        {
            var apiCallResult = this.transportProxy.Get<SystemTopology>(this.topologyEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("SystemInformation Api 'GetSystemTopology' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Retrieves licensing information
        /// </summary>
        /// <returns>License object</returns>
        /// <remarks>Calls GET on apiRoot/SystemInformation/License</remarks>
        public ApiCallResponse<License> GetLicense()
        {
            var apiCallResult = this.transportProxy.Get<License>(this.licenseEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("SystemInformation Api 'GetLicense' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Checks if given feature for a given product is licensed.
        /// </summary>
        /// <param name="product">Product name</param>
        /// <param name="feature">Feature name</param>
        /// <returns>True if feature is licensed. False if it isn't or an error has occurred while checking for licensing information</returns>
        /// <remarks>Calls GET on apiRoot/SystemInformation/LicenseExpiry/Product/{product}/Feature/{feature}</remarks>
        public ApiCallResponse<FeatureExpiry> GetFeatureExpiry(string product, string feature)
        {
            string endpoint = string.Format(this.featureExpiry, product, feature);
            var apiCallResult = this.transportProxy.Get<FeatureExpiry>(endpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("SystemInformation Api 'GetFeatureExpiry' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Checks if given product is licensed.
        /// </summary>
        /// <param name="product">Product name</param>
        /// <returns>True if product is licensed. False if it isn't or an error has occurred while checking for licensing information</returns>
        /// <remarks>Calls GET on apiRoot/SystemInformation/LicenseExpiry/Product/{product}</remarks>
        public ApiCallResponse<FeatureExpiry> GetProductExpiry(string product)
        {
            string endpoint = string.Format(this.productExpiry, product);
            var apiCallResult = this.transportProxy.Get<FeatureExpiry>(endpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("SystemInformation Api 'GetProductExpiry' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Issued a request to reactivate Tachyon license
        /// </summary>
        /// <returns>Bool if the request was issued successfully. False if an error has occurred.</returns>
        /// <remarks>Calls POST on apiRoot/SystemInformation/License/Reactivate</remarks>
        public ApiCallResponse<bool> ReactivateTachyonLicense()
        {
            var apiCallResult = this.transportProxy.PostEmpty<bool>(this.reactivate);
            if (!apiCallResult.Success)
            {
                this.LogError("SystemInformation Api 'ReactivateLicense' call failed");
            }

            return apiCallResult;
        }
    }
}
