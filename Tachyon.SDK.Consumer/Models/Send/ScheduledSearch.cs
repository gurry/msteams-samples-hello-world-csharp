namespace Tachyon.SDK.Consumer.Models.Send
{
    using System;
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Model to be used with search functionality for ScheduledInstructions
    /// </summary>
    public class ScheduledSearch
    {
        /// <summary>
        /// Filter expression
        /// </summary>
        public ExpressionObject Filter { get; set; }
        /// <summary>
        /// Start date
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// End date
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Maximum number of entries returned
        /// </summary>
        public int? PageSize { get; set; }
    }
}
