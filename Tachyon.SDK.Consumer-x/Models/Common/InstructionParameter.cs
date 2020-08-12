namespace Tachyon.SDK.Consumer.Models.Common
{
    /// <summary>
    /// Model defining an instruction parameter
    /// </summary>
    public class InstructionParameter
    {
        /// <summary>
        /// Name of the parameter
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Pattern this parameter uses inside payload and readable payload
        /// </summary>
        public string Pattern { get; set; }
        /// <summary>
        /// Data type
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// Type of control that should be used to display this parameter (freeText, valuePicker, customPropertyKey or customPropertyValue)
        /// </summary>
        public string ControlType { get; set; }
        /// <summary>
        /// Control metadata. This is control type specific:
        /// 1) freeText - ignored
        /// 2) valuePicker - ignored
        /// 3) customPropertyKey - must contain type of custom property ie. "CoverageTag" as it exists in the database
        /// 4) customPropertyValue - must contain type of custom property ie. "CoverageTag" as it exists in the database
        /// </summary>
        public string ControlMetadata { get; set; }
        /// <summary>
        /// Placeholder to display in the control before the user puts in any data
        /// </summary>
        public string Placeholder { get; set; }
        /// <summary>
        /// Default value to use. This value must conform to validation rules.
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// Validation rules for this parameter
        /// </summary>
        public ValidationDefinition Validation { get; set; }
        /// <summary>
        /// Value of the parameter. Valid when sending instructions to the api.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Hint text to display for this parameter
        /// </summary>
        public string HintText { get; set; }
        /// <summary>
        /// Valid only for custom property value fields. Points to the parameter that has the custom property (key) to use for this value
        /// </summary>
        public string Source { get; set; }
    }
}
