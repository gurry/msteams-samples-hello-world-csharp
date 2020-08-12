namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Model representing a single switch within the tachyon setup topology
    /// </summary>
    public class SwitchTopologyElement
    {
        /// <summary>
        /// Id of the switch
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the switch
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Number of slots
        /// </summary>
        public int? Slots { get; set; }
        /// <summary>
        /// Number of agents connected to this switch
        /// </summary>
        public int Agents { get; set; }
    }
}
