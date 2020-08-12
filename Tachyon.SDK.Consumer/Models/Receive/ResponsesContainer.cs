namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System.Collections.Generic;
    /// <summary>
    /// Model representing a container holding responses
    /// </summary>
    public class ResponsesContainer
    {
        /// <summary>
        /// Range of responses returned. Can be used as start token in subsequent calls.
        /// </summary>
        public string Range { get; set; }
        /// <summary>
        /// List of responses
        /// </summary>
        public IEnumerable<Response> Responses { get; set; }
    }
}
