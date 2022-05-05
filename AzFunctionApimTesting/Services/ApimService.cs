using System;
using System.Collections.Generic;
using System.Linq;
using AzFunctionApimTesting.Helpers;
using AzFunctionApimTesting.Models;
using Microsoft.Azure.Management.ApiManagement;
using Microsoft.Azure.Management.ApiManagement.Models;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace AzFunctionApimTesting.Services
{
    public class ApimService : IApimService
    {
        private readonly string _resourceGroupName = Environment.GetEnvironmentVariable("RESOURCE_GROUP");
        private readonly string _serviceName = Environment.GetEnvironmentVariable("SERVICE_NAME");
        private readonly string _subscriptionId = Environment.GetEnvironmentVariable("SUBSCRIPTION_ID");
        private readonly string _tenantId = Environment.GetEnvironmentVariable("TENANT_ID");
        private readonly string _clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
        private readonly string _clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

        readonly ApiManagementClient _client;
        readonly ApiManagementServiceResource _service;

        public ApimService()
        {
            _client = ConfigureClient();
            _service = ConfigureService();
        }

        public BackendContract GetBackend(string name)
        {
            return _client.Backend.Get(_resourceGroupName, _serviceName, name);
        }

        public List<BackendModel> GetBackends()
        {
            var backends = _client.Backend.ListByService(_resourceGroupName, _serviceName);
            return backends.Select(x => new BackendModel() {Name = x.Name, Url = x.Url}).ToList();
        }

        public List<BackendModel> TestBackEnds()
        {
            var backendResults = new List<BackendModel>();
            var backends = _client.Backend.ListByService(_resourceGroupName, _serviceName);
            foreach (var backend in backends)
            {
                var httpResult = HttpRequestHelper.HttpGetBackend(backend);
                var result = new BackendModel();
                result.Name = backend.Name;
                result.Url = backend.Url;
                result.Status = (int) httpResult.Result.StatusCode;
                backendResults.Add(result);
            }
            return backendResults;
        }

        private ApiManagementClient ConfigureClient()
        {
            try
            {
                var credentials = SdkContext.AzureCredentialsFactory
                    .FromServicePrincipal(_clientId,
                        _clientSecret,
                        _tenantId,
                        AzureEnvironment.AzureGlobalCloud);

                return new ApiManagementClient(credentials)
                {
                    SubscriptionId = _subscriptionId
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        private ApiManagementServiceResource ConfigureService()
        {
            return _client.ApiManagementService.Get(_resourceGroupName, _serviceName);
        }
    }
}
