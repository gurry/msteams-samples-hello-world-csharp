namespace Tachyon.SDK.Consumer.Models.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// Model representing a complete Agent Deployment Job, which includes both the job itself and the list of devices to which Agents should be deployed
    /// </summary>
    public class AgentDeployment
    {
        /// <summary>
        /// General configuration of the Deployment job
        /// </summary>
        public AgentDeploymentJob Job { get; set; }

        /// <summary>
        /// List of computers where Agents should be deployed
        /// </summary>
        public List<AgentDeploymentDevice> Devices { get; set; }
    }
}