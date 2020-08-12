namespace Tachyon.SDK.Consumer.Models.Common
{
    /// <summary>
    /// Model defining an Active Directory principal
    /// </summary>
    public class PrincipalSearchItem
    {
        /// <summary>
        /// Principal Name
        /// </summary>
        public string PrincipalName { get; set; }
        /// <summary>
        /// External Id (SID)
        /// </summary>
        public string ExternalId { get; set; }
        /// <summary>
        /// Email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Flag defining if this principal is a group
        /// </summary>
        public bool IsGroup { get; set; }
    }
}
