namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System.Collections.Generic;

    /// <summary>
    /// Model representing Response template configuration
    /// </summary>
    public class ResponseTemplateConfigurations
    {
        /// <summary>
        /// Name of the response template
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Collection of template configurations
        /// </summary>
        public List<ResponseTemplateConfiguration> TemplateConfigurations { get; set; }
        /// <summary>
        /// Collection of post processors
        /// </summary>
        public List<PostProcessor> PostProcessors { get; set; }
    }
}
