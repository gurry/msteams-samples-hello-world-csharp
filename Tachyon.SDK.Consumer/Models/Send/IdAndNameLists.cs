namespace Tachyon.SDK.Consumer.Models.Send
{
    using System.Collections.Generic;

    /// <summary>
    /// Model representins a list or Ids or a list of names.
    /// Use one or the other.
    /// </summary>
    public class IdAndNameLists
    {
        /// <summary>
        /// Collection of Ids
        /// </summary>
        public IEnumerable<int> Ids { get; set; }
        /// <summary>
        /// Collection of Names
        /// </summary>
        public IEnumerable<string> Names { get; set; }
    }
}
