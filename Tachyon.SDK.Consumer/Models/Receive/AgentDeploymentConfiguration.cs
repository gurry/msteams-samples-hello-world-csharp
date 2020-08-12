namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model representing the data that is received from the server when calling the API that returns the configuration for Remote Agent Deployment
    /// </summary>
    public class AgentDeploymentConfiguration
    {
        /// <summary>
        /// List of switches available to the Tachyon Server
        /// </summary>
        public List<Switch> Switches { get; set; }

        /// <summary>
        /// List of switches available to the Tachyon Server
        /// </summary>
        public List<BackgroundChannel> BackgroundChannels { get; set; }

        /// <summary>
        /// Represents one switch as returned from the server API
        /// </summary>
        public class Switch
        {
            /// <summary>
            /// Internal ID of the switch
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// The FQDN of the swithc, such as "tachyon.acme.local"
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// The TCP port for switch communications (usually 4000)
            /// </summary>
            public int Port { get; set; }
        }

        /// <summary>
        /// Represents one BackgroundChannel as returned from the server API
        /// </summary>
        public class BackgroundChannel
        {
            /// <summary>
            /// Internal ID of the BackgroundChannel
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// The URL of the BackgroundChannel, such as "https://tachyon.acme.local:443/BackgroundChannel/"
            /// </summary>
            public string ResourceUrl { get; set; }
        }
    }
}
