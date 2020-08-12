namespace Tachyon.SDK.Consumer.Api
{
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Receive;
    using Tachyon.SDK.Consumer.Models.Send;

    /// <summary>
    /// Abstracts AuditLogs controller of Consumer API
    /// </summary>
    public class AuditLogs : ApiBase
    {
        private readonly string coreEndpoint = "AuditLogs";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public AuditLogs(ITransportProxy transportProxy, ILogProxy logProxy) : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Returns Audit Logs that match given search settings
        /// </summary>
        /// <param name="searchSettings">Search settings</param>
        /// <returns>Any audit logs matching the search setting</returns>
        /// <remarks>Calls POST on apiRoot/AuditLogs</remarks>
        public ApiCallResponse<SearchResult<AuditLog>> FindAuditLogs(Search searchSettings)
        {
            var apiCallResult = this.transportProxy.Post<Search, SearchResult<AuditLog>>(searchSettings, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Audit logs Api 'FindAuditLogs' call failed");
            }

            return apiCallResult;
        }
    }
}
