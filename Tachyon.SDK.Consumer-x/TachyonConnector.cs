namespace Tachyon.SDK.Consumer
{
    using System;

    using Tachyon.SDK.Consumer.Api;
    using Tachyon.SDK.Consumer.DefaultImplementations;
    using Tachyon.SDK.Consumer.ExternalInterfaces;

    /// <summary>
    /// Tachyon connector. Used to connect to Tachyon webapi.
    /// </summary>
    public class TachyonConnector : IDisposable
    {
        private static readonly string apiVersion = "v3.2/";
        private readonly ITransportProxy proxy;

        /// <summary>B
        /// Constructor. Requires only api address and consumer name. Uses default transport proxy and doesn't use logging.
        /// </summary>
        /// <param name="apiRoot">Root address of the web api</param>
        /// <param name="consumerName">Name of the consumer that will be using this SDK</param>
        public TachyonConnector(string apiRoot, string consumerName) : this(apiRoot, consumerName, null, null, false, apiVersion)
        {
        }

        /// <summary>
        /// Constructor. Requires api address and consumer name. Can be supplied with a logger proxy.
        /// </summary>
        /// <param name="apiRoot">Root address of the web api</param>
        /// <param name="consumerName">Name of the consumer that will be using this SDK</param>
        /// <param name="logProxy">Logger object</param>
        public TachyonConnector(string apiRoot, string consumerName, ILogProxy logProxy) : this(apiRoot, consumerName, null, logProxy, false, apiVersion)
        {
        }

        /// <summary>
        /// Constructor. Requires api address and consumer name. Can be supplied with a logger proxy.
        /// </summary>
        /// <param name="apiRoot">Root address of the web api</param>
        /// <param name="consumerName">Name of the consumer that will be using this SDK</param>
        /// <param name="logProxy">Logger object</param>
        /// <param name="overrideApiVersion">Accepts a value such as "3.1" to override the api vaersion that is "baked-in" to the SDK. It is needed when a client such as the RunInstructionUI is compiled with a modern version of the SDK but needs to be backwards-compatible with older versions of Tachyon.</param>
        public TachyonConnector(string apiRoot, string consumerName, ILogProxy logProxy, string overrideApiVersion) : this(apiRoot, consumerName, null, logProxy, false, overrideApiVersion)
        {
        }

        /// <summary>
        /// Constructor. Allows full customization. Requires api address and an instance of a transport proxy. Can be supplied with a logger proxy.
        /// </summary>
        /// <param name="apiRoot">Root address of the web api</param>
        /// <param name="connectionProxy">Transport proxy. Must fully implement the interface and take care of consumer name.
        /// If not supplied a DefaultTransportProxy object will be used.
        /// Will be disposed of then this object is disposed of.</param>
        /// <param name="logProxy">Optional. Logger object</param>
        public TachyonConnector(string apiRoot, ITransportProxy connectionProxy, ILogProxy logProxy) : this(apiRoot, null, connectionProxy, logProxy, true, apiVersion)
        {
        }

        /// <summary>
        /// Private Constructor. Allows full customization. Requires api address and an instance of a transport proxy. Can be supplied with a logger proxy.
        /// </summary>
        /// <param name="apiRoot">Root address of the web api</param>
        /// <param name="consumerName">Name of the consumer that will be using this SDK. Must be supplied if no connection proxy is supplied</param>
        /// <param name="connectionProxy">Transport proxy. Must fully implement the interface and take care of consumer name.
        /// If not supplied a DefaultTransportProxy object will be used.
        /// Will be disposed of then this object is disposed of.</param>
        /// <param name="logProxy">Optional. Logger object</param>
        /// <param name="customProxy">Flag defining if a custom connection proxy is being used</param>
        /// <param name="overrideApiVersion">Accepts a value such as "3.1" to override the api vaersion that is "baked-in" to the SDK. It is needed when a client such as the RunInstructionUI is compiled with a modern version of the SDK but needs to be backwards-compatible with older versions of Tachyon.</param>
        private TachyonConnector(string apiRoot, string consumerName, ITransportProxy connectionProxy, ILogProxy logProxy, bool customProxy, string overrideApiVersion)
        {
            ITransportProxy transportProxy;

            if (apiRoot == null)
            {
                throw new ArgumentNullException("apiRoot");
            }

            if (!customProxy && consumerName == null)
            {
                throw new ArgumentNullException("consumerName");
            }

            if (customProxy && connectionProxy == null)
            {
                throw new ArgumentNullException("connectionProxy");
            }

            var versionedApiRoot = apiRoot;
            if (!apiRoot.EndsWith("/"))
            {
                versionedApiRoot = string.Concat(versionedApiRoot, "/");
            }

            versionedApiRoot = string.Concat(versionedApiRoot, overrideApiVersion);

            if (!versionedApiRoot.EndsWith("/"))
            {
                versionedApiRoot = string.Concat(versionedApiRoot, "/");
            }

            if (connectionProxy == null)
            {
                transportProxy = new DefaultTransportProxy(logProxy, consumerName, versionedApiRoot);
            }
            else
            {
                transportProxy = connectionProxy;
                transportProxy.SetApiRootAddress(versionedApiRoot);
            }

            this.proxy = transportProxy;

            this.ApplicableOperations = new ApplicableOperations(transportProxy, logProxy);
            this.Approvals = new Approvals(transportProxy, logProxy);
            this.AuditLogs = new AuditLogs(transportProxy, logProxy);
            this.Authentication = new Authentication(transportProxy, logProxy);
            this.Consumers = new Consumers(transportProxy, logProxy);
            this.DataExport = new DataExport(transportProxy, logProxy);
            this.Deployment = new Deployment(transportProxy, logProxy);
            this.Devices = new Devices(transportProxy, logProxy);
            this.Expressions = new Expressions(transportProxy, logProxy);
            this.CustomProperties = new CustomProperties(transportProxy, logProxy);
            this.CustomPropertyTypes = new CustomPropertyTypes(transportProxy, logProxy);
            this.CustomPropertyValues = new CustomPropertyValues(transportProxy, logProxy);
            this.Information = new Information(transportProxy, logProxy);
            this.InstructionDefinitions = new InstructionDefinitions(transportProxy, logProxy);
            this.InstructionHierarchies = new InstructionHierarchies(transportProxy, logProxy);
            this.Instructions = new Instructions(transportProxy, logProxy);
            this.ScheduledInstructions = new ScheduledInstructions(transportProxy, logProxy);
            this.InstructionSets = new InstructionSets(transportProxy, logProxy);
            this.InstructionStatistics = new InstructionStatistics(transportProxy, logProxy);
            this.Notifications = new Notifications(transportProxy, logProxy);
            this.Permissions = new Permissions(transportProxy, logProxy);
            this.PrincipalRoles = new PrincipalRoles(transportProxy, logProxy);
            this.Principals = new Principals(transportProxy, logProxy);
            this.PrincipalSearch = new PrincipalSearch(transportProxy, logProxy);
            this.ProductPacks = new ProductPacks(transportProxy, logProxy);
            this.RelatedInstructionDefinitions = new RelatedInstructionDefinitions(transportProxy, logProxy);
            this.ResponseErrors = new ResponseErrors(transportProxy, logProxy);
            this.Responses = new Responses(transportProxy, logProxy);
            this.Roles = new Roles(transportProxy, logProxy);
            this.SecurableTypes = new SecurableTypes(transportProxy, logProxy);
            this.Settings = new Settings(transportProxy, logProxy);
            this.SystemInformation = new SystemInformation(transportProxy, logProxy);
            this.SystemStatistics = new SystemStatistics(transportProxy, logProxy);
            this.Tasks = new Tasks(transportProxy, logProxy);
        }
        /// <summary>
        /// Allows access to object oriented representation of ApplicableOperations consumer API controller
        /// </summary>
        public ApplicableOperations ApplicableOperations { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of Approvals consumer API controller
        /// </summary>
        public Approvals Approvals { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of AuditLogs consumer API controller
        /// </summary>
        public AuditLogs AuditLogs { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of Authentication consumer API controller
        /// </summary>
        public Authentication Authentication { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of Consumers consumer API controller
        /// </summary>
        public Consumers Consumers { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of DataExport consumer API controller
        /// </summary>
        public DataExport DataExport { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of Devices consumer API controller
        /// </summary>
        public Deployment Deployment { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of the Deploy consumer API controller
        /// </summary>
        public Devices Devices { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of Expressions consumer API controller
        /// </summary>
        public Expressions Expressions { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of CustomProperties consumer API controller
        /// </summary>
        public CustomProperties CustomProperties { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of CustomPropertyTypes consumer API controller
        /// </summary>
        public CustomPropertyTypes CustomPropertyTypes { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of CustomPropertyValues consumer API controller
        /// </summary>
        public CustomPropertyValues CustomPropertyValues { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of Information consumer API controller
        /// </summary>
        public Information Information { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of InstructionDefinitions consumer API controller
        /// </summary>
        public InstructionDefinitions InstructionDefinitions { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of InstructionHierarchies consumer API controller
        /// </summary>
        public InstructionHierarchies InstructionHierarchies { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of Instructions consumer API controller
        /// </summary>
        public Instructions Instructions { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of ScheduledInstructions consumer API controller
        /// </summary>
        public ScheduledInstructions ScheduledInstructions { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of InstructionSets consumer API controller
        /// </summary>
        public InstructionSets InstructionSets { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of InstructionStatistics consumer API controller
        /// </summary>
        public InstructionStatistics InstructionStatistics { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of Notifications consumer API controller
        /// </summary>
        public Notifications Notifications { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of Permissions consumer API controller
        /// </summary>
        public Permissions Permissions { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of PrincipalRoles consumer API controller
        /// </summary>
        public PrincipalRoles PrincipalRoles { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of Principals consumer API controller
        /// </summary>
        public Principals Principals { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of PrincipalSearch consumer API controller
        /// </summary>
        public PrincipalSearch PrincipalSearch { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of ProductPacks consumer API controller
        /// </summary>
        public ProductPacks ProductPacks { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of RelatedInstructionDefinitions consumer API controller
        /// </summary>
        public RelatedInstructionDefinitions RelatedInstructionDefinitions { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of ResponseErrors consumer API controller
        /// </summary>
        public ResponseErrors ResponseErrors { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of Responses consumer API controller
        /// </summary>
        public Responses Responses { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of Roles consumer API controller
        /// </summary>
        public Roles Roles { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of SecurableTypes consumer API controller
        /// </summary>
        public SecurableTypes SecurableTypes { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of Settings consumer API controller
        /// </summary>
        public Settings Settings { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of SystemInformation consumer API controller
        /// </summary>
        public SystemInformation SystemInformation { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of SystemStatistics consumer API controller
        /// </summary>
        public SystemStatistics SystemStatistics { get; private set; }
        /// <summary>
        /// Allows access to object oriented representation of Tasks consumer API controller
        /// </summary>
        public Tasks Tasks { get; private set; }
        /// <summary>
        /// Call to dispose of the connector.
        /// </summary>
        public void Dispose()
        {
            if (this.proxy != null)
            {
                this.proxy.Dispose();
            }
        }
    }
}
