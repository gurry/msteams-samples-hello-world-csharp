namespace Tachyon.SDK.Consumer.Models.Common
{
    using System.Collections.Generic;
    /// <summary>
    /// Model defining parameter validation rules
    /// </summary>
    public class ValidationDefinition
    {
        /// <summary>
        /// Regular expression to use. Only valid for string.
        /// </summary>
        public string Regex { get; set; }
        /// <summary>
        /// Max length. Only valid for string
        /// </summary>
        public string MaxLength { get; set; }
        /// <summary>
        /// List of values that are allowed
        /// </summary>
        public List<string> AllowedValues { get; set; }
        /// <summary>
        /// Restrictions on numerical values. Not used at this time.
        /// </summary>
        public string NumValueRestrictions { get; set; }
    }
}
