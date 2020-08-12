namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Model representing detailed system statistics
    /// </summary>
    public class DetailSystemStatistics
    {
        /// <summary>
        /// Number of connected devices
        /// </summary>
        public int ConnectedDeviceCount { get; set; }
        /// <summary>
        /// Number of all devices regardless of state
        /// </summary>
        public int AllDeviceCount { get; set; }
        /// <summary>
        /// Number of devices that have been connected in the last seven days
        /// </summary>
        public int ConnectedDeviceLastSevenDaysCount { get; set; }
        /// <summary>
        /// Number of questions regardless of state
        /// </summary>
        public int AllQuestionCount { get; set; }
        /// <summary>
        /// Number of questions in progress
        /// </summary>
        public int QuestionInProgressCount { get; set; }
        /// <summary>
        /// Number of questions with available responses
        /// </summary>
        public int QuestionResponseAvailableCount { get; set; }
        /// <summary>
        /// Number of actions regardless of state
        /// </summary>
        public int AllActionCount { get; set; }
        /// <summary>
        /// Number of actions with in progress
        /// </summary>
        public int ActionInProgressCount { get; set; }
        /// <summary>
        /// Number of actions with available responses
        /// </summary>
        public int ActionResponseAvailableCount { get; set; }
        /// <summary>
        /// Number of events regardless of state
        /// </summary>
        public int AllEventCount { get; set; }
        /// <summary>
        /// Number of events in progress
        /// </summary>
        public int EventsInProgressCount { get; set; }
        /// <summary>
        /// Number of events with available responses
        /// </summary>
        public int EventsResponseAvailableCount { get; set; }
    }
}
