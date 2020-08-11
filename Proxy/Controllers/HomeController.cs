using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Proxy.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(IConfiguration config, ILogger<HomeController> logger)
        {
            _logger = logger;
            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = true,
                ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(config["UpstreamUrl"])
            };
        }

        [HttpGet("{**path}")]
        [HttpPost("{**path}")]
        [HttpPut("{**path}")]
        [HttpPatch("{**path}")]
        [HttpDelete("{**path}")]
        public async Task<IActionResult> Get(string path)
        {
            _logger.LogInformation($"-> GET {path}");

            var request = new HttpRequestMessage(ToHttpMethod(Request.Method), path);
            foreach (var header in Request.Headers)
            {
                request.Headers.Add(header.Key, header.Value[0]);
            }

            request.Headers.Remove("host");

            if (Request.BodyReader != null)
            {
                var reader = new StreamReader(Request.BodyReader.AsStream());
                var bodyStr = await reader.ReadToEndAsync();
                request.Content = new StringContent(bodyStr);
            }

            var responseFromUpstream = await _httpClient.SendAsync(request);
            var bodyFromUpstream = await responseFromUpstream.Content.ReadAsStringAsync();

            var result = new ObjectResult(bodyFromUpstream)
            {
                StatusCode = (int) responseFromUpstream.StatusCode
            };

            Response.Headers.Clear();
            foreach (var header in responseFromUpstream.Headers)
            {
                Response.Headers.Add(header.Key, header.Value.First());
            }

            _logger.LogInformation($"<- {result.StatusCode}");
            return result;
        }

        private static HttpMethod ToHttpMethod(string requestMethod)
        {
            return requestMethod.ToUpperInvariant() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "PATCH" => HttpMethod.Patch,
                "DELETE" => HttpMethod.Delete,
                _ => throw new InvalidOperationException("Unsupported HTTP method")
            };
        }
    }
}
