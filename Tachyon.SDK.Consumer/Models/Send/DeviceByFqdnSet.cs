namespace Tachyon.SDK.Consumer.Models.Send
{
    /// <summary>
    /// Model used when getting aggregated impact statistics using a list of device Fqdns
    /// </summary>
    public class DeviceByFqdnSet
    {
        /// <summary>
        /// Fqdn list
        /// </summary>
        public string Scope { get; set; } // Was FqdnList, but that failed because it doesn't match the property name used by the Consumer API.
        /// <summary>
        /// Aggregate flag. Not used at this time.
        /// </summary>
        public int? Aggregate { get; set; }
    }
}
