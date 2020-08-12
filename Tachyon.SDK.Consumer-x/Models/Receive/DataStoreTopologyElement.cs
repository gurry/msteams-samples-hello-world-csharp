namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System.Collections.Generic;
    /// <summary>
    /// Model representing a single data store within the tachyon setup topology
    /// </summary>
    public class DataStoreTopologyElement
    {
        /// <summary>
        /// Id of the data store
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the data store
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Provider name used by this data store
        /// </summary>
        public string ProviderName { get; set; }
        /// <summary>
        /// Collection of Core APIs connected to this data store
        /// </summary>
        public IEnumerable<CoreApiTopologyElement> Cores { get; set; }
    }
}
