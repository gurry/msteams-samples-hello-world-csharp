namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Send;

    /// <summary>
    /// Abstracts DataExport controller of Consumer API
    /// </summary>
    public class DataExport : ApiBase
    {
        private readonly string coreEndpoint = "DataExport";
        private readonly string initExportRepsonsesEndpoint = "DataExport/exportresponses";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public DataExport(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Converts given comma delimited data stream to a csv file
        /// </summary>
        /// <param name="data">Data to convert</param>
        /// <returns>Stream containing the converted file</returns>
        /// <remarks>Calls POST with Form data on apiRoot/DataExport</remarks>
        public ApiCallResponse<string> ConvertToCsv(string data)
        {
            var formData = new Dictionary<string,string>()
            {
                { "Filename", "file.csv" },
                { "Data", data },
            };
            var apiCallResult = this.transportProxy.PostForm<string>(formData, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Data Export Api 'ConvertToCsv' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Initiates responses export
        /// </summary>
        /// <param name="dataExportSettings">Data export settings</param>
        /// <returns>Only success indication</returns>
        /// <remarks>Calls POST on apiRoot/DataExport/exportresponses</remarks>
        public ApiCallResponse<object> InitiateResponsesExport(ExportResponses dataExportSettings)
        {
            var apiCallResult = this.transportProxy.Post<ExportResponses, object>(dataExportSettings, this.initExportRepsonsesEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Data Export Api 'InitiateResponsesExport' call failed");
            }

            return apiCallResult;
        }
    }
}
