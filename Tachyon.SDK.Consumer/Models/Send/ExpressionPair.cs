namespace Tachyon.SDK.Consumer.Models.Send
{
    using Tachyon.SDK.Consumer.Models.Common;

    /// <summary>
    /// Models used to pass in two expressions
    /// </summary>
    public class ExpressionPair
    {
        /// <summary>
        /// First (or Left) expression
        /// </summary>
        public ExpressionObject First { get; set; }
        /// <summary>
        /// Second (or Right) expression
        /// </summary>
        public ExpressionObject Second { get; set; }
    }
}
