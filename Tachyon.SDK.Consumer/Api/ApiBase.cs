namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Tachyon.SDK.Consumer.Enums;
    using Tachyon.SDK.Consumer.ExternalInterfaces;

    /// <summary>
    /// Base class for all the classes that abstract API endpoints
    /// </summary>
    public abstract class ApiBase
    {
        /// <summary>
        /// Root address of the API
        /// </summary>
        protected readonly string apiRootAddress;
        /// <summary>
        /// Instance of a transport proxy
        /// </summary>
        protected readonly ITransportProxy transportProxy = null;
        /// <summary>
        /// Flag indicating if the logger proxy is available
        /// </summary>
        private readonly bool loggerAvailable = false;
        /// <summary>
        /// Instance of a logger proxy
        /// </summary>
        private readonly ILogProxy logProxy = null;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy">Instance of a transport proxy</param>
        /// <param name="logProxy">Optional. Instance of a logger proxy</param>
        protected ApiBase(ITransportProxy transportProxy, ILogProxy logProxy)
        {
            if (logProxy != null)
            {
                this.loggerAvailable = true;
            }

            this.transportProxy = transportProxy;
            this.logProxy = logProxy;
        }

        /// <summary>
        /// Sends error level message through the logger proxy, if one is available
        /// </summary>
        /// <param name="text">Message</param>
        protected void LogError(string text)
        {
            if (this.loggerAvailable)
            {
                this.logProxy.LogError(text);
            }
        }

        /// <summary>
        /// Sends warning level message through the logger proxy, if one is available
        /// </summary>
        /// <param name="text">Message</param>
        protected void LogWarning(string text)
        {
            if (this.loggerAvailable)
            {
                this.logProxy.LogWarning(text);
            }
        }

        /// <summary>
        /// Sends information level message through the logger proxy, if one is available
        /// </summary>
        /// <param name="text">Message</param>
        protected void LogInfo(string text)
        {
            if (this.loggerAvailable)
            {
                this.logProxy.LogInfo(text);
            }
        }

        /// <summary>
        /// Convert a list of instruction types into query string parameters.
        /// </summary>
        /// <param name="instructionTypes">List of instruction type enum values</param>
        /// <returns>Query string</returns>
        protected string ConvertInputListToQueryString(List<InstructionType> instructionTypes)
        {
            if (instructionTypes == null || !instructionTypes.Any())
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();
            foreach (var entry in instructionTypes)
            {
                builder.Append(string.Format("instructionType={0}&", (int)entry));
            }
            builder.Length -= 1;
            return builder.ToString();
        }
    }
}
