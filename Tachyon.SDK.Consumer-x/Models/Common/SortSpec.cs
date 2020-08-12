namespace Tachyon.SDK.Consumer.Models.Common
{
    /// <summary>
    /// Model defining sort settings
    /// </summary>
    public class SortSpec
    {
        /// <summary>
        /// Column to sort on
        /// </summary>
        public string Column { get; set; }
        /// <summary>
        /// Direction of the sort. ASC for ascending. DESC for descending.
        /// </summary>
        public string Direction { get; set; }
    }
}
