using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ForOpenHack
{
    public static class GetRating
    {
        [FunctionName("GetRating")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            string ratingId;
            try
            {
                dynamic data = await req.Content.ReadAsAsync<object>();
                ratingId = data?.ratingId;
            }
            catch
            {
                return req.CreateResponse(HttpStatusCode.NotFound, "");
            }

            string sql = string.Format("Select id, userId, productId, timestamp, locationName, rating, userNotes From Ratings where id = '{0}';", ratingId);

            return req.CreateResponse(HttpStatusCode.OK, sql);
        }
    }
}
