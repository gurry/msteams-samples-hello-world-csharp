namespace Tachyon.SDK.Consumer.Models.Common
{
    using System.Collections.Generic;

    using Tachyon.SDK.Consumer.Enums;

    /// <summary>
    /// Models used to represent a set of instruction definition hints
    /// </summary>
    public class InstructionHintContainer
    {
        /// <summary>
        /// Default constructor. Will get Result to Successful
        /// </summary>
        public InstructionHintContainer()
        {
            Result = InstructionHintSearchResult.Successful;
        }
        /// <summary>
        /// Result of the call
        /// </summary>
        public InstructionHintSearchResult Result { get; set; }
        /// <summary>
        /// Any error that might have occurred
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// Instructions that match the input
        /// </summary>
        public List<InstructionHint> Instructions { get; set; }
    }
}
