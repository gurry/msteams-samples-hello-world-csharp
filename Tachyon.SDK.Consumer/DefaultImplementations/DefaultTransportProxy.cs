namespace Tachyon.SDK.Consumer.DefaultImplementations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;

    using Newtonsoft.Json;

    using Tachyon.SDK.Consumer.Extensions;
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Tools;

    /// <summary>
    /// Default implementation of transport proxy
    /// </summary>
    public class DefaultTransportProxy : ITransportProxy
    {
        private readonly ILogProxy logger;
        private readonly bool loggerAvailable = false;
        private readonly string consumerIdHeaderField = "X-Tachyon-Consumer";
        private readonly HttpClient client;
        private string apiRootAddress;
        private readonly IHttpClientManager clientManager;

        /// <summary>
        /// Default constructor. This will use DefaultHttpClientManager to handle HttpClient lifecycle.
        /// </summary>
        /// <param name="logger">Optional. Implementation of a logger proxy if you wish to receive debug info from this class.</param>
        /// <param name="consumerName">Identification string of the consumer using this class</param>
        /// <param name="apiRootAddress">Root of the consumer Api</param>
        public DefaultTransportProxy(ILogProxy logger, string consumerName, string apiRootAddress) : this(logger, consumerName, apiRootAddress, new DefaultHttpClientManager(), false)
        {
        }

        /// <summary>
        /// Constructor. This will use provided HttpClientManager to handle HttpClient lifecycle.
        /// </summary>
        /// <param name="logger">Optional. Implementation of a logger proxy if you wish to receive debug info from this class.</param>
        /// <param name="consumerName">Identification string of the consumer using this class</param>
        /// <param name="clientManager">HttpClientManager that this class should use to manage the lifecycle of HttpClient object used by this class</param>
        /// <remarks>There is no need to set the BaseAddress property of the HttpClient your HttpClientManager provides. This will be supplied by TachyonConnector.</remarks>
        public DefaultTransportProxy(ILogProxy logger, string consumerName, IHttpClientManager clientManager) : this(logger, consumerName, null, clientManager, true)
        {
        }

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="logger">Optional. Implementation of a logger proxy if you wish to receive debug info from this class.</param>
        /// <param name="consumerId">Identification string of the consumer using this class</param>
        /// <param name="apiRootAddress">Root of the consumer Api</param>
        /// <param name="clientManager">HttpClientManager implementation that will be used to obtain an instance of HttpClient</param>
        /// <param name="customManager">Indicates if a constructor that was passed a custom HttpClientManager called this constructor</param>
        private DefaultTransportProxy(ILogProxy logger, string consumerId, string apiRootAddress, IHttpClientManager clientManager, bool customManager)
        {
            if (consumerId == null)
            {
                throw new ArgumentNullException("consumerId");
            }

            if (!customManager && apiRootAddress == null)
            {
                throw new ArgumentNullException("apiRootAddress");
            }

            if (clientManager == null)
            {
                throw new ArgumentNullException("clientManager");
            }

            this.apiRootAddress = apiRootAddress;
            this.logger = logger;
            this.clientManager = clientManager;
            if (logger != null)
            {
                this.loggerAvailable = true;
            }

            this.client = this.clientManager.GetClient();

            if (this.client == null)
            {
                throw new Exception("Cannot obtain an instance of HttpClient from IHttpClientManager. A null was returned by GetClient method.");
            }

            if (!customManager && !string.IsNullOrEmpty(apiRootAddress))
            {
                this.client.BaseAddress = new Uri(apiRootAddress);
            }

            this.client.DefaultRequestHeaders.Add(this.consumerIdHeaderField, consumerId);
        }

        /// <summary>
        /// Issues a GET request
        /// </summary>
        /// <typeparam name="TReceive">Type of data being received from the Api</typeparam>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>Object of TReceive class if successful or default value for that type id not.</returns>
        public ApiCallResponse<TReceive> Get<TReceive>(string endpoint)
        {
            HttpStatusCode? responseStatusCode = null;
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = this.client.GetAsync(endpoint).Result;
                responseStatusCode = response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    var returnObject = response.Content.ReadAs<TReceive>();
                    return new ApiCallResponse<TReceive>(returnObject, true, null, response.StatusCode);
                }
                else
                {
                    if (this.loggerAvailable)
                    {
                        var msg = response.Content.ReadAsStringAsync().Result; 
                        this.logger.LogError(
                            string.Format("Tachyon Api GET call to {0}{1} failed with code {2}. Raw message: {3}",
                                this.apiRootAddress,
                                endpoint,
                                response.StatusCode,
                                msg));
                    }

                    return new ApiCallResponse<TReceive>(default(TReceive), false, ErrorCreator.CreateErrorObject(response), response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new ApiCallResponse<TReceive>(default(TReceive), false, ErrorCreator.CreateErrorObject(ex), responseStatusCode);
            }
        }

        /// <summary>
        /// Issues a POST request with specific body
        /// </summary>
        /// <typeparam name="TSend">Type of data being sent to the Api</typeparam>
        /// <typeparam name="TReceive">Type of data being received from the Api</typeparam>
        /// <param name="objectToPost">Object to post as the body of the request</param>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>Object of TReceive class if successful or default value for that type id not.</returns>
        public ApiCallResponse<TReceive> Post<TSend, TReceive>(TSend objectToPost, string endpoint)
        {
            HttpStatusCode? responseStatusCode = null;
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = this.client.PostAs<TSend>(endpoint, objectToPost);
                responseStatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    var returnObject = response.Content.ReadAs<TReceive>();
                    return new ApiCallResponse<TReceive>(returnObject, true, null, response.StatusCode);
                }
                else
                {
                    if (this.loggerAvailable)
                    {
                        var msg = response.Content.ReadAsStringAsync().Result;
                        this.logger.LogError(
                            string.Format("Tachyon Api POST call to {0}{1} failed with code {2}. Raw message: {3}",
                                this.apiRootAddress,
                                endpoint,
                                response.StatusCode,
                                msg));
                    }

                    return new ApiCallResponse<TReceive>(default(TReceive), false, ErrorCreator.CreateErrorObject(response), response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new ApiCallResponse<TReceive>(default(TReceive), false, ErrorCreator.CreateErrorObject(ex), responseStatusCode);
            }
        }

        /// <summary>
        /// Issues a POST request without any body
        /// </summary>
        /// <typeparam name="TReceive">Type of data being received from the Api</typeparam>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>Object of TReceive class if successful or default value for that type id not.</returns>
        public ApiCallResponse<TReceive> PostEmpty<TReceive>(string endpoint)
        {
            HttpStatusCode? responseStatusCode = null;
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = this.client.PostAsync(endpoint, new StringContent(string.Empty)).Result;
                responseStatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    var returnObject = response.Content.ReadAs<TReceive>();
                    return new ApiCallResponse<TReceive>(returnObject, true, null, response.StatusCode);
                }
                else
                {
                    if (this.loggerAvailable)
                    {
                        this.logger.LogError(
                            string.Format("Tachyon Api POST call to {0}{1} failed with code {2}. Raw message: {3}",
                                this.apiRootAddress,
                                endpoint,
                                response.StatusCode,
                                response.Content.ReadAsStringAsync().Result));
                    }

                    return new ApiCallResponse<TReceive>(default(TReceive), false, ErrorCreator.CreateErrorObject(response), response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new ApiCallResponse<TReceive>(default(TReceive), false, ErrorCreator.CreateErrorObject(ex), responseStatusCode);
            }
        }

        /// <summary>
        /// Issues a post with Form contents
        /// </summary>
        /// <typeparam name="TReceive">Type of data being received from the Api</typeparam>
        /// <param name="postFormElements">Form elements to post in key-value pairs</param>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>Object of TReceive class if successful or default value for that type id not.</returns>
        public ApiCallResponse<TReceive> PostForm<TReceive>(Dictionary<string, string> postFormElements, string endpoint)
        {
            using (var formData = new MultipartFormDataContent())
            {
                HttpStatusCode? responseStatusCode = null;
                foreach (var formElement in postFormElements)
                {
                    if (formElement.Value != null)
                    {
                        formData.Add(new StringContent(formElement.Value), string.Format("\"{0}\"", formElement.Key));
                    }
                }
                try
                {
                    HttpResponseMessage response = this.client.PostAsync(endpoint, formData).Result;
                    responseStatusCode = response.StatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        var returnObject = response.Content.ReadAs<TReceive>();
                        return new ApiCallResponse<TReceive>(returnObject, true, null, response.StatusCode);
                    }
                    else
                    {
                        if (this.loggerAvailable)
                        {
                            this.logger.LogError(
                                string.Format("Tachyon Api POST call to {0}{1} failed with code {2}. Raw message: {3}",
                                    this.apiRootAddress,
                                    endpoint,
                                    response.StatusCode,
                                    response.Content.ReadAsStringAsync().Result));
                        }

                        return new ApiCallResponse<TReceive>(default(TReceive), false, ErrorCreator.CreateErrorObject(response), response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    return new ApiCallResponse<TReceive>(default(TReceive), false, ErrorCreator.CreateErrorObject(ex), responseStatusCode);
                }
            }
        }

        /// <summary>
        /// Issues a post with Form contents and File contents
        /// </summary>
        /// <typeparam name="TReceive">Type of data being received from the Api</typeparam>
        /// <param name="postFormElements">Form elements to post in key-value pairs</param>
        /// <param name="postFormFiles">Files to be posted with the form in key-value pairs. Key is file name, value is the stream itself</param>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>Object of TReceive class if successful or default value for that type id not.</returns>
        public ApiCallResponse<TReceive> PostForm<TReceive>(Dictionary<string, string> postFormElements, Dictionary<string, MemoryStream> postFormFiles, string endpoint)
        {
            using (var formData = new MultipartFormDataContent())
            {
                HttpStatusCode? responseStatusCode = null;
                foreach (var formElement in postFormElements)
                {
                    if (formElement.Value != null)
                    {
                        formData.Add(new StringContent(formElement.Value), string.Format("\"{0}\"", formElement.Key));
                    }
                }
                foreach (var fileEntry in postFormFiles)
                {
                    formData.Add(new StreamContent(fileEntry.Value), fileEntry.Key, fileEntry.Key);
                }
                try
                {
                    HttpResponseMessage response = this.client.PostAsync(endpoint, formData).Result;
                    responseStatusCode = response.StatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        var returnObject = response.Content.ReadAs<TReceive>();
                        return new ApiCallResponse<TReceive>(returnObject, true, null, response.StatusCode);
                    }
                    else
                    {
                        if (this.loggerAvailable)
                        {
                            this.logger.LogError(
                                string.Format("Tachyon Api POST call to {0}{1} failed with code {2}. Raw message: {3}",
                                    this.apiRootAddress,
                                    endpoint,
                                    response.StatusCode,
                                    response.Content.ReadAsStringAsync().Result));
                        }

                        return new ApiCallResponse<TReceive>(default(TReceive), false, ErrorCreator.CreateErrorObject(response), response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    return new ApiCallResponse<TReceive>(default(TReceive), false, ErrorCreator.CreateErrorObject(ex), responseStatusCode);
                }
            }
        }

        /// <summary>
        /// Issues a PUT request with specific body
        /// </summary>
        /// <typeparam name="TSend">Type of data being sent to the Api</typeparam>
        /// <typeparam name="TReceive">Type of data being received from the Api</typeparam>
        /// <param name="objectToPut">Object to use as the body of the request</param>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>Object of TReceive class if successful or default value for that type id not.</returns>
        public ApiCallResponse<TReceive> Put<TSend, TReceive>(TSend objectToPut, string endpoint)
        {
            HttpStatusCode? responseStatusCode = null;
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = this.client.PutAs<TSend>(endpoint, objectToPut);
                responseStatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    var returnObject = response.Content.ReadAs<TReceive>();
                    return new ApiCallResponse<TReceive>(returnObject, true, null, response.StatusCode);
                }
                else
                {
                    if (this.loggerAvailable)
                    {
                        this.logger.LogError(
                            string.Format("Tachyon Api PUT call to {0}{1} failed with code {2}. Raw message: {3}",
                                this.apiRootAddress,
                                endpoint,
                                response.StatusCode,
                                response.Content.ReadAsStringAsync().Result));
                    }

                    return new ApiCallResponse<TReceive>(default(TReceive), false, ErrorCreator.CreateErrorObject(response), response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new ApiCallResponse<TReceive>(default(TReceive), false, ErrorCreator.CreateErrorObject(ex), responseStatusCode);
            }
        }

        /// <summary>
        /// Issues a DELETE request
        /// </summary>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns></returns>
        public ApiCallResponse<bool> Delete(string endpoint)
        {
            HttpStatusCode? responseStatusCode = null;
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = this.client.DeleteAsync(endpoint).Result;
                responseStatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    return new ApiCallResponse<bool>(true, true, null, response.StatusCode);
                }
                else
                {
                    if (this.loggerAvailable)
                    {
                        this.logger.LogError(
                            string.Format(
                                "Tachyon Api DELETE call to {0}{1} failed with code {2}. Raw message: {3}",
                                this.apiRootAddress,
                                endpoint,
                                response.StatusCode,
                                response.Content.ReadAsStringAsync().Result));
                    }

                    return new ApiCallResponse<bool>(false, false, ErrorCreator.CreateErrorObject(response), response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new ApiCallResponse<bool>(false, false, ErrorCreator.CreateErrorObject(ex), responseStatusCode);
            }
        }

        /// <summary>
        /// Issues a DELETE request
        /// </summary>
        /// <param name="objectToSend">Object to use as the body of the request</param>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns></returns>
        public ApiCallResponse<bool> Delete<TSend>(TSend objectToSend, string endpoint)
        {
            HttpStatusCode? responseStatusCode = null;
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, endpoint);
                message.Content = new StringContent(JsonConvert.SerializeObject(objectToSend), Encoding.UTF8, "application/json");
                HttpResponseMessage response = this.client.SendAsync(message).Result;
                responseStatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    return new ApiCallResponse<bool>(true, true, null, response.StatusCode);
                }
                else
                {
                    if (this.loggerAvailable)
                    {
                        this.logger.LogError(
                            string.Format(
                                "Tachyon Api DELETE call to {0}{1} failed with code {2}. Raw message: {3}",
                                this.apiRootAddress,
                                endpoint,
                                response.StatusCode,
                                response.Content.ReadAsStringAsync().Result));
                    }

                    return new ApiCallResponse<bool>(false, false, ErrorCreator.CreateErrorObject(response), response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new ApiCallResponse<bool>(false, false, ErrorCreator.CreateErrorObject(ex), responseStatusCode);
            }
        }

        /// <summary>
        /// Uploads stream content
        /// </summary>
        /// <param name="streamToPost">stream to upload</param>
        /// <param name="fileName">Name of the file</param>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>String with the content of the entire response</returns>
        public ApiCallResponse<string> UploadFile(MemoryStream streamToPost, string fileName, string endpoint)
        {
            HttpStatusCode? responseStatusCode = null;
            HttpContent fileStreamContent = new StreamContent(streamToPost);
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent, fileName, fileName);
                try
                {
                    var response = this.client.PostAsync(endpoint, formData).Result;
                    responseStatusCode = response.StatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        var returnObject = response.Content.ReadAsStringAsync().Result;
                        return new ApiCallResponse<string>(returnObject, true, null, response.StatusCode);
                    }
                    else
                    {
                        if (this.loggerAvailable)
                        {
                            this.logger.LogError(
                                string.Format("Tachyon Api POST call to {0}{1} failed with code {2}. Raw message: {3}",
                                    this.apiRootAddress,
                                    endpoint,
                                    response.StatusCode,
                                    response.Content.ReadAsStringAsync().Result));
                        }

                        return new ApiCallResponse<string>(string.Empty, false, ErrorCreator.CreateErrorObject(response), response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    return new ApiCallResponse<string>(string.Empty, false, ErrorCreator.CreateErrorObject(ex), responseStatusCode);
                }
            }
        }

        /// <summary>
        /// Downloads a file
        /// </summary>
        /// <param name="endpoint">Specific endpoint to call</param>
        /// <returns>Memory Stream containing the file</returns>
        public ApiCallResponse<MemoryStream> DownloadFile(string endpoint)
        {
            HttpStatusCode? responseStatusCode = null;
            try
            {
                var response = this.client.GetAsync(endpoint).Result;
                responseStatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    var returnObject = response.Content.ReadAsStreamAsync().Result;
                    var returnObjectAsMemoryStream = new MemoryStream();
                    returnObject.CopyTo(returnObjectAsMemoryStream);
                    return new ApiCallResponse<MemoryStream>(returnObjectAsMemoryStream, true, null, response.StatusCode);
                }
                else
                {
                    if (this.loggerAvailable)
                    {
                        this.logger.LogError(
                            string.Format("Tachyon Api GET call to {0}{1} failed with code {2}. Raw message: {3}",
                                this.apiRootAddress,
                                endpoint,
                                response.StatusCode,
                                response.Content.ReadAsStringAsync().Result));
                    }

                    return new ApiCallResponse<MemoryStream>(null, false, ErrorCreator.CreateErrorObject(response), response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new ApiCallResponse<MemoryStream>(null, false, ErrorCreator.CreateErrorObject(ex), responseStatusCode);
            }
        }

        /// <summary>
        /// Download a file
        /// </summary>
        /// <typeparam name="TSend"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="objectToSend"></param>
        /// <returns></returns>
        public ApiCallResponse<MemoryStream> DownloadFile<TSend>(string endpoint, TSend objectToSend)
        {
            HttpStatusCode? responseStatusCode = null;
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var response = this.client.PostAs(endpoint, objectToSend);
                responseStatusCode = response.StatusCode;
                if (response.IsSuccessStatusCode)
                {
                    var returnObject = response.Content.ReadAsStreamAsync().Result;
                    var returnObjectAsMemoryStream = new MemoryStream();
                    returnObject.CopyTo(returnObjectAsMemoryStream);
                    return new ApiCallResponse<MemoryStream>(returnObjectAsMemoryStream, true, null, response.StatusCode);
                }
                else
                {
                    if (this.loggerAvailable)
                    {
                        this.logger.LogError(
                            string.Format("Tachyon Api GET call to {0}{1} failed with code {2}. Raw message: {3}",
                                this.apiRootAddress,
                                endpoint,
                                response.StatusCode,
                                response.Content.ReadAsStringAsync().Result));
                    }

                    return new ApiCallResponse<MemoryStream>(null, false, ErrorCreator.CreateErrorObject(response), response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return new ApiCallResponse<MemoryStream>(null, false, ErrorCreator.CreateErrorObject(ex), responseStatusCode);
            }
        }

        /// <summary>
        /// Set the API root address
        /// </summary>
        /// <param name="address"></param>
        public void SetApiRootAddress(string address)
        {
            this.apiRootAddress = address;
            this.client.BaseAddress = new Uri(address);
        }

        /// <summary>
        /// Should be invoked when the instance is no longer needed
        /// </summary>
        public void Dispose()
        {
            this.clientManager.DisposeOfClient(this.client);
        }
    }
}
