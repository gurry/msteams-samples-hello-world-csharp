namespace Tachyon.SDK.Consumer.Models.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// Model representing a Task Group
    /// </summary>
    public class TaskGroup
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
        /// Id of the parent Task Group. Null if this is Task Group has no parent.
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// Collection of child Task Groups
        /// </summary>
        public IEnumerable<TaskGroup> ChildTaskGroups { get; set; }
        /// <summary>
        /// Collection of child Tasks
        /// </summary>
        public IEnumerable<Task> Tasks { get; set; }
    }
}
