namespace Tachyon.SDK.Consumer.Api
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Newtonsoft.Json;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Receive;
    using Tachyon.SDK.Consumer.Tools;

    /// <summary>
    /// Abstracts ProductPacks controller of Consumer API
    /// </summary>
    public class ProductPacks : ApiBase
    {
        private readonly string coreEndpoint = "ProductPacks";
        private readonly string bySetIdEndpoint = "ProductPacks/InstructionSet/Id/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public ProductPacks(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Uploads a product pack
        /// </summary>
        /// <param name="stream">Stream with the contents of the product pack</param>
        /// <param name="fileName">Name of the product pack</param>
        /// <returns></returns>
        /// <remarks>Calls apiRoot/ProductPacks</remarks>
        public ApiCallResponse<IEnumerable<InstructionDefinitionUploadResult>> Upload(MemoryStream stream, string fileName)
        {
            var apiCallResult = this.transportProxy.UploadFile(stream, fileName, this.coreEndpoint);
            return ProcessUploadResponse(apiCallResult, "Upload");
        }

        /// <summary>
        /// Uploads a product pack
        /// </summary>
        /// <param name="filePath">Full path to the product pack zip file</param>
        /// <returns></returns>
        /// <remarks>Calls apiRoot/ProductPacks</remarks>
        public ApiCallResponse<IEnumerable<InstructionDefinitionUploadResult>> Upload(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            var fileName = Path.GetFileName(filePath);
            var stream = new MemoryStream(File.ReadAllBytes(filePath));
            return Upload(stream, fileName);
        }

        /// <summary>
        /// Uploads a product pack into a specific instruction set
        /// </summary>
        /// <param name="stream">Stream with the contents of the product pack</param>
        /// <param name="fileName">Name of the product pack</param>
        /// <param name="instructionSetId">Id of the instruction set you want to upload the instruction(s) to.</param>
        /// <returns></returns>
        /// <remarks>Calls apiRoot/ProductPacks/Id/{id}</remarks>
        public ApiCallResponse<IEnumerable<InstructionDefinitionUploadResult>> UploadIntoInstructionSet(MemoryStream stream, string fileName, int instructionSetId)
        {
            var endpoint = string.Format(this.bySetIdEndpoint, instructionSetId);
            var apiCallResult = this.transportProxy.UploadFile(stream, fileName, endpoint);

            return ProcessUploadResponse(apiCallResult, "UploadIntoInstructionSet");
        }

        /// <summary>
        /// Uploads a product pack into a specific instruction set
        /// </summary>
        /// <param name="filePath">Full path to the product pack zip file</param>
        /// <param name="instructionSetId">Id of the instruction set you want to upload the instruction(s) to.</param>
        /// <returns></returns>
        /// <remarks>Calls apiRoot/ProductPacks/Id/{id}</remarks>
        public ApiCallResponse<IEnumerable<InstructionDefinitionUploadResult>> UploadIntoInstructionSet(string filePath, int instructionSetId)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            var fileName = Path.GetFileName(filePath);
            var stream = new MemoryStream(File.ReadAllBytes(filePath));
            return UploadIntoInstructionSet(stream, fileName, instructionSetId);
        }

        private ApiCallResponse<IEnumerable<InstructionDefinitionUploadResult>> ProcessUploadResponse(
            ApiCallResponse<string> response,
            string methodName)
        {
            if (!response.Success)
            {
                this.LogError(string.Format("Product Packs Api '{0}' call failed", methodName));
                return new ApiCallResponse<IEnumerable<InstructionDefinitionUploadResult>>(null, false, response.Errors, response.ResponseStatusCode);
            }

            try
            {
                var results = JsonConvert.DeserializeObject<IEnumerable<InstructionDefinitionUploadResult>>(response.ReceivedObject);
                return new ApiCallResponse<IEnumerable<InstructionDefinitionUploadResult>>(results, true, null, response.ResponseStatusCode);
            }
            catch (Exception ex)
            {
                return new ApiCallResponse<IEnumerable<InstructionDefinitionUploadResult>>(
                    null,
                    false,
                    ErrorCreator.CreateErrorObject(ex),
                    response.ResponseStatusCode);
            }
        }
    }
}
