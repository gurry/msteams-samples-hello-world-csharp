namespace Tachyon.SDK.Consumer.Models.Send
{
    using System;
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Data for searching devices by Domains and Names
    /// </summary>
    public class DevicesDomainsAndNames
    {
        /// <summary>
        /// List of daomains and names
        /// </summary>
        public List<DomainAndNames> DomainsAndNames { get; set; }

        /// <summary>
        /// A domain and a list of names
        /// </summary>
        public class DomainAndNames
        {
            /// <summary>
            /// Domain to search
            /// </summary>
            public string Domain { get; set; }

            /// <summary>
            /// Names to search
            /// </summary>
            public List<string> Names { get; set; }
        }
    }
}
