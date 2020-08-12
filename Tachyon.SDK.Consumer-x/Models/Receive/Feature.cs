namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Single licensed feature
    /// </summary>
    public class Feature
    {
        /// <summary>
        /// Collection of licensed instructions
        /// </summary>
        public IEnumerable<LicenseInstruction> Instructions { get; set; }
        /// <summary>
        /// Collection of licensed consumers
        /// </summary>
        public IEnumerable<LicenseConsumer> Consumers { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Expiry date
        /// </summary>
        public DateTime? Expiry { get; set; }
        /// <summary>
        /// Maximum count
        /// </summary>
        public string MaxCount { get; set; }
    }
}
