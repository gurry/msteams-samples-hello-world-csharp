namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Information about feature's expiration
    /// </summary>
    public class FeatureExpiry
    {
        /// <summary>
        /// Number of days left till the future expires
        /// </summary>
        public int DaysLeft { get; set; }
        /// <summary>
        /// Expiry date time
        /// </summary>
        public string ExpiryDateTime { get; set; }
    }
}
