namespace Tachyon.SDK.Consumer.Api
{
    using System.Collections.Generic;
    using System.IO;

    using Tachyon.SDK.Consumer.ExternalInterfaces;
    using Tachyon.SDK.Consumer.Models.Api;
    using Tachyon.SDK.Consumer.Models.Common;
    using Tachyon.SDK.Consumer.Models.Send;

    /// <summary>
    /// Abstracts InstructionSets controller of Consumer API
    /// </summary>
    public class InstructionSets : ApiBase
    {
        private readonly string coreEndpoint = "InstructionSets";
        private readonly string withCountsEndpoint = "InstructionSets/IncludeCounts";
        private readonly string byIdEndpoint = "InstructionSets/Id/{0}";
        private readonly string byIdOptionalDeleteEndpoint = "InstructionSets/Id/{0}?deleteContent={1}";
        private readonly string byNameEndpoint = "InstructionSets/Name/{0}";
        private readonly string exportByIdEndpoint = "InstructionSets/Id/{0}/Export";
        private readonly string contentsEndpoint = "InstructionSets/Contents";
        private readonly string clearByIdEndpoint = "InstructionSets/Id/{0}/Clear";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transportProxy"></param>
        /// <param name="logProxy"></param>
        public InstructionSets(ITransportProxy transportProxy, ILogProxy logProxy)
            : base(transportProxy, logProxy)
        {
        }

