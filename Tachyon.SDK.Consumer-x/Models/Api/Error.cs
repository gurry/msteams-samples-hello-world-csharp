 namespace Tachyon.SDK.Consumer.Models.Api
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.Enums;
    /// <summary>
    /// Model of an error returned by the consumer api or exception caught by the SDK itself.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Error code
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Error data. Used for localization
        /// </summary>
        public IEnumerable<string> Data { get; set; }
        /// <summary>
        /// Error type
        /// </summary>
        public ErrorType ErrorType { get; set; }
    }
}
