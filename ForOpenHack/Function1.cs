using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ForOpenHack
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", "get", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string productId = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "productId", true) == 0)
                .Value;

            if (productId == null)
            {
                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();
                productId = data?.productId;
            }

            return productId == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "The product name for your product id nullId is Starfruit Explosion")
                : req.CreateResponse(HttpStatusCode.OK, "The product name for your product id " + productId + " is Starfruit Explosion");
            
        }
    }
}
