namespace Tachyon.SDK.Consumer.Extensions
{
    using System.Net.Http;
    using Newtonsoft.Json;

    internal static class HttpContentJsonFormattingExtensions
    {
        public static T ReadAs<T>(this HttpContent content)
        {
            var stringContent = content.ReadAsStringAsync().Result;

            if (stringContent == null)
            {
                return default(T);
            }

            // For future reference - yes, primitive types WILL have double quotes around them
            // so your will have "True" instead of True but that is API side doing it, not the SDK.
            // The SDK must not try to de-serialize when T is string using jsonconvert because that
            // method will attempt to interpret the string as JSON even though it is being told it's not
            // meant to be an object but a string.
            if (typeof(T) == typeof(string))
            {
                return (T)((object)content.ReadAsStringAsync().Result);
            }

            return JsonConvert.DeserializeObject<T>(stringContent);
        }
    }
}
