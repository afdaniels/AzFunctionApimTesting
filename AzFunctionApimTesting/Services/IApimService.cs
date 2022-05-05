using System.Collections.Generic;
using AzFunctionApimTesting.Models;
using Microsoft.Azure.Management.ApiManagement.Models;

namespace AzFunctionApimTesting.Services
{
    public interface IApimService
    {
        BackendContract GetBackend(string name);
        List<BackendModel> GetBackends();
        List<BackendModel> TestBackEnds();
    }
}
