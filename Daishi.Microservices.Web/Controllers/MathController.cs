#region Includes

using System.Net;
using System.Web.Http;
using Daishi.AMQP;
using RabbitMQ.Client.Events;

#endregion

namespace Daishi.Microservices.Web.Controllers {
    public class MathController : ApiController {
        [Route("api/math/{number}")]
        public string Get(int number) {
            var queue = QueuePool.Instance.Get();
            RabbitMQAdapter.Instance.Publish(string.Concat(number, ",", queue.Name), "Math");

            string message;
            BasicDeliverEventArgs args;

            var responded = RabbitMQAdapter.Instance.TryGetNextMessage(queue.Name, out message, out args, 5000);
            QueuePool.Instance.Put(queue);

            if (responded) {
                return message;
            }
            throw new HttpResponseException(HttpStatusCode.BadGateway);
        }
    }
}