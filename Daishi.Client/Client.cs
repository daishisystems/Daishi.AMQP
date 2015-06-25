#region Includes

using Daishi.AMQP;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

#endregion

namespace Daishi.Client {
    public class Request {
        public string Command { get; set; }
        public object Payload { get; set; }

        public string ToJSON() {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class Response {
        public string Description { get; set; }
        public object Value { get; set; }
    }

    public class Client {
        private readonly AMQPAdapter _adapter;

        public Client() {
            _adapter = new RabbitMQAdapter();
            _adapter.Connect();
        }

        public void Push(Request r, string queueName) {
            _adapter.Publish(r.ToJSON(), queueName);
        }

        public Response Pop(string queueName, int timeout) {
            string rawMessage;
            BasicDeliverEventArgs e;
            _adapter.TryGetNextMessage(queueName, out rawMessage, out e, timeout);

            return JsonConvert.DeserializeObject<Response>(rawMessage);
        }

        public void Disconnect() {
            if (_adapter != null && _adapter.IsConnected) {
                _adapter.Disconnect();
            }
        }
    }
}