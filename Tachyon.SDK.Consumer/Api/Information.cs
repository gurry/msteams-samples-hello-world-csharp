namespace Tachyon.SDK.Consumer.Api
{
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Receive;

    /// <summary>
    /// Abstracts Information controller of Consumer API
    /// </summary>
    public class Information : ApiBase
    {
        private readonly string coreEndpoint = "Information";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public Information(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets general system information
        /// </summary>
        /// <returns>API version information</returns>
        /// <remarks>Calls GET on apiRoot/Information</remarks>
        public ApiCallResponse<ProductInformation> Get()
        {
            var apiCallResult = this.transportProxy.Get<ProductInformation>(this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Information Api 'Get' call failed");
            }

            return apiCallResult;
        }
    }
}
