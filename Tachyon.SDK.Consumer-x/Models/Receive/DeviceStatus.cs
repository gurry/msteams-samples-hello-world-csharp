namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Model representing a result of an authentication call
    /// </summary>
    public class DeviceStatus
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Domain this device is connected to
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Device status
        /// </summary>
        public int Status { get; set; }
    }
}
