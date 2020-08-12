using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Tachyon.SDK.Consumer.ExternalInterfaces;

namespace Microsoft.Teams.Samples.HelloWorld.Web
{
    public class LogProxy<T>: ILogProxy
    {
        private readonly ILogger<T> _logger;

        public LogProxy(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogError(string text)
        {
            _logger.LogError(text);
        }

        public void LogWarning(string text)
        {
            _logger.LogWarning(text);
        }

        public void LogInfo(string text)
        {
            _logger.LogInformation(text);
        }
    }
}
