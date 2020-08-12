namespace Tachyon.SDK.Consumer.ExternalInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Tachyon.SDK.Consumer.Models.Api;
    /// <summary>
    /// Interface defining a transport proxy class that will be used to communicate with Consumer api of the Tachyon platform
    /// </summary>
    public interface ITransportProxy: IDisposable
    {
        /// <summary>
        /// Issues a GET request
        /// </summary>
        /// <typeparam name="TReceive">Type of data being received from the Api</typeparam>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>Object of TReceive class if successful or default value for that type id not.</returns>
        ApiCallResponse<TReceive> Get<TReceive>(string endpoint);
        /// <summary>
        /// Issues a POST request with specific body
        /// </summary>
        /// <typeparam name="TSend">Type of data being sent to the Api</typeparam>
        /// <typeparam name="TReceive">Type of data being received from the Api</typeparam>
        /// <param name="objectToPost">Object to post as the body of the request</param>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>Object of TReceive class if successful or default value for that type id not.</returns>
        ApiCallResponse<TReceive> Post<TSend, TReceive>(TSend objectToPost, string endpoint);
        /// <summary>
        /// Issues a POST request without any body
        /// </summary>
        /// <typeparam name="TReceive">Type of data being received from the Api</typeparam>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>Object of TReceive class if successful or default value for that type id not.</returns>
        ApiCallResponse<TReceive> PostEmpty<TReceive>(string endpoint);
        /// <summary>
        /// Issues a post with Form contents
        /// </summary>
        /// <typeparam name="TReceive">Type of data being received from the Api</typeparam>
        /// <param name="postFormElements">Form elements to post in key-value pairs</param>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>Object of TReceive class if successful or default value for that type id not.</returns>
        ApiCallResponse<TReceive> PostForm<TReceive>(Dictionary<string, string> postFormElements, string endpoint);
        /// <summary>
        /// Issues a post with Form contents and File contents
        /// </summary>
        /// <typeparam name="TReceive">Type of data being received from the Api</typeparam>
        /// <param name="postFormElements">Form elements to post in key-value pairs</param>
        /// <param name="postFormFiles">Files to be posted with the form in key-value pairs. Key is file name, value is the stream itself</param>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>Object of TReceive class if successful or default value for that type id not.</returns>
        ApiCallResponse<TReceive> PostForm<TReceive>(Dictionary<string, string> postFormElements, Dictionary<string, MemoryStream> postFormFiles, string endpoint);
        /// <summary>
        /// Issues a PUT request with specific body
        /// </summary>
        /// <typeparam name="TSend">Type of data being sent to the Api</typeparam>
        /// <typeparam name="TReceive">Type of data being received from the Api</typeparam>
        /// <param name="objectToPut">Object to use as the body of the request</param>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>Object of TReceive class if successful or default value for that type id not.</returns>
        ApiCallResponse<TReceive> Put<TSend, TReceive>(TSend objectToPut, string endpoint);
        /// <summary>
        /// Issues a DELETE request
        /// </summary>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns></returns>
        ApiCallResponse<bool> Delete(string endpoint);
        /// <summary>
        /// Issues a DELTE request with a specific body
        /// </summary>
        /// <typeparam name="TSend">Type of object to be sent</typeparam>
        /// <param name="objectToSend">Object to post with the body of the request</param>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns></returns>
        ApiCallResponse<bool> Delete<TSend>(TSend objectToSend, string endpoint);
        /// <summary>
        /// Uploads stream content
        /// </summary>
        /// <param name="streamToPost">stream to upload</param>
        /// <param name="fileName">Name of the file</param>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>String with the content of the entire response</returns>
        ApiCallResponse<string> UploadFile(MemoryStream streamToPost, string fileName, string endpoint);
        /// <summary>
        /// Downloads a file
        /// </summary>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>Memory Stream containing the file</returns>
        ApiCallResponse<MemoryStream> DownloadFile(string endpoint);
        /// <summary>
        /// Downloads a file. Includes a body in the request
        /// </summary>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <param name="objectToSend">Object to send with the request</param>
        /// <returns>Memory Stream containing the file</returns>
        ApiCallResponse<MemoryStream> DownloadFile<TSend>(string endpoint, TSend objectToSend);
        /// <summary>
        /// Sets the root address of the web api
        /// </summary>
        /// <param name="address">Address to use as the base address of the api. 
        /// For example, if you're using HttpClient this is what you would assign to its BaseAddress property.</param>
        void SetApiRootAddress(string address);
    }
}
