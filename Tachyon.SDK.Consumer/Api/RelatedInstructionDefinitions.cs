namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.Enums;
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;

    /// <summary>
    /// Abstracts RelatedInstructionDefinitions controller of Consumer API
    /// </summary>
    public class RelatedInstructionDefinitions : ApiBase
    {
        private readonly string getByIdEndpoint = "RelatedInstructionDefinitions/Id/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public RelatedInstructionDefinitions(ITransportProxy transportProxy, ILogProxy logProxy) : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Returns instructions of specified type that are related to a specific instruction
        /// </summary>
        /// <param name="id">Id of the instruction you want to get instructions related to</param>
        /// <param name="instructionTypes">Types of instructions to return</param>
        /// <returns>Related instruction definitions object</returns>
        /// <remarks>Calls GET on apiRoot/RelatedInstructionDefinitions/Id/{id}</remarks>
        public ApiCallResponse<Models.Receive.RelatedInstructionDefinitions> Get(int id, List<InstructionType> instructionTypes)
        {
            string queryString = ConvertInputListToQueryString(instructionTypes);
            string endpoint = string.Format(this.getByIdEndpoint, id);
            var apiCallResult = this.transportProxy.Get<Models.Receive.RelatedInstructionDefinitions>(string.IsNullOrEmpty(queryString) ? endpoint : endpoint + "?" + queryString);
            if (!apiCallResult.Success)
            {
                this.LogError("Related Instruction Definitions Api 'Get' call failed.");
            }

            return apiCallResult;
        }
    }
}
