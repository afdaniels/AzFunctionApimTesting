using System;
using System.Net;
using System.Threading.Tasks;
using AzFunctionApimTesting.Models;
using AzFunctionApimTesting.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AzFunctionApimTesting.Functions
{
    public class TestBackendsEndpoint
    {
        private readonly ILogger<TestBackendsEndpoint> _logger;
        private readonly IApimService _apimService;

        public TestBackendsEndpoint(ILogger<TestBackendsEndpoint> log, IApimService apimService)
        {
            _logger = log;
            _apimService = apimService;
        }

        [FunctionName("TestBackendsEndpoint")]
        [OpenApiOperation(operationId: "Status", tags: new[] { "APIM Status" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(BackendModel), Description = "The OK response")]
        public Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            try
            {
                _logger.LogCritical("");
                var backendResults = _apimService.TestBackEnds();
                //var backendResults = new ApimService().TestBackEnds();
                return Task.FromResult<IActionResult>(new OkObjectResult(backendResults));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
            
        }
    }
}

