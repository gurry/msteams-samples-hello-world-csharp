namespace Tachyon.SDK.Consumer.DefaultImplementations
{
    using System.Net;
    using System.Net.Http;

    using Tachyon.SDK.Consumer.ExternalInterfaces;

    /// <summary>
    /// <see cref="HttpClient"/> manager based on explict credentials.
    /// Useful when we don't want to use default credentials.
    /// </summary>
    public class ExplicitCredentialsHttpClientManager : IHttpClientManager
    {
        private readonly NetworkCredential credentials;

        /// <summary>
        /// Creates an instance of <see cref="ExplicitCredentialsHttpClientManager"/> class.
        /// </summary>
        /// <param name="credentials"><see cref="NetworkCredential"/> to be used</param>
        public ExplicitCredentialsHttpClientManager(NetworkCredential credentials)
        {
            this.credentials = credentials;
        }

        /// <summary>
        /// Creates an instance of <see cref="ExplicitCredentialsHttpClientManager"/> class.
        /// </summary>
        /// <param name="userName">userName of the credential to be used</param>
        /// <param name="password">password of the credential to be used</param>
        public ExplicitCredentialsHttpClientManager(string userName, string password)
        {
            this.credentials = new NetworkCredential(userName, password);
        }

        /// <summary>
        /// Returns a new instance of HttpClient.
        /// You can pre configure it with whatever credentials etc. you wish to use, but this must
        /// be a new instance that was not used to make Http calls before because it will be configured internally.
        /// </summary>
        /// <returns></returns>
        public HttpClient GetClient()
        {
            var messageHandler = new HttpClientHandler() { Credentials = this.credentials };

            var client = new HttpClient(messageHandler);

            return client;
        }

        /// <summary>
        /// Will be called in order to dispose of the instance of HttpClient previously obtained
        /// by calling the GetClient method.
        /// </summary>
        /// <param name="client"></param>
        public void DisposeOfClient(HttpClient client)
        {
            client.Dispose();
        }
    }
}