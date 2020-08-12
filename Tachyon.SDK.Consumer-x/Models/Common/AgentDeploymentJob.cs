namespace Tachyon.SDK.Consumer.Models.Common
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model representing details of a single Agent Deployment Job
    /// </summary>
    public class AgentDeploymentJob
    {
        /// <summary>
        /// Job Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Job description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// Current workflow state of the job
        /// </summary>
        public int WorkflowState { get; set; }
        /// <summary>
        /// Current status of the job
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// User this job was created by
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Timestamp of when this job was created
        /// </summary>
        public DateTime CreatedTimestampUtc { get; set; }
        /// <summary>
        /// Credentials to be used when executing the job. These credentials will be used with all machines that are part of this job.
        /// </summary>
        public Credentials Credentials { get; set; }
        /// <summary>
        /// URL to call when this job finishes
        /// </summary>
        public string CallbackUrl { get; set; }
        /// <summary>
        /// Any custom data the calling consumer wishes to store with this job. Not used by Tachyon platform.
        /// </summary>
        public string ConsumerCustomData { get; set; }
        /// <summary>
        /// Settings to be applied to the agent installer.
        /// This should contain key-value pairs with the key being setting name and value being the value for that setting
        /// </summary>
        /// <example>
        /// Usually you would configure switch and background channel addresses as a minimum:
        /// Key = "SWITCH", Value = "TachyonSwitch.SomeDomain.com"
        /// Key = "BACKGROUNDCHANNELURL", Value = "https://TachyonServer.SomeDomain.com:443/Background"
        /// </example>
        public Dictionary<string, string> AgentSettings { get; set; }
    }
}
