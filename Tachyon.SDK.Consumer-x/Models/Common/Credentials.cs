namespace Tachyon.SDK.Consumer.Models.Common
{
    /// <summary>
    /// Model representing user credentials
    /// Note that passwords are usually sent as plain text but received encrypted or not received at all for security reasons.
    /// Please refer to particular endpoint's documentation for details.
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
