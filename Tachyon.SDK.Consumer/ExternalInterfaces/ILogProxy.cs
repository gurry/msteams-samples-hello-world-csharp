namespace Tachyon.SDK.Consumer.ExternalInterfaces
{
    /// <summary>
    /// This interface represents a contract for logging proxy
    /// </summary>
    public interface ILogProxy
    {
        /// <summary>
        /// Method that will log an error message
        /// </summary>
        /// <param name="text"></param>
        void LogError(string text);
        /// <summary>
        /// Method that will log a warning message
        /// </summary>
        /// <param name="text"></param>
        void LogWarning(string text);
        /// <summary>
        /// Method that will log information-level message
        /// </summary>
        /// <param name="text"></param>
        void LogInfo(string text);
    }
}
