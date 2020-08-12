namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Model representing a user
    /// </summary>
    public class User
    {
        /// <summary>
        /// Principal name (as known to the AD)
        /// </summary>
        public string PrincipalName { get; set; }
        /// <summary>
        /// External ID (SID)
        /// </summary>
        public string ExternalId { get; set; }
        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// User Display name
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Photo in binary form
        /// </summary>
        public byte[] Photo { get; set; }
    }
}
