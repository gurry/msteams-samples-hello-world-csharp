namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Receive;

    /// <summary>
    /// Abstracts ResponseTemplates controller of Consumer API
    /// </summary>
    public class ResponseTemplates : ApiBase
    {
        private readonly string coreEndpoint = "ResponseTemplates";
        private readonly string byIdEndpoint = "ResponseTemplates/Id/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public ResponseTemplates(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets a specific response template object
        /// </summary>
        /// <param name="id">Id of the response template</param>
        /// <returns>Response template</returns>
        /// <remarks>Calls GET on apiRoot/ResponseTemplates</remarks>
        public ApiCallResponse<ResponseTemplate> GetById(int id)
        {
            var apiCallResult = this.transportProxy.Get<ResponseTemplate>(string.Format(this.byIdEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Response Templates Api 'GetById' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets all response templates in the system
        /// </summary>
        /// <returns>Collection of response templates</returns>
        /// <remarks>Calls GET on apiRoot/ResponseTemplates/Id/{id}</remarks>
        public ApiCallResponse<IEnumerable<ResponseTemplate>> GetAll()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<ResponseTemplate>>(this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Response Templates Api 'GetAll' call failed");
            }

            return apiCallResult;
        }
    }
}
