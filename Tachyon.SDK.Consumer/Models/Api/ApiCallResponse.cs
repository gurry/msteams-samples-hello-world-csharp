namespace Tachyon.SDK.Consumer.Models.Api
{
    using System.Collections.Generic;
    using System.Net;
    
    /// <summary>
    /// Model used for all responses coming from the consumer api
    /// </summary>
    /// <typeparam name="T">Type of the object returned by the api</typeparam>
    public class ApiCallResponse<T>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="receivedObject">Object received from the api</param>
        /// <param name="successfullCall">Flag indicating id the api call itself was successful</param>
        /// <param name="errors">Errors received from the api caught by the SDK</param>
        /// <param name="responseStatusCode">HTTP status code received with the response</param>
        public ApiCallResponse(T receivedObject, bool successfullCall, IEnumerable<Error> errors, HttpStatusCode? responseStatusCode)
        {
            this.ReceivedObject = receivedObject;
            this.Success = successfullCall;
            this.Errors = errors;
            this.ResponseStatusCode = responseStatusCode;
        }
        /// <summary>
        /// Object received from the api
        /// </summary>
        public T ReceivedObject { get; private set; }
        /// <summary>
        /// True if the call was successful. False otherwise.
        /// Please note a call can be successful even if nothing is returned from the api.
        /// </summary>
        public bool Success { get; private set; }
        /// <summary>
        /// Collection of errors received from the consumer api or caught by the SDK
        /// </summary>
        public IEnumerable<Error> Errors { get; private set; }
        /// <summary>
        /// HTTP status code received with the response.
        /// If this field is null it is most likely because error(s) have occurred before or while making
        /// the HTTP call itself, which means no HTTP error code was returned.
        /// </summary>
        public HttpStatusCode? ResponseStatusCode { get; private set; }
    }
}
