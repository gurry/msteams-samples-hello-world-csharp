namespace Tachyon.SDK.Consumer.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;

    using Newtonsoft.Json.Linq;

    using Tachyon.SDK.Consumer.Enums;
    using Tachyon.SDK.Consumer.Models.Api;

    /// <summary>
    /// Utility for creating error objects
    /// </summary>
    public static class ErrorCreator
    {
        /// <summary>
        /// Creates an error object from Http response object
        /// </summary>
        /// <param name="response">Http response message object</param>
        /// <returns>Collection of errors</returns>
        public static IEnumerable<Error> CreateErrorObject(HttpResponseMessage response)
        {
            List<Error> errors = new List<Error>();
            var rawResult = response.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(rawResult))
            {
                if (rawResult.StartsWith("<!DOCTYPE "))
                {
                    ParseReturnedHtml(rawResult, ref errors);
                }
                else
                {
                    ParseReturnedJson(rawResult, ref errors);
                }

                if (errors != null && errors.Count > 0)
                {
                    return errors;
                }
            }

            var errorMessage = string.Format(
                "Http error has occurred. Status code '{0}', message: {1}",
                response.StatusCode,
                rawResult);
            var error = new Error { ErrorType = ErrorType.Other, Message = errorMessage };

            return new List<Error>() { error };
        }

        /// <summary>
        /// Creates an error object from an exception
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Collection of errors</returns>
        public static IEnumerable<Error> CreateErrorObject(Exception exception)
        {
            string msg = string.Format("An exception has occurred: {0}", exception.Message);

            for (Exception ex = exception.InnerException; ex != null; ex = ex.InnerException)
            {
                msg += " " + ex.Message;
            }

            var error = new Error
            {
                ErrorType = ErrorType.Exception,
                Message = msg
            };

            return new List<Error> { error };
        }

        private static void ParseReturnedJson(string rawResult, ref List<Error> errors)
        {
            var receivedObject = JToken.Parse(rawResult);
            if (receivedObject != null)
            {
                if (receivedObject is JArray)
                {
                    errors = receivedObject.ToObject<List<Error>>();
                }
                else if (receivedObject is JObject)
                {
                    var singleError = receivedObject.ToObject<Error>();

                    // If we received an ExceptionMessage, append it to the message.
                    foreach (JToken jt in receivedObject.Children())
                    {
                        if (jt.Path == "ExceptionMessage")
                        {
                            singleError.Message += " " + ((dynamic)receivedObject).ExceptionMessage.ToString();
                            break;
                        }
                    }

                    errors.Add(singleError);
                }
            }
        }

        private static void ParseReturnedHtml(string rawResult, ref List<Error> errors)
        {
            string message = null;

            // We previously tried to parse the HTML to extract significant text. However, since the error page will vary depending on the version and configuration of IIS, and could even be a completely custom page,
            // we now return a "generic" error and attach the whole raw HTML to the Error Data so that the caller can examine it.
            ////Regex re = new Regex("<h4>(.*?)</h4>", RegexOptions.IgnoreCase);
            ////MatchCollection mc = re.Matches(rawResult);
            ////if (mc.Count > 0)
            ////{
            ////    Match m = mc[0];
            ////    if (m.Groups.Count > 1)
            ////    {
            ////        message = m.Groups[1].Value;
            ////    }
            ////    else
            ////    {
            ////        message = m.ToString();
            ////    }
            ////}

            message = "The server returned an error in the form of an HTML page";

            Error error = new Error();
            error.ErrorType = ErrorType.Other;
            error.Message = message;
            error.Data = new string[] { rawResult };
            errors.Add(error);
        }
    }
}
