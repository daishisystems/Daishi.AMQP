#region Includes

using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

#endregion

namespace Daishi.AMQP {
    public class RabbitMQQueueMetricsManager : AMQPQueueMetricsManager {
        public RabbitMQQueueMetricsManager(bool isSecure, string hostName,
            int port, string userName, string password) : base(isSecure, hostName, port, userName, password) {}

        public override Dictionary<string, AMQPQueueMetric> GetAMQPQueueMetrics() {
            var request = RabbitMQHTTPRequest.Create(isSecure ? "https" : "http", hostName, port, @"api/queues", userName, password);
            var content = new MemoryStream();

            using (var response = request.GetResponse()) {
                using (var responseStream = response.GetResponseStream())
                    if (responseStream != null)
                        responseStream.CopyTo(content);
                    else
                        throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            var rabbitMQQueueMetrics = Encoding.UTF8.GetString(content.ToArray());
            return RabbitMQQueueMetricsParser.Parse(rabbitMQQueueMetrics);
        }

        public override async Task<Dictionary<string, AMQPQueueMetric>> GetAMQPQueueMetricsAsync() {
            var request = RabbitMQHTTPRequest.Create(isSecure ? "https" : "http", hostName, port, @"api/queues", userName, password);
            var content = new MemoryStream();

            using (var response = await request.GetResponseAsync()) {
                using (var responseStream = response.GetResponseStream())
                    if (responseStream != null)
                        await responseStream.CopyToAsync(content);
                    else
                        throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            var rabbitMQQueueMetrics = Encoding.UTF8.GetString(content.ToArray());
            return RabbitMQQueueMetricsParser.Parse(rabbitMQQueueMetrics);
        }
    }
}