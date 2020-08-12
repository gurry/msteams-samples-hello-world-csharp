namespace Tachyon.SDK.Consumer.DefaultImplementations
{
    using System.Net.Http;
    using Tachyon.SDK.Consumer.ExternalInterfaces;

    /// <summary>
    /// Default implementation of Http Client Manager
    /// </summary>
    public class DefaultHttpClientManager : IHttpClientManager
    {
        /// <summary>
        /// Returns a new instance of HttpClient using default credentials
        /// </summary>
        /// <returns>HttpClient</returns>
        public HttpClient GetClient()
        {
            var handler = new HttpClientHandler { UseDefaultCredentials = true };
            return new HttpClient(handler);
        }

        /// <summary>
        /// Disposes of the HttpClient
        /// </summary>
        /// <param name="client">Client to dispose of. Should be the same one that was obtained from GetClient method.</param>
        public void DisposeOfClient(HttpClient client)
        {
            client.Dispose();
        }
    }
}
