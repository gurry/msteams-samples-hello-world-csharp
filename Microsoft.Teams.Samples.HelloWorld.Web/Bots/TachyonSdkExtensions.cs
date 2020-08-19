using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tachyon.SDK.Consumer.Api;
using Tachyon.SDK.Consumer.Enums;
using Tachyon.SDK.Consumer.Models.Api;
using Tachyon.SDK.Consumer.Models.Common;
using Tachyon.SDK.Consumer.Models.Receive;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Bots
{
    public static class TachyonSdkExtensions
    {
        private static List<InstructionHint> _hardCodedInstructions = new List<InstructionHint>
        {
            new InstructionHint()
            {
                ReadableName = "What processes are running?",
                Description = "Get all running processes"
            },
            new InstructionHint
            {
                ReadableName = "What services are running?",
                Description = "Retrieves all the running services. Windows only",
            },
            new InstructionHint
            {
                ReadableName = "What processor types are being used?",
                Description = "Get processor types being used by devices. Windows only",
            },
            new InstructionHint
            {
                ReadableName = "Which hard drives are installed?",
                Description = "Get details of physical hard drives. Windows only",
            },
        };


        private static readonly List<InstructionType> InstructionTypes = new List<InstructionType>
            {InstructionType.Action, InstructionType.Question};

        public static IList<InstructionHint> Search(this InstructionDefinitions defs, string searchString)
        {
            if (searchString.Length < 3) // must be at least three chars for tachyon to return anything
            {
                return new List<InstructionHint>();
            }

            var result = MakeTachyonCall(() => defs.GetInstructionDefinitionHints(searchString, InstructionTypes));
            if (result.Result != InstructionHintSearchResult.Successful || result.Error?.Length > 0)
            {
                var error = result.Error.Length > 0 ? result.Error[0].ToString() : "Unknown error";
                throw new Exception($"Error while searching instructions: {error}");
            }

            return result.Instructions;
        }

        public static T MakeTachyonCall<T>(Func<ApiCallResponse<T>> call) where T : class
        {
             ApiCallResponse<T> response;
            try
            {
                response = call();
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception while calling Tachyon: {ex}", ex);
            }

            if (response == null)
            {
                throw new Exception("Tachyon returned null response");
            }

            if (!response.Success)
            {
                var error = response.Errors?.FirstOrDefault();
                var errorMsg = error != null ? ": " + error.Message : "";
                throw new Exception($"Tachyon returned an error response {errorMsg}");
            }

            var receivedObject = response.ReceivedObject;

            if (receivedObject == null)
            {
                throw new Exception("Tachyon returned response with null 'ReceivedObj'");
            }

            return receivedObject;
        }
    }
}
