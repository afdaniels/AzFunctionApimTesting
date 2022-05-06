using System;
using AzFunctionApimTesting.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzFunctionApimTesting.Startup))]

namespace AzFunctionApimTesting;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        if (builder == null)
            throw new ArgumentException(nameof(builder));

        builder.Services.AddScoped<IApimService, ApimService>();
    }
}

