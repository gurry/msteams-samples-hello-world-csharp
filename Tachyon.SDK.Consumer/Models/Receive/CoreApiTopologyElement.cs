namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System.Collections.Generic;
    /// <summary>
    /// Model representing a single Core API within the tachyon setup topology
    /// </summary>
    public class CoreApiTopologyElement
    {
        /// <summary>
        /// Id of the Core API
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the Core API
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Collection of switches connected to this Core API
        /// </summary>
        public IEnumerable<SwitchTopologyElement> Switches { get; set; }
    }
}