        /// <summary>
        /// Returns all instruction sets
        /// </summary>
        /// <returns>Collection of instruction sets</returns>
        /// <remarks>Calls apiRoot/InstructionSets/</remarks>
        public ApiCallResponse<IEnumerable<InstructionSet>> GetAll()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<InstructionSet>>(coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionSets Api 'GetAll' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns all instruction sets, and includes the count of instructions in each set
        /// </summary>
        /// <returns>Collection of instruction sets with counts</returns>
        /// <remarks>Calls apiRoot/InstructionSets/IncludeCounts</remarks>
        public ApiCallResponse<IEnumerable<InstructionSetWithCounts>> GetAllWithCounts()
        {
            var apiCallResult = this.transportProxy.Get<IEnumerable<InstructionSetWithCounts>>(withCountsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionSets Api 'GetAllWithCounts' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns an Instruction Set
        /// </summary>
        /// <param name="instructionSetId">Id of the instruction set</param>
        /// <returns>Requested instruction set</returns>
        /// <remarks>Calls apiRoot/InstructionSets/Id/{id}</remarks>
        public ApiCallResponse<InstructionSet> Get(int instructionSetId)
        {
            var apiCallResult = this.transportProxy.Get<InstructionSet>(string.Format(this.byIdEndpoint, instructionSetId));
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionSets Api 'Get' (Id) call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Returns an Instruction Set
        /// </summary>
        /// <param name="instructionSetName">Name of the instruction set</param>
        /// <returns>Requested instruction set</returns>
        /// <remarks>Calls apiRoot/InstructionSets/Name/{name}</remarks>
        public ApiCallResponse<InstructionSet> Get(string instructionSetName)
        {
            var apiCallResult = this.transportProxy.Get<InstructionSet>(string.Format(this.byNameEndpoint, instructionSetName));
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionSets Api 'Get' (Name) call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Adds n new Instruction set
        /// </summary>
        /// <param name="instructionSet">Instruction set to add</param>
        /// <returns>Created instruction set</returns>
        /// <remarks>Calls POST on apiRoot/InstructionSets/</remarks>
        public ApiCallResponse<InstructionSet> Add(InstructionSet instructionSet)
        {
            var apiCallResult = this.transportProxy.Post<InstructionSet, InstructionSet>(instructionSet, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionSets Api 'Add' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Updates given Instruction set
        /// </summary>
        /// <param name="instructionSet">Instruction set to update</param>
        /// <returns>Updated instruction set</returns>
        /// <remarks>Calls PUT on apiRoot/InstructionSets/</remarks>
        public ApiCallResponse<InstructionSet> Update(InstructionSet instructionSet)
        {
            var apiCallResult = this.transportProxy.Put<InstructionSet, InstructionSet>(instructionSet, this.coreEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionSets Api 'Update' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Deletes given instruction set. All instruction definitions in that set will go to 'unassigned'
        /// </summary>
        /// <param name="instructionSetId">Id of the instruction set you want to delete</param>
        /// <returns></returns>
        /// <remarks>Calls DELETE on apiRoot/InstructionSets/Id/{id}</remarks>
        public ApiCallResponse<bool> Delete(int instructionSetId)
        {
            var apiCallResult = this.transportProxy.Delete(string.Format(this.byIdEndpoint, instructionSetId));
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionSets Api 'Delete' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Deletes given instruction set. Allows to provide a parameter that will cause all of the instruction definitions referenced by that set to be deleted as well.
        /// </summary>
        /// <param name="instructionSetId">Id of the instruction set you want to delete</param>
        /// <param name="shouldContentsBeDeleted">Flag indicating if you want to delete instruction definitions from given set as well</param>
        /// <returns></returns>
        /// <remarks>Calls DELETE on apiRoot/InstructionSets/Id/{id}?deleteContent={shouldContentsBeDeleted}</remarks>
        public ApiCallResponse<bool> DeleteWithContents(int instructionSetId, bool shouldContentsBeDeleted)
        {
            var apiCallResult = this.transportProxy.Delete(string.Format(this.byIdOptionalDeleteEndpoint, instructionSetId, shouldContentsBeDeleted));
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionSets Api 'DeleteWithContents' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Exports specific instruction set as a ZIP file that contains all instruction definitions that belong to that set.
        /// </summary>
        /// <param name="instructionSetId">Id of the set you want to export instruction definitions from</param>
        /// <returns>Memory stream containing the ZIP file</returns>
        /// <remarks>Calls apiRoot/InstructionSets/Id/{id}/Export</remarks>
        public ApiCallResponse<MemoryStream> Export(int instructionSetId)
        {
            var apiCallResult = this.transportProxy.DownloadFile(string.Format(this.exportByIdEndpoint, instructionSetId));
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionSets Api 'Export' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Adds/Moves specific instruction definition(s) to given instruction set.
        /// </summary>
        /// <param name="instructionSetId">Id of the set you want to add/move instruction definitions to. Null value means 'unassigned'</param>
        /// <param name="instructionDefinitionsIds">Id(s) of instruction definitions you want to add/move</param>
        /// <returns>Modified instruction set</returns>
        /// <remarks>Calls POST on apiRoot/InstructionSets/Contents</remarks>
        public ApiCallResponse<InstructionSet> AddInstructionDefinitions(int? instructionSetId, IEnumerable<int> instructionDefinitionsIds)
        {
            var payload = new InstructionSetToDefinitionLink
            {
                SetId = instructionSetId,
                InstructionDefinitionIds = instructionDefinitionsIds
            };

            var apiCallResult = this.transportProxy.Post<InstructionSetToDefinitionLink, InstructionSet>(payload, this.contentsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionSets Api 'AddInstructionDefinitions' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Adds/Moves specific instruction definition(s) to given instruction set.
        /// </summary>
        /// <param name="linkDefinition">Object that defines 1-to-Many relationship between Instruction Set and Instruction Definitions
        /// Passing null as SetId is allowed means 'unassigned'</param>
        /// <returns>Modified instruction set</returns>
        /// <remarks>Calls POST on apiRoot/InstructionSets/Contents</remarks>
        public ApiCallResponse<InstructionSet> AddInstructionDefinitions(InstructionSetToDefinitionLink linkDefinition)
        {
            var apiCallResult = this.transportProxy.Post<InstructionSetToDefinitionLink, InstructionSet>(linkDefinition, this.contentsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionSets Api 'AddInstructionDefinitions' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Removes specific instruction definition(s) from a given instruction set.
        /// </summary>
        /// <param name="instructionSetId">Id of the set you want to remove instruction definitions from</param>
        /// <param name="instructionDefinitionsIds">Id(s) of instruction definitions you want to remove</param>
        /// <returns></returns>
        /// <remarks>Calls DETELE on apiRoot/InstructionSets/Contents</remarks>
        public ApiCallResponse<bool> RemoveInstructionDefinitions(int instructionSetId, IEnumerable<int> instructionDefinitionsIds)
        {
            var payload = new InstructionSetToDefinitionLink
            {
                SetId = instructionSetId,
                InstructionDefinitionIds = instructionDefinitionsIds
            };

            var apiCallResult = this.transportProxy.Delete(payload, this.contentsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionSets Api 'RemoveInstructionDefinitions' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Removes specific instruction definition(s) from a given instruction set.
        /// </summary>
        /// <param name="linkDefinition">Object that defines 1-to-Many relationship between Instruction Set and Instruction Definitions
        /// Passing null as SetId is not allowed and will result in an error.</param>
        /// <returns></returns>
        /// <remarks>Calls DELETE on apiRoot/InstructionSets/Contents</remarks>
        public ApiCallResponse<bool> RemoveInstructionDefinitions(InstructionSetToDefinitionLink linkDefinition)
        {
            var apiCallResult = this.transportProxy.Delete(linkDefinition, this.contentsEndpoint);
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionSets Api 'RemoveInstructionDefinitions' call failed");
            }

            return apiCallResult;
        }

        /// <summary>
        /// Removes all instruction definitions from a given set
        /// </summary>
        /// <param name="instructionSetId">Id of the instruction set you want to empty</param>
        /// <returns></returns>
        /// <remarks>Calls DELETE on apiRoot/InstructionSets/Id/{setId:int}/Clear</remarks>
        public ApiCallResponse<bool> Clear(int instructionSetId)
        {
            var apiCallResult = this.transportProxy.Delete(string.Format(this.clearByIdEndpoint, instructionSetId));
            if (!apiCallResult.Success)
            {
                this.LogError("InstructionSets Api 'Clear' call failed");
            }

            return apiCallResult;
        }
    }
}
