namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;
    using Tachyon.SDK.Consumer.Models.Receive;
    using Tachyon.SDK.Consumer.Models.Send;
    using Tachyon.SDK.Consumer.Tools;

    /// <summary>
    /// Abstracts Devices controller of Consumer API 
    /// </summary>
    public class Devices : ApiBase
    {
        private readonly string endpointName = "Devices";
        private readonly string getByScopeEndpointName = "Devices/scope";
        private readonly string searchByFqdnEndpointName = "Devices/fqdn";
        private readonly string getApproxTargetEndpointName = "Devices/approxtarget";
        private readonly string getByGuidEndpointName = "Devices/tachyonguid/";
        private readonly string getAsCsv = "Devices/exportmatching";
        private readonly string getByDomainsAndNames = "Devices/ByDomainAndName";
        private readonly string getSummary = "Devices/summary";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public Devices(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Returns all devices
        /// </summary>
        /// <returns>list of devices</returns>
        /// <remarks>Calls GET on apiRoot/Devices</remarks>
        public ApiCallResponse<IEnumerable<Device>> GetDevices()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<Device>>(this.endpointName);
            if (!apiCallResult.Success)
            {
                this.LogError("DevicesApi 'GetDevices' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns all devices that match given scope expression
        /// </summary>
        /// <param name="scope">Scope expression to evaluate</param>
        /// <returns>List of devices</returns>
        /// <remarks>Calls POST on apiRoot/Devices/scope</remarks>
        public ApiCallResponse<IEnumerable<Device>> GetDevicesMatchingScope(ExpressionObject scope)
        {
            var apiCallResult = this.transportProxy.Post<ExpressionObject, IEnumerable<Device>>(scope, this.getByScopeEndpointName);
            if (!apiCallResult.Success)
            {
                this.LogError("DevicesApi 'GetDevicesMatchingScope' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns aggregated impact statistics based on given scope expression
        /// </summary>
        /// <param name="scope">Optional. Scope expression to use when evaluating impact</param>
        /// <returns>Aggregate results showing projected impact as number of machines grouped by DeviceByFqdnSet type and by Operating System</returns>
        /// <remarks>Calls (POST) apiRoot/Devices/approxtarget</remarks>
        public ApiCallResponse<AggregatedDeviceStatisticsContainer> GetApproximateTarget(ExpressionObject scope)
        {
            var apiCallResult = this.transportProxy.Post<ExpressionObject, AggregatedDeviceStatisticsContainer>(scope, this.getApproxTargetEndpointName);
            if (!apiCallResult.Success)
            {
                this.LogError("DevicesApi 'GetApproximateTarget' call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Returns a single device that matches given Fqdn
        /// </summary>
        /// <param name="fqdn">Fully qualified domain name</param>
        /// <returns>Device with given Fqdn is found. Null if not found</returns>
        /// <remarks>Calls GET on apiRoot/Devices/fqdn/{fqdn}</remarks>
        public ApiCallResponse<Device> GetDeviceByFqdn(string fqdn)
        {
            var apiCallResult = this.transportProxy.Get<Device>(this.searchByFqdnEndpointName + "/" + Base64Utility.ToUrlSafeBase64(fqdn));
            if (!apiCallResult.Success)
            {
                this.LogError("DevicesApi 'GetDeviceByFqdn' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets aggregated impact statistics based on a list of Fqdns
        /// </summary>
        /// <param name="settings">Settings to use. FqdnList should have comma separated list of Fqdns. Aggregate is ignored.</param>
        /// <returns>Aggregate results showing projected impact as number of machines grouped by DeviceByFqdnSet type and by Operating System</returns>
        /// <remarks>Calls GET on apiRoot/Devices/fqdn</remarks>
        public ApiCallResponse<AggregatedDeviceStatisticsContainer> GetAggregatedResultOfAffectedDevices(Models.Send.DeviceByFqdnSet settings)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.DeviceByFqdnSet, AggregatedDeviceStatisticsContainer>(settings, this.searchByFqdnEndpointName);
            if (!apiCallResult.Success)
            {
                this.LogError("DevicesApi 'GetAggregatedResultOfAffectedDevices' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns a single device that has given Guid as Tachyon Guid
        /// </summary>
        /// <param name="guid">Guid to look for</param>
        /// <returns>Device with given Guid is found. Null if not found</returns>
        /// <remarks>Calls GET on apiRoot/Devices/tachyonguid/{guid}</remarks>
        public ApiCallResponse<Device> GetDeviceByGuid(string guid)
        {
            var apiCallResult = this.transportProxy.Get<Device>(this.getByGuidEndpointName + guid);
            if (!apiCallResult.Success)
            {
                this.LogError("DevicesApi 'GetDeviceByGuid' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns summarized device information for devices that match given search criteria
        /// </summary>
        /// <param name="searchSettings">Object defining a combination of Filter and Sort expressions, start index and number of devices</param>
        /// <returns>Container with summarized device data</returns>
        /// <remarks>Calls POST on apiRoot/Devices/summary</remarks>
        public ApiCallResponse<SummarizedDevicesContainer> GetSummary(Search searchSettings)
        {
            var apiCallResult = this.transportProxy.Post<Search, SummarizedDevicesContainer>(searchSettings, this.getSummary);
            if (!apiCallResult.Success)
            {
                this.LogError("DevicesApi 'GetSummary' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets devices matching given properties
        /// </summary>
        /// <param name="searchSettings">Object defining a combination of Filter and Sort expressions, start index and number of devices</param>
        /// <returns>Search result object wrapping results themselves and number of rows returned</returns>
        /// <remarks>Calls POST on apiRoot/Devices</remarks>
        public ApiCallResponse<SearchResult<Device>> GetDevices(Models.Send.Search searchSettings)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.Search, SearchResult<Device>> (searchSettings, this.endpointName);
            if (!apiCallResult.Success)
            {
                this.LogError("DevicesApi 'GetDevices' (post) call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns data about devices matching given expression as comma delimited data stream to a csv file
        /// </summary>
        /// <param name="filter">Filter to use when selecting devices. If set to null all devices will be returned</param>
        /// <returns>String containing data about matching devices formatted as comma separated values</returns>
        /// <remarks>Calls POST with FORM data on apiRoot/Devices/exportmatching</remarks>
        public ApiCallResponse<string> GetMatchingDevicesAsCsv(ExpressionObject filter)
        {
            var serializedFilter = filter == null ? string.Empty : JsonConvert.SerializeObject(filter);
            var formData = new Dictionary<string, string>()
            {
                { "Filter", serializedFilter}
            };

            var apiCallResult = this.transportProxy.PostForm<string>(formData, this.getAsCsv);
            if (!apiCallResult.Success)
            {
                this.LogError("Devices Api 'GetMatchingDevicesAsCsv' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Fast query for many devices by domain and name
        /// </summary>
        /// <param name="domainsAndNamesToSearch">Object defining the devices to fetch</param>
        /// <returns>List of structures containing Id, Domain, Name and Status</returns>
        /// <remarks>Calls Devices/ByDomainAndName</remarks>
        public ApiCallResponse<List<DeviceStatus>> GetDevicesByDomainAndName(Models.Send.DevicesDomainsAndNames domainsAndNamesToSearch)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.DevicesDomainsAndNames, List<DeviceStatus>>(domainsAndNamesToSearch, this.getByDomainsAndNames);
            if (!apiCallResult.Success)
            {
                this.LogError("DevicesApi 'GetDevicesByDomainAndName' (post) call failed.");
            }

            return apiCallResult;
        }
    }
}
