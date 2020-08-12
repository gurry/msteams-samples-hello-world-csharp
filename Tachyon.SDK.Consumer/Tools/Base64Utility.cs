namespace Tachyon.SDK.Consumer.Tools
{
    using System;
    using System.Text;

    /// <summary>
    /// Utility class for encoding and decoding Base64 data
    /// </summary>
    public static class Base64Utility
    {
        /// <summary>
        /// Encodes the string into a URL/URI compliant Base64 string
        /// Will encode using UTF8
        /// </summary>
        /// <param name="value">Value to be converted</param>
        /// <returns>Base64 encoded and sanitized version of the input string</returns>
        public static string ToUrlSafeBase64(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var bytes = Encoding.UTF8.GetBytes(value);
            var base64String = Convert.ToBase64String(bytes);

            string urlSafeString = base64String.Replace("=", string.Empty).Replace("/", "_").Replace("+", "-");
            return urlSafeString;
        }

        /// <summary>
        /// Convert from Base64 to string
        /// Assumes UTF8 encoding
        /// </summary>
        /// <param name="value">Base64 encoded string</param>
        /// <returns>Decoded string</returns>
        public static string FromBase64(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var decodedArray = Convert.FromBase64String(value);
            var decodedValue = Encoding.UTF8.GetString(decodedArray);
            return decodedValue;
        }
    }
}
