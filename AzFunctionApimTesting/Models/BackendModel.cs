using Newtonsoft.Json;

namespace AzFunctionApimTesting.Models
{
    public class BackendModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
