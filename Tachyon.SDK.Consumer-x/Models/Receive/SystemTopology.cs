namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System.Collections.Generic;
    /// <summary>
    /// Model representing the tachyon setup topology
    /// </summary>
    public class SystemTopology
    {
        /// <summary>
        /// List of data stores
        /// </summary>
        public IEnumerable<DataStoreTopologyElement> DataStores { get; set; }
    }
}
