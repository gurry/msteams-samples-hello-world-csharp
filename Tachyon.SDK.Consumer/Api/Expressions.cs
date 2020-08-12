namespace Tachyon.SDK.Consumer.Api
{
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;
    using Tachyon.SDK.Consumer.Models.Send;

    /// <summary>
    /// Abstracts Expressions controller of Consumer API
    /// </summary>
    public class Expressions : ApiBase
    {
        private readonly string getReadableEndpoint = "Expressions/GetReadable";
        private readonly string checkScopeEndpoint = "Expressions/CheckScope";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public Expressions(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Converts give expression to a human readable version
        /// </summary>
        /// <param name="expression">Expression to convert</param>
        /// <returns>string containing the converted expression</returns>
        /// <remarks>Calls POST on apiRoot/Expressions/GetReadable</remarks>
        public ApiCallResponse<string> ConvertExpressionToReadableString(ExpressionObject expression)
        {
            var apiCallResult = this.transportProxy.Post<ExpressionObject, string>(expression, this.getReadableEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Expressions Api 'ConvertExpressionToReadableString' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Check if two given expressions can be merged without errors
        /// </summary>
        /// <param name="expressionPair">Object containing two expressions</param>
        /// <returns>Calls POST on apiRoot/Expressions/CheckScope</returns>
        public ApiCallResponse<string> CheckIfExpressionsCanBeMerged(ExpressionPair expressionPair)
        {
            var apiCallResult = this.transportProxy.Post<ExpressionPair, string>(expressionPair, this.checkScopeEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Expressions Api 'CheckIfExpressionsCanBeMerged' call failed");
            }

            return apiCallResult;
        }
    }
}
