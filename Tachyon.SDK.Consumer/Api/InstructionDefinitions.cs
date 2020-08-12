namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Tachyon.SDK.Consumer.Enums;
    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;
    using Tachyon.SDK.Consumer.Models.Receive;
    using Tachyon.SDK.Consumer.Models.Send;
    using Tachyon.SDK.Consumer.Tools;

    /// <summary>
    /// Abstracts InstructionDefinitions controller of Consumer API
    /// </summary>
    public class InstructionDefinitions : ApiBase
    {
        private readonly string coreEndpoint = "InstructionDefinitions";
        private readonly string getByNameEndpoint = "InstructionDefinitions/Name/";
        private readonly string getByIdEndpoint = "InstructionDefinitions/Id/";
        private readonly string searchTermEndpoint = "InstructionDefinitions/Search/";
        private readonly string getDefinitionsBySetId = "InstructionDefinitions/InstructionSet/Id/";
        private readonly string getDefinitionsForAllSets = "InstructionDefinitions/InstructionSet";
        private readonly string getAllDefinitions = "InstructionDefinitions/All";
        private readonly string getDefinitionsBySetName = "InstructionDefinitions/InstructionSet/Name/";
        private readonly string setManagement = "InstructionDefinitions/Id/{0}/InstructionSet/{1}";
        private readonly string exportById = "InstructionDefinitions/Id/{0}/export";
        private readonly string exportByName = "InstructionDefinitions/Name/{0}/export";
        private readonly string coreExport = "InstructionDefinitions/Export";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public InstructionDefinitions(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Gets all devices with the option to narrow down to given instructions type(s)
        /// </summary>
        /// <param name="instructionTypes">Optional. List the types of instructions you wish to receive</param>
        /// <returns>Collection of instruction definitions</returns>
        /// <remarks>Calls GET on apiRoot/InstructionDefinitions</remarks>
        public ApiCallResponse<IEnumerable<InstructionDefinition>> Get(List<InstructionType> instructionTypes)
        {
            string queryString = ConvertInputListToQueryString(instructionTypes);
            var apiCallResult = this.transportProxy.Get<IEnumerable<InstructionDefinition>>(string.IsNullOrEmpty(queryString) ? this.coreEndpoint : this.coreEndpoint + "?" + queryString);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'Get' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns definition of a instruction with a given id
        /// </summary>
        /// <param name="instructionId">Id of the definition to look for</param>
        /// <returns>Instruction definition</returns>
        /// <remarks>Calls apiRoot/InstructionDefinitions/Id/{instructionId}</remarks>
        public ApiCallResponse<InstructionDefinition> GetInstructionDefinition(int instructionId)
        {
            var apiCallResult = this.transportProxy.Get<InstructionDefinition>(this.getByIdEndpoint + instructionId);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'GetInstructionDefinition' (Id) call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Returns definition of a instruction with a given name
        /// </summary>
        /// <param name="instructionName">Name of the definition to look for</param>
        /// <returns>Instruction definition</returns>
        /// <remarks>Calls apiRoot/InstructionDefinitions/Name/{instructionName}</remarks>
        public ApiCallResponse<InstructionDefinition> GetInstructionDefinition(string instructionName)
        {
            var apiCallResult = this.transportProxy.Get<InstructionDefinition>(this.getByNameEndpoint + instructionName);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'GetInstructionDefinition' (Name) call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Deletes instruction definition with a given id
        /// </summary>
        /// <param name="instructionId">Id of the definition to delete</param>
        /// <returns>True if successful</returns>
        /// <remarks>Calls DELETE on apiRoot/InstructionDefinitions/Id/{instructionId}</remarks>
        public ApiCallResponse<bool> DeleteInstructionDefinition(int instructionId)
        {
            var apiCallResult = this.transportProxy.Delete(this.getByIdEndpoint + instructionId);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'DeleteInstructionDefinition' (Id) call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Deletes instruction definition with a given name
        /// </summary>
        /// <param name="instructionName">Name of the definition to delete</param>
        /// <returns>True if successful</returns>
        /// <remarks>Calls DELETE on apiRoot/InstructionDefinitions/Name/{instructionName}</remarks>
        public ApiCallResponse<bool> DeleteInstructionDefinition(string instructionName)
        {
            var apiCallResult = this.transportProxy.Delete(this.getByNameEndpoint + instructionName);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'DeleteInstructionDefinition' (Name) call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Deletes given instruction definitions
        /// </summary>
        /// <param name="instructionDefinitionIdList">Collection of instruction definition Ids</param>
        /// <returns></returns>
        /// <remarks>Calls DELETE on apiRoot/InstructionDefinitions</remarks>
        public ApiCallResponse<bool> DeleteInstructionDefinitions(List<int> instructionDefinitionIdList)
        {
            var requestBody = new IdAndNameLists()
            {
                Ids = instructionDefinitionIdList
            };

            var apiCallResult = this.transportProxy.Delete(requestBody, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'DeleteInstructionDefinitions' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Deletes given instruction definitions
        /// </summary>
        /// <param name="instructionDefinitionNameList">Collection of instruction definition names</param>
        /// <returns></returns>
        /// <remarks>Calls DELETE on apiRoot/InstructionDefinitions</remarks>
        public ApiCallResponse<bool> DeleteInstructionDefinitions(List<string> instructionDefinitionNameList)
        {
            var requestBody = new IdAndNameLists()
            {
                Names = instructionDefinitionNameList
            };

            var apiCallResult = this.transportProxy.Delete(requestBody, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'DeleteInstructionDefinitions' call failed.");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Deletes given instruction definitions
        /// </summary>
        /// <param name="instructionDefinitions">Collection of instruction definitions</param>
        /// <returns></returns>
        /// <remarks>Calls DELETE on apiRoot/InstructionDefinitions</remarks>
        public ApiCallResponse<bool> DeleteInstructionDefinitions(List<Models.Receive.InstructionDefinition> instructionDefinitions)
        {
            return this.DeleteInstructionDefinitions(instructionDefinitions.Select(p => p.Id).ToList());
        }

        /// <summary>
        /// Gets instruction definitions that match given search. Data returned is streamlined and not full information about the definition.
        /// Designed for quick response ie. hint-as-you-type controls.
        /// </summary>
        /// <param name="searchTerm">Term to search for</param>
        /// <param name="instructionTypes">Optional. List of instruction types you wish to search for.</param>
        /// <returns>List of instruction definitions that fit the search criteria. Streamlined data.</returns>
        /// <remarks>Calls GET on apiRoot/InstructionDefinitions/Search/{searchTerm}</remarks>
        public ApiCallResponse<InstructionHintContainer> GetInstructionDefinitionHints(string searchTerm, List<InstructionType> instructionTypes)
        {
            string queryString = ConvertInputListToQueryString(instructionTypes);
            var apiCallResult = this.transportProxy.Get<InstructionHintContainer>(string.IsNullOrEmpty(queryString) ? this.searchTermEndpoint + Base64Utility.ToUrlSafeBase64(searchTerm)
                                                                                                                    : this.searchTermEndpoint + Base64Utility.ToUrlSafeBase64(searchTerm) + "?" + queryString);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'GetInstructionDefinitionHints' call failed.");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Returns all instruction definitions that belong to a given Instruction Set.
        /// </summary>
        /// <param name="instructionSetId">Id of the instruction set</param>
        /// <returns>Collection of instruction definitions that belong to given set.</returns>
        /// <remarks>Calls GET on apiRoot/InstructionDefinitions/InstructionSet/Id/{setId}</remarks>
        public ApiCallResponse<IEnumerable<InstructionDefinition>> GetInstructionDefinitionsByInstructionSet(int? instructionSetId)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<InstructionDefinition>>(this.getDefinitionsBySetId + instructionSetId);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'GetInstructionDefinitionsByInstructionSet' (Id) call failed");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Returns all instruction definitions that are assigned to an instruction set
        /// </summary>        
        /// <returns>Collection of instruction definitions.</returns>
        /// <remarks>Calls GET on apiRoot/InstructionDefinitions/InstructionSet</remarks>
        public ApiCallResponse<IEnumerable<InstructionDefinition>> GetInstructionDefinitionsForAllInstructionSets()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<InstructionDefinition>>(this.getDefinitionsForAllSets);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'GetInstructionDefinitionsForAllInstructionSets' call failed");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Returns all instruction definitions, includes both assigned and unassigned
        /// Should only be used for instruction set management and not issuing instructions
        /// </summary>        
        /// <returns>Collection of instruction definitions.</returns>
        /// <remarks>Calls GET on apiRoot/InstructionDefinitions/All</remarks>
        public ApiCallResponse<IEnumerable<InstructionDefinition>> GetAllInstructionDefinitions()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<InstructionDefinition>>(this.getAllDefinitions);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'GetAllInstructionDefinitions' call failed");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Returns all instruction definitions that belong to a given Instruction Set.
        /// </summary>
        /// <param name="instructionSetName">Name of the instruction set</param>
        /// <returns>Collection of instruction definitions that belong to given set.</returns>
        /// <remarks>Calls GET on apiRoot/InstructionDefinitions/InstructionSet/Name/{setName}</remarks>
        public ApiCallResponse<IEnumerable<InstructionDefinition>> GetInstructionDefinitionsByInstructionSet(string instructionSetName)
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<InstructionDefinition>>(this.getDefinitionsBySetName + instructionSetName);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'GetInstructionDefinitionsByInstructionSet' (Name) call failed");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Adds selected instruction definition to selected instruction set
        /// </summary>
        /// <param name="instructionDefinitionId">Id of the instruction definition you wish to add to the set</param>
        /// <param name="instructionSetId">Id of the set you want to add the definition to</param>
        /// <returns>Updated instruction definition</returns>
        /// <remarks>Calls POST on apiRoot/InstructionDefinitions/Id/{defId:int}/InstructionSet/{setId:int}</remarks>
        public ApiCallResponse<InstructionDefinition> AddDefinitionToSet(int instructionDefinitionId, int instructionSetId)
        {
            var endpoint = string.Format(this.setManagement, instructionDefinitionId, instructionSetId);
            var apiCallResult = this.transportProxy.PostEmpty<InstructionDefinition>(endpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'AddDefinitionToSet' call failed");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Removes selected instruction definition from selected instruction set
        /// </summary>
        /// <param name="instructionDefinitionId">Id of the instruction definition you wish to remove from the set</param>
        /// <param name="instructionSetId">Id of the set you want to remove the definition from</param>
        /// <returns>Updated instruction definition</returns>
        /// <remarks>Calls DELETE on apiRoot/InstructionDefinitions/Id/{defId:int}/InstructionSet/{setId:int}</remarks>
        public ApiCallResponse<bool> RemoveDefinitionFromSet(int instructionDefinitionId, int instructionSetId)
        {
            var endpoint = string.Format(this.setManagement, instructionDefinitionId, instructionSetId);
            var apiCallResult = this.transportProxy.Delete(endpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'RemoveDefinitionFromSet' call failed");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Returns given instruction definition as an Xml document
        /// </summary>
        /// <param name="instructionDefinitionId">Instruction definition Id</param>
        /// <returns>Memory stream containing the definition in Xml form</returns>
        /// <remarks>Calls apiRoot/InstructionDefinitions/Id/{id:int}/export</remarks>
        public ApiCallResponse<MemoryStream> ExportDefinition(int instructionDefinitionId)
        {
            var apiCallResult = this.transportProxy.DownloadFile(string.Format(this.exportById, instructionDefinitionId));
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'ExportDefinition' (Id) call failed");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Returns given instruction definition as an Xml document
        /// </summary>
        /// <param name="instructionDefinitionName">Instruction definition Name</param>
        /// <returns>Memory stream containing the definition in Xml form</returns>
        /// <remarks>Calls apiRoot/InstructionDefinitions/Name/{name}/export</remarks>
        public ApiCallResponse<MemoryStream> ExportDefinition(string instructionDefinitionName)
        {
            var apiCallResult = this.transportProxy.DownloadFile(string.Format(this.exportByName, instructionDefinitionName));
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'ExportDefinition' (Name) call failed");
            }
            return apiCallResult;
        }

        /// <summary>
        /// Returns specified instruction definitions as Xml documents inside a zip archive
        /// </summary>
        /// <param name="instructionDefinitionIdList">List of instruction definition Ids</param>
        /// <returns>Memory stream containing the archive</returns>
        /// <remarks>Calls POST apiRoot/InstructionDefinitions/Export</remarks>
        public ApiCallResponse<MemoryStream> ExportDefinitions(List<int> instructionDefinitionIdList)
        {
            var requestBody = new IdAndNameLists
            {
                Ids = instructionDefinitionIdList
            };

            var apiCallResult = this.transportProxy.DownloadFile(this.coreExport, requestBody);
            if (!apiCallResult.Success)
            {
                this.LogError("Instruction Definitions Api 'ExportDefinitions' call failed");
            }
            return apiCallResult;
        }
    }
}
