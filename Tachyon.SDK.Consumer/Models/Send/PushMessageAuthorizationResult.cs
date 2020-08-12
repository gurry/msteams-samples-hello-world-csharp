namespace Tachyon.SDK.Consumer.Models.Send
{
    /// <summary>
    /// Model representing a result of a push message authorization request
    /// </summary>
    public class PushMessageAuthorizationResult
    {
        /// <summary>
        /// Request Id
        /// </summary>
        public int RequestId { get; set; }
        /// <summary>
        /// Request Guid
        /// </summary>
        public string RequestGuid { get; set; }
        /// <summary>
        /// Boolean flag indicating if the request was authorized (true) or not (false)
        /// </summary>
        public bool Authorized { get; set; }
        /// <summary>
        /// Any comments left by the user who handled the authorization request
        /// </summary>
        public string Comment { get; set; }
    }
}
