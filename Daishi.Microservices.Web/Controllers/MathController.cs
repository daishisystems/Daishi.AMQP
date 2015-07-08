#region Includes

using System.Net;
using System.Web.Http;
using Daishi.AMQP;
using RabbitMQ.Client.Events;

#endregion

namespace Daishi.Microservices.Web.Controllers {
    public class MathController : ApiController {
        public string Get(int id) {
            RabbitMQAdapter.Instance.Publish(id.ToString(), "Math");

            string message;
            BasicDeliverEventArgs args;
            var responded = RabbitMQAdapter.Instance.TryGetNextMessage("MathResponse", out message, out args, 5000);

            if (responded) {
                return message;
            }
            throw new HttpResponseException(HttpStatusCode.BadGateway);
        }
    }
}