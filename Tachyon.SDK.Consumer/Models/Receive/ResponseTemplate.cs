namespace Tachyon.SDK.Consumer.Models.Receive
{
    /// <summary>
    /// Model representing a response template
    /// </summary>
    public class ResponseTemplate
    {
        /// <summary>
        /// Id of the template
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the template
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Template JSON
        /// </summary>
        public string Template { get; set; }
    }
}
