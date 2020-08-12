namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System.Collections.Generic;
    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Model representing a collection of Tasks and Task Group for a given Task Group
    /// </summary>
    public class TaskGroupByParent
    {
        /// <summary>
        /// Child task groups
        /// </summary>
        public IEnumerable<TaskGroup> ChildTaskGroups { get; set; }
        /// <summary>
        /// Tasks assigned to this task group
        /// </summary>
        public IEnumerable<Task> Tasks { get; set; }
    }
}
