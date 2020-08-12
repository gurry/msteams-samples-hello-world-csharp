namespace Tachyon.SDK.Consumer.ExternalInterfaces
{
    using System.Net.Http;

    /// <summary>
    /// This interface represents a contract for management of lifecycle of a HttpClient.
    /// </summary>
    public interface IHttpClientManager
    {
        /// <summary>
        /// Returns a new instance of HttpClient.
        /// You can pre configure it with whatever credentials etc. you wish to use, but this must
        /// be a new instance that was not used to make Http calls before because it will be configured internally.
        /// </summary>
        /// <returns></returns>
        HttpClient GetClient();
        /// <summary>
        /// Will be called in order to dispose of the instance of HttpClient previously obtained
        /// by calling the GetClient method.
        /// </summary>
        /// <param name="client"></param>
        void DisposeOfClient(HttpClient client);
    }
}
