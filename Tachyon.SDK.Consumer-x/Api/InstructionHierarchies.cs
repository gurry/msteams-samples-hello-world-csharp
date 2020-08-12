namespace Tachyon.SDK.Consumer.Api
{
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;

    /// <summary>
    /// Abstracts InstructionHierarchies controller of Consumer API
    /// </summary>
    public class InstructionHierarchies : ApiBase
    {
        private readonly string getByIdEndpoint = "InstructionHierarchies/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public InstructionHierarchies(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets instruction hierarchy for given instruction
        /// </summary>
        /// <param name="id">Id of the instruction</param>
        /// <returns>Instruction hierarchy object</returns>
        /// <remarks>Calls GET on apiRoot/InstructionHierarchies/{id}</remarks>
        public ApiCallResponse<Models.Receive.InstructionHierarchies> Get(int id)
        {
            var apiCallReturn = this.transportProxy.Get<Models.Receive.InstructionHierarchies>(string.Format(this.getByIdEndpoint, id));
            if (!apiCallReturn.Success)
            {
                this.LogError("InstructionHierarchies Api 'Get' call failed");
            }

            return apiCallReturn;
        }
    }
}
