namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    using Tachyon.SDK.Consumer.Enums;
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;
    using Tachyon.SDK.Consumer.Models.Receive;
    using Tachyon.SDK.Consumer.Models.Send;
    using Tachyon.SDK.Consumer.Tools;

    /// <summary>
    /// Abstracts the Agent Deployment Jobs controller of Consumer API 
    /// </summary>
    public class Deployment : ApiBase
    {
        private readonly string jobsEndpoint = "Deploy/Jobs";
        private readonly string jobSearchEndpoint = "Deploy/Jobs/Search";
        private readonly string getJobByIdEndpoint = "Deploy/Jobs/{0}";
        private readonly string deviceSearchEndpoint = "Deploy/Devices/Search";
        private readonly string getDeviceByIdEndpoint = "Deploy/Devices/{id}";
        private readonly string cancelJobEndpoint = "Deploy/Jobs/{0}/Cancel";
        private readonly string configurationEndpoint = "Deploy/Configuration";
        private readonly string getInstallersEndpoint = "Deploy/Installers";
        private readonly string installerEndpoint1 = "Deploy/Installers/AgentVersion/{agentVersion}/Os/{operatingSystemType}/OsVersion/{operatingSystemVersion}/OsArch/{operatingSystemArchitecture}";
        private readonly string installerEndpoint2 = "Deploy/Installers/AgentVersion/{agentVersion}/Os/{operatingSystemType}/OsArch/{operatingSystemArchitecture}";
        private readonly string deleteInstallerByIdEndpoint = "Deploy/Installers/{0}";
        private readonly string supportedOsEndpoint = "Deploy/SupportedOs";
        private readonly string supportedInstallerTypesEndpoint = "Deploy/SupportedInstallerTypes";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy">Instance of a transport proxy</param>
        /// <param name="logProxy">OPTIONAL. Instance of a logging proxy</param>
        public Deployment(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Returns all jobs
        /// </summary>
        /// <returns>List of Agent Deployment Jobs</returns>
        /// <remarks>Calls GET on apiRoot/Deploy/Jobs</remarks>
        public ApiCallResponse<IEnumerable<AgentDeploymentJob>> GetAllJobs()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<AgentDeploymentJob>>(this.jobsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Deployment Api 'GetAllJobs' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns all Agent Deployment Jobs that match a given scope expression
        /// </summary>
        /// <param name="searchParameters">Configuration for the search operation</param>
        /// <returns>List of Agent Deployment Jobs</returns>
        /// <remarks>Calls POST on apiRoot/Deploy/Jobs/Search</remarks>
        public ApiCallResponse<SearchResult<AgentDeploymentJob>> FindJobs(Models.Send.Search searchParameters)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.Search, SearchResult<AgentDeploymentJob>>(searchParameters, this.jobSearchEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Deployment Api 'FindJobs' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Retrieves a Job by its Id
        /// </summary>
        /// <returns>Agent Deployment Job</returns>
        /// <remarks>Calls GET on apiRoot/Deploy/Jobs/{id}</remarks>
        public ApiCallResponse<AgentDeploymentJob> GetJob(int id)
        {
            var apiCallResult = this.transportProxy.Get<AgentDeploymentJob>(string.Format(this.getJobByIdEndpoint, id.ToString()));
            if (!apiCallResult.Success)
            {
                this.LogError("Deployment Api 'GetJob' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns all Agent Deployment Devices that match a given scope expression
        /// </summary>
        /// <param name="searchParameters">Configuration for the search operation</param>
        /// <returns>List of Devices for Agent Deployment Jobs</returns>
        /// <remarks>Calls POST on apiRoot/Deploy/Devices/Search</remarks>
        public ApiCallResponse<SearchResult<AgentDeploymentDevice>> FindDevices(Models.Send.Search searchParameters)
        {
            var apiCallResult = this.transportProxy.Post<Models.Send.Search, SearchResult<AgentDeploymentDevice>>(searchParameters, this.deviceSearchEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Deployment Api 'FindDevices' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Retrieves a Device by its Id
        /// </summary>
        /// <returns>Agent Deployment Device</returns>
        /// <remarks>Calls GET on apiRoot/Deploy/Devices/{id}</remarks>
        public ApiCallResponse<AgentDeploymentDevice> GetDevice(int id)
        {
            var apiCallResult = this.transportProxy.Get<AgentDeploymentDevice>(this.getDeviceByIdEndpoint.Replace("{id}", id.ToString()));
            if (!apiCallResult.Success)
            {
                this.LogError("Deployment Api 'GetDevice' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Sends a new Job
        /// </summary>
        /// <param name="job">AgentDeployment job to send</param>
        /// <returns>Newly created job if successful. Null otherwise.</returns>
        /// <remarks>Calls apiRoot/Deploy/Jobs</remarks>
        public ApiCallResponse<AgentDeployment> SendJob(AgentDeployment job)
        {
            var apiCallResult = this.transportProxy.Post<AgentDeployment, AgentDeployment>(job, this.jobsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Instructions Api 'SendJob' call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Cancels given deployment job
        /// </summary>
        /// <param name="jobId">Id of the job to cancel</param>
        /// <returns>True if successful. False otherwise</returns>
        /// <remarks>Deploy/Jobs/{id}/Cancel</remarks>
        public ApiCallResponse<bool> CancelJob(int jobId)
        {
            var apiCallResult = this.transportProxy.PostEmpty<object>(string.Format(this.cancelJobEndpoint, jobId));
            if (!apiCallResult.Success)
            {
                this.LogError("Instructions Api 'CancelJob' call failed.");
            }

            return new ApiCallResponse<bool>(apiCallResult.Success, apiCallResult.Success, apiCallResult.Errors, apiCallResult.ResponseStatusCode);
        }

        /// <summary>
        /// Returns Configuration Data
        /// </summary>
        /// <returns>Switches, background channels, and other options that can be used for building a job</returns>
        /// <remarks>Calls GET on apiRoot/Deploy/Configuration</remarks>
        public ApiCallResponse<AgentDeploymentConfiguration> GetConfiguration()
        {
            var apiCallResult = this.transportProxy.Get<AgentDeploymentConfiguration>(this.configurationEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Deployment Api 'GetConfiguration' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns available installers
        /// </summary>
        /// <returns>Switches, background channels, and other options that can be used for building a job</returns>
        /// <remarks>Calls GET on Deploy/Installers</remarks>
        public ApiCallResponse<IEnumerable<AgentDeploymentInstaller>> GetAllInstallers()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<AgentDeploymentInstaller>>(this.getInstallersEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Deployment Api 'GetAllInstallers' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Uploads an installer
        /// </summary>
        /// <param name="agentVersion">Version of the agent to be installed</param>
        /// <param name="fileName">Name of the installer file</param>
        /// <param name="operatingSystemArchitecture">Operating system architecture: X64 or X86</param>
        /// <param name="operatingSystemType">Operating system type ie. Windows</param>
        /// <param name="operatingSystemVersion">Operating system version. Used only for operating systems that require different installers for different versions, like various Linuxes</param>
        /// <param name="stream">Stream containing the installer file</param>
        /// <returns></returns>
        /// <remarks>Calls POST on Deploy/Installers
        /// </remarks>
        public ApiCallResponse<string> Upload(MemoryStream stream, string fileName, string agentVersion, OperatingSystem operatingSystemType, string operatingSystemVersion, OperatingSystemArchitecture operatingSystemArchitecture)
        {
            var formData = new Dictionary<string,string>
            {
                { "AgentVersion", agentVersion },
                { "Os", ((int)operatingSystemType).ToString() },
                { "OsArch", ((int)operatingSystemArchitecture).ToString() },
                { "OsVersion", operatingSystemVersion }
            };
            var files = new Dictionary<string, MemoryStream>
            {
                { fileName, stream }
            };
            var apiCallResult = this.transportProxy.PostForm<string>(formData, files, this.getInstallersEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Deployment Api 'Upload' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Uploads an installer
        /// </summary>
        /// <param name="agentVersion">Version of the agent to be installed</param>
        /// <param name="operatingSystemArchitecture">Operating system architecture: X64 or X86</param>
        /// <param name="operatingSystemType">Operating system type ie. Windows</param>
        /// <param name="operatingSystemVersion">Operating system version. Used only for operating systems that require different installers for different versions, like various Linuxes</param>
        /// <param name="filePath">Full path to the installer file</param>
        /// <returns></returns>
        /// <remarks>Calls POST on Deploy/Installers
        /// </remarks>
        public ApiCallResponse<string> Upload(string filePath, string agentVersion, OperatingSystem operatingSystemType, string operatingSystemVersion, OperatingSystemArchitecture operatingSystemArchitecture)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }

            var fileName = Path.GetFileName(filePath);
            var stream = new MemoryStream(File.ReadAllBytes(filePath));
            return Upload(stream, fileName, agentVersion, operatingSystemType, operatingSystemVersion, operatingSystemArchitecture);
        }

        /// <summary>
        /// Delete an installer
        /// </summary>
        /// <param name="id">The id of the installer</param>
        /// <returns>True if deleted successfully. False otherwise</returns>
        /// <remarks>Calls DELETE on Deploy/Installers/{id}</remarks>
        public ApiCallResponse<bool> Delete(int id)
        {
            var apiCallResult = this.transportProxy.Delete(string.Format(this.deleteInstallerByIdEndpoint, id));
            if (!apiCallResult.Success)
            {
                this.LogError("Deployment Api 'Delete' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Delete an installer
        /// </summary>
        /// <param name="agentVersion">Version of the agent to be installed</param>
        /// <param name="operatingSystemArchitecture">Operating system architecture: X64 or X86</param>
        /// <param name="operatingSystemType">Operating system type ie. Windows</param>
        /// <param name="operatingSystemVersion">Operating system version. Used only for operating systems that require different installers for different versions, like various Linuxes</param>
        /// <returns>True if deleted successfully. False otherwise</returns>
        /// <remarks>Calls DELETE on
        ///   Deploy/Installers/AgentVersion/{agentVersion}/Os/{operatingSystemType}/OsVersion/{operatingSystemVersion}/OsArch/{operatingSystemArchitecture} if operatingSystemVersion is supplied, or
        ///   Deploy/Installers/AgentVersion/{agentVersion}/Os/{operatingSystemType}/OsArch/{operatingSystemArchitecture} if operatingSystemVersion is null or empty
        /// </remarks>
        public ApiCallResponse<bool> Delete(string agentVersion, Enums.OperatingSystem operatingSystemType, string operatingSystemVersion, Enums.OperatingSystemArchitecture operatingSystemArchitecture)
        {
            string endpointToUse = string.IsNullOrEmpty(operatingSystemVersion) ? this.installerEndpoint2 : this.installerEndpoint1;
            endpointToUse = ReplaceParametersInEndpoint(agentVersion, operatingSystemType, operatingSystemVersion, operatingSystemArchitecture, endpointToUse);
            var apiCallResult = this.transportProxy.Delete(endpointToUse);
            if (!apiCallResult.Success)
            {
                this.LogError("Deployment Api 'Delete' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Retrieves a list of operating systems supported by agent deployment feature.
        /// </summary>
        /// <returns>Dictionary, where the key is an integer representing the numerical value of an entry from
        /// the Common.Enums.OperatingSystem enumeration and value is a string representing textual value of the same enumeration. </returns>
        public ApiCallResponse<Dictionary<int, string>> GetSupportedOperatingSystems()
        {
            var apiCallResult = this.transportProxy.Get<Dictionary<int, string>>(this.supportedOsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Deployment Api 'GetSupportedOperatingSystems' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Retrieves a list of supported installer types
        /// </summary>
        /// <returns>a list of <see cref="InstallerType"/> objects.</returns>
        public ApiCallResponse<List<InstallerType>> GetSupportedInstallerTypes()
        {
            var apiCallResult = this.transportProxy.Get<List<InstallerType>>(this.supportedInstallerTypesEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError(string.Format("Deployment Api '{0}' call failed", MethodBase.GetCurrentMethod().Name));
            }

            return apiCallResult;
        }

        private static string ReplaceParametersInEndpoint(string agentVersion, Enums.OperatingSystem operatingSystemType, string operatingSystemVersion, Enums.OperatingSystemArchitecture operatingSystemArchitecture, string endpointToUse)
        {
            endpointToUse = endpointToUse.Replace("{agentVersion}", agentVersion);
            endpointToUse = endpointToUse.Replace("{operatingSystemType}", operatingSystemType.ToString());
            endpointToUse = endpointToUse.Replace("{operatingSystemVersion}", operatingSystemVersion ?? string.Empty);
            endpointToUse = endpointToUse.Replace("{operatingSystemArchitecture}", operatingSystemArchitecture.ToString());
            return endpointToUse;
        }
    }
}
