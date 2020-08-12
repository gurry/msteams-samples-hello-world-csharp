namespace Tachyon.SDK.Consumer.Models.Send
{
    /// <summary>
    /// Model representing authentication token
    /// </summary>
    public class AuthenticationToken
    {
        /// <summary>
        /// Item id for which token has been issued
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Token string
        /// </summary>
        public string Token { get; set; }
    }
}
