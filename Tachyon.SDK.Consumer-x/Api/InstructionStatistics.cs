namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Receive;

    /// <summary>
    /// Abstracts InstructionStatistics controller of Consumer API
    /// </summary>
    public class InstructionStatistics : ApiBase
    {
        private readonly string coreEndpoint = "InstructionStatistics";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public InstructionStatistics(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets all instruction statistics
        /// </summary>
        /// <returns>Collection of instruction statistics</returns>
        /// <remarks>Calls GET on apiRoot/InstructionStatistics</remarks>
        public ApiCallResponse<IEnumerable<InstructionStatistic>> GetAll()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<InstructionStatistic>>(this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionStatistics Api 'GetAll' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets instruction statistics for a specific instruction
        /// </summary>
        /// <param name="id">Instruction Id</param>
        /// <returns>Instruction statistics</returns>
        /// <remarks>Calls GET on apiRoot/InstructionStatistics/{id}</remarks>
        public ApiCallResponse<InstructionStatistic> Get(int id)
        {
            var apiCallResult = this.transportProxy.Get<InstructionStatistic>(this.coreEndpoint + "/" + id);
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionStatistics Api 'GetAll' call failed");
            }

            return apiCallResult;
        }
    }
}
