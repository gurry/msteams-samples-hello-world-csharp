namespace Tachyon.SDK.Consumer.Extensions
{
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using Newtonsoft.Json;

    internal static class HttpClientJsonFormattingExtensions
    {
        private const string MediaType = "application/json";

        public static HttpResponseMessage PostAs<T>(this HttpClient client, string endpoint, T objectToPost)
        {
            string serializedClass = JsonConvert.SerializeObject(objectToPost);
            return client.PostAsync(endpoint, new StringContent(serializedClass, Encoding.UTF8, MediaType), CancellationToken.None).Result;
        }

        public static HttpResponseMessage PutAs<T>(this HttpClient client, string endpoint, T objectToPost)
        {
            string serializedClass = JsonConvert.SerializeObject(objectToPost);
            return client.PutAsync(endpoint, new StringContent(serializedClass, Encoding.UTF8, MediaType), CancellationToken.None).Result;
        }
    }
}
