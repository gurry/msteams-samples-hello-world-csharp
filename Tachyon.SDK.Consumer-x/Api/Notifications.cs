namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Receive;

    /// <summary>
    /// Abstracts Notifications controller of Consumer API
    /// </summary>
    public class Notifications : ApiBase
    {
        private readonly string coreEndpoint = "Notifications";
        private readonly string getByIdEndpoint = "Notifications/{0}";
        private readonly string getByComponent = "Notifications/Component/{0}/{1}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public Notifications(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets all notifications
        /// </summary>
        /// <returns>Returns collection of all notifications</returns>
        /// <remarks>Calls GET on apiRoot/Notifications</remarks>
        public ApiCallResponse<IEnumerable<Notification>> GetAll()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<Notification>>(this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Notifications Api 'GetAll' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets notifications for specific instruction
        /// </summary>
        /// <param name="id">Instruction id</param>
        /// <returns>Returns collection of notifications pertaining to given instruction</returns>
        /// <remarks>Calls GET on apiRoot/Notifications/{instructionId}</remarks>
        public ApiCallResponse<IEnumerable<Notification>> GetByInstructionId(int id)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<Notification>>(string.Format(this.getByIdEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Notifications Api 'GetByInstructionId' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Gets notifications for a specific component type and optionally an id
        /// </summary>
        /// <param name="component">Component type</param>
        /// <param name="componentId">Optional. Component id</param>
        /// <returns>Collection of notification pertaining to given component type and optionally instance</returns>
        /// <remarks>Calls GET on apiRoot/Notifications/Component/{component}/{componentId:int?}</remarks>
        public ApiCallResponse<IEnumerable<Notification>> GetByComponentType(string component, int? componentId = null)
        {
            var endpoint = componentId.HasValue
                ? string.Format(this.getByComponent, component, componentId)
                : string.Format(this.getByComponent, component, string.Empty);
            var apiCallResult = this.transportProxy.Get<IEnumerable<Notification>>(endpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Notifications Api 'GetByComponentType' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Searches for notifications that meet given criteria
        /// </summary>
        /// <param name="searchParameters">Search parameters</param>
        /// <returns>Collection of notifications that meet given search criteria</returns>
        /// <remarks>Calls POST on apiRoot/Notifications</remarks>
        public ApiCallResponse<SearchResult<Notification>> Get(Models.Send.Search searchParameters)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.Search, SearchResult<Notification>>(searchParameters, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Notifications Api 'Get' call failed");
            }

            return apiCallResult;
        }
    }
}
