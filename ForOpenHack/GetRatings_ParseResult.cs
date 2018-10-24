using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ForOpenHack
{
    public static class GetRatings_ParseResult
    {
        [FunctionName("GetRatings_ParseResult")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
                dynamic data = await req.Content.ReadAsAsync<object>();
                string result = "" + data?.result.ResultSets.Table1;
                return req.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "parse result failure");
            }
        }
    }
}
