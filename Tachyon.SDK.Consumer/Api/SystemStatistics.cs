namespace Tachyon.SDK.Consumer.Api
{
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Receive;
    using Tachyon.SDK.Consumer.Models.Send;

    /// <summary>
    /// Abstracts SystemStatistics controller of Consumer API
    /// </summary>
    public class SystemStatistics : ApiBase
    {
        private readonly string highLevelEndpoint = "SystemStatistics/HighLevel";
        private readonly string detailEndpoint = "SystemStatistics/Detail";
        private readonly string projectedStatisticsEndpoint = "SystemStatistics/ProjectedInstructionStatistics";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public SystemStatistics(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets high level system statistics
        /// </summary>
        /// <returns>High level system statistics</returns>
        /// <remarks>Calls GET on apiRoot/SystemStatistics/HighLevel</remarks>
        public ApiCallResponse<HighLevelSystemStatistics> GetHighLevelStatistics()
        {
            var apiCallResult = this.transportProxy.Get<HighLevelSystemStatistics>(this.highLevelEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("SystemStatistics Api 'GetHighLevelStatistics' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets detailed system statistics
        /// </summary>
        /// <returns>Detailed system statistics</returns>
        /// <remarks>Calls GET on apiRoot/SystemStatistics/Detail</remarks>
        public ApiCallResponse<DetailSystemStatistics> GetDetailedStatistics()
        {
            var apiCallResult = this.transportProxy.Get<DetailSystemStatistics>(this.detailEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("SystemStatistics Api 'GetDetailedStatistics' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets projected statistics for a given instruction definition with specific scope
        /// </summary>
        /// <param name="request">Id - the id of the instruction definition
        /// Expression - expression to be used as instruction's scope</param>
        /// <returns>Projected statistics calculated based on previous runs of given instruction definition.
        /// Projection gives an estimation on what the system calculates various stats to be if given instruction
        /// definition was issued with given scope.</returns>
        /// <remarks>Calls GET on apiRoot/SystemStatistics/ProjectedInstructionStatistics</remarks>
        public ApiCallResponse<ProjectedInstructionStatistic> GetProjectedInstructionStatistics(IdExpressionPair request)
        {
            var apiCallResult = this.transportProxy.Post<IdExpressionPair, ProjectedInstructionStatistic>(request, this.projectedStatisticsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("SystemStatistics Api 'GetProjectedInstructionStatistics' call failed");
            }

            return apiCallResult;
        }
    }
}
