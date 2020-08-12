namespace Tachyon.SDK.Consumer.Models.Send
{
    /// <summary>
    /// Model representing an Instruction Token
    /// </summary>
    public class InstructionToken
    {
        /// <summary>
        /// Instruction id for which token has been issued
        /// </summary>
        public int InstructionId { get; set; }
        /// <summary>
        /// Token string
        /// </summary>
        public string Token { get; set; }
    }
}
