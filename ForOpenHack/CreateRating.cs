using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ForOpenHack
{
    public static class CreateRating
    {
        [FunctionName("CreateRating")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            string userId, productId;
            string locationName, userNotes;
            int rating;
            string timestamp;
            try
            {
                dynamic data = await req.Content.ReadAsAsync<object>();
                userId = data?.userId;
                productId = data?.productId;

                // ratingåüèÿ
                string tmp = data?.rating;
                rating = int.Parse(tmp);
                if (rating < 0 || rating > 5)
                    return req.CreateResponse(HttpStatusCode.NotFound, "");
                
                locationName = data?.locationName;
                userNotes = data?.userNotes;
                timestamp = data?.timestamp;
            }
            catch
            {
                return req.CreateResponse(HttpStatusCode.NotFound, "");
            }

            string sql = "DECLARE @ID uniqueidentifier;";
            sql += "SET @ID = NEWID();";
            sql += string.Format("Insert Into Ratings(id, userId, productId, timestamp, locationName, rating, userNotes) VALUES(@ID,'{0}','{1}','{2}','{3}',{4},'{5}');", userId, productId, timestamp, locationName, rating, userNotes);
            sql += "Select id, userId, productId, timestamp, locationName, rating, userNotes From Ratings where id = @ID;";
            
            return req.CreateResponse(HttpStatusCode.OK, sql);
        }
    }
}
