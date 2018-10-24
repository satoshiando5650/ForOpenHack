using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ForOpenHack
{
    public static class GetRatings
    {
        [FunctionName("GetRatings")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            string userId;
            try
            {
                dynamic data = await req.Content.ReadAsAsync<object>();
                userId = data?.userId;
            }
            catch
            {
                return req.CreateResponse(HttpStatusCode.NotFound, "");
            }

            string sql = string.Format("Select id, userId, productId, timestamp, locationName, rating, userNotes From Ratings where userId = '{0}';", userId);

            return req.CreateResponse(HttpStatusCode.OK, sql);
        }
    }
}
