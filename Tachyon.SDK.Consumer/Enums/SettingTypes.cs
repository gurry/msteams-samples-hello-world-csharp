namespace Tachyon.SDK.Consumer.Enums
{
    /// <summary>
    /// Available global settings
    /// </summary>
    public enum SettingTypes
    {
        /// <summary>
        /// Maximum TTL for instruction execution
        /// </summary>
        MaximumInstructionTtlMinutes,
        /// <summary>
        /// Maximum TTL for responses
        /// </summary>
        MaximumResponseTtlMinutes,
        /// <summary>
        /// Maximum number of simultaneous in-flight instructions
        /// </summary>
        MaximumSimultaneousInFlightInstructions,
        /// <summary>
        /// Default number of bytes that will be returned for CLOBs
        /// </summary>
        ClobDefaultReadSize
    }
}
