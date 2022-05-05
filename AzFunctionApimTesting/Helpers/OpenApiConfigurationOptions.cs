using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace AzFunctionApimTesting.Helpers
{
    public class OpenApiConfigurationOptions : IOpenApiConfigurationOptions
    {
        public OpenApiInfo Info { get; set; } = new OpenApiInfo()
        {
            Version = "1.0.0",
            Title = "APIM Testing",
            Description = "",
        };

        public List<OpenApiServer> Servers { get; set; } = new List<OpenApiServer>();
        public OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V2;
        public bool IncludeRequestingHostName { get; set; }
        public bool ForceHttp { get; set; }
        public bool ForceHttps { get; set; }
    }
}
