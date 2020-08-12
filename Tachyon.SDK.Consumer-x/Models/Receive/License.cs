namespace Tachyon.SDK.Consumer.Models.Receive
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// License object
    /// </summary>
    public class License
    {
        /// <summary>
        /// Customer name
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// Customer email
        /// </summary>
        public string CustomerEmail { get; set; }
        /// <summary>
        /// License serial number
        /// </summary>
        public string LicenseSerialNumber { get; set; }
        /// <summary>
        /// Issue date
        /// </summary>
        public DateTime? IssueDate { get; set; }
        /// <summary>
        /// Overall expiry date
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
        /// <summary>
        /// Activation period
        /// </summary>
        public int? ActivationPeriodDays { get; set; }
        /// <summary>
        /// Grace period
        /// </summary>
        public int? ActivationGraceDays { get; set; }
        /// <summary>
        /// License service
        /// </summary>
        public LicenseService LicenseService { get; set; }
        /// <summary>
        /// Collection of licensed products
        /// </summary>
        public IEnumerable<Product> Products { get; set; }
        /// <summary>
        /// License type
        /// </summary>
        public string LicenseType { get; set; }
        /// <summary>
        /// Flag indicating if this is a demo license
        /// </summary>
        public int? DemonstrationOnly { get; set; }
    }
}
