namespace Tachyon.SDK.Consumer.Api
{
    using Tachyon.SDK.Consumer.Enums;
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;

    /// <summary>
    /// Abstracts Settings controller of Consumer API
    /// </summary>
    public class Settings : ApiBase
    {
        private readonly string coreEndpoint = "Settings/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public Settings(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Returns value of a given setting
        /// </summary>
        /// <param name="setting">Setting name</param>
        /// <returns>Value</returns>
        /// <remarks>Calls GET on apiRoot/Settings/{key}</remarks>
        public ApiCallResponse<string> Get(SettingTypes setting)
        {
            var apiCallResult = this.transportProxy.Get<string>(string.Format(this.coreEndpoint, setting.ToString()));
            if (!apiCallResult.Success)
            {
                this.LogError("Settings Api 'Get' call failed");
            }

            return apiCallResult;
        }
    }
}
