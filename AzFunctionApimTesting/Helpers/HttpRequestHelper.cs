using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Management.ApiManagement.Models;
using Newtonsoft.Json;

namespace AzFunctionApimTesting.Helpers
{
    public class HttpRequestHelper
    {
        public static async Task<T> Get<T>(string url)
        {
            HttpClient client = new();
            var response = await client.GetAsync(url);
            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }

        public static async Task<HttpResponseMessage> HttpGetBackend(BackendContract contract)
        {
            HttpClient client = new();
            var clientHeaders = (from x in contract.Credentials.Header
                select x).ToList();

            if (!clientHeaders.Any()) return await client.GetAsync(contract.Url + contract.Description);
            foreach (var (key, value) in clientHeaders)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(key, value.First());
            }
            //HACK -- put test path into backend desc
            return await client.GetAsync(contract.Url + contract.Description);
        }

        public static async Task<T> Post<T>(string url, string payload, string headerKey, string headerValue)
        {
            var jsonObject = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpClient client = new();
            if (!string.IsNullOrEmpty(headerValue))
                client.DefaultRequestHeaders.TryAddWithoutValidation(headerKey, headerValue);
            var request = await client.PostAsync(url, jsonObject);
            return JsonConvert.DeserializeObject<T>(request.Content.ReadAsStringAsync().Result);
        }
    }
}
