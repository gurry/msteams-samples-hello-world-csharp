namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System.Collections.Generic;
    /// <summary>
    /// Model representing a container holding other / error responses
    /// </summary>
    public class ResponseErrorContainer
    {
        /// <summary>
        /// Range of responses returned. Can be used as start token in subsequent calls.
        /// </summary>
        public string Range { get; set; }
        /// <summary>
        /// List of other responses / errors
        /// </summary>
        public IEnumerable<ResponseError> Responses { get; set; }
    }
}
