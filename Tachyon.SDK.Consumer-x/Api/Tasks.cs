namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;
    using Tachyon.SDK.Consumer.Models.Receive;
    using Tachyon.SDK.Consumer.Models.Send;

    /// <summary>
    /// Abstracts Tasks controller of Consumer API
    /// </summary>
    public class Tasks : ApiBase
    {
        private readonly string getTaskGroupByParentEndpoint = "Tasks/Parent/{0}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public Tasks(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets Task Groups given the parent Id. If the parent Id is null, retrieves the root Task Group.
        /// </summary>
        /// <param name="id">Id of Task Group to be retrieved</param>
        /// <returns>Returns a TaskGroupByParent object, which contains Tasks and Task Groups</returns>
        /// <remarks>Calls GET on apiRoot/Tasks/Parent/{id}</remarks>
        public ApiCallResponse<TaskGroupByParent> GetTaskGroupByParentId(int? id)
        {
            string endPoint = this.getTaskGroupByParentEndpoint;
            if (!id.HasValue)
            {
                endPoint = endPoint.Replace("/{0}", string.Empty);
            }

            var apiCallResult = this.transportProxy.Get<TaskGroupByParent>(string.Format(endPoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Tasks Api 'GetTaskGroupByParentId' call failed");
            }

            return apiCallResult;
        }
    }
}
