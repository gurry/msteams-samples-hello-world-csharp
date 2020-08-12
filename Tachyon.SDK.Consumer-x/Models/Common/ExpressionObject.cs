namespace Tachyon.SDK.Consumer.Models.Common
{
    using System.Collections.Generic;

    /// <summary>
    /// Model representing a single node in an expression tree object
    /// </summary>
    public class ExpressionObject
    {
        /// <summary>
        /// Operator of this expression. Valid for leaves and nodes.
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// Attribute (L-value). Valid for leaves
        /// </summary>
        public string Attribute { get; set; }
        /// <summary>
        /// Value (R-value). Valid for leaves
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Data type of the attribute. Valid for leaves
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// Operands. These are other ExpressionObject objects. Valid for nodes.
        /// </summary>
        public IEnumerable<ExpressionObject> Operands { get; set; }
    }
}
