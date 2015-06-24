#region Includes

using System;
using Daishi.AMQP;
using Daishi.Words;

#endregion

namespace Daishi.Microservices {
    public class SimpleWordMicroservice : Microservice {
        private RabbitMQAdapter _adapter;
        private RabbitMQConsumerCatchAll _rabbitMQConsumerCatchAll;

        public void Init() {
            _adapter = RabbitMQAdapter.Instance;
            _adapter.Init("hostName", 1234, "userName", "password", 50);

            _rabbitMQConsumerCatchAll = new RabbitMQConsumerCatchAll("queueName", 10);
            _rabbitMQConsumerCatchAll.MessageReceived += OnMessageReceived;

            _adapter.Connect();
            _adapter.ConsumeAsync(_rabbitMQConsumerCatchAll);
        }

        public void OnMessageReceived(object sender, MessageReceivedEventArgs e) {
            var result = Functions.Reverse(e.Message);
            Console.WriteLine(result);
        }

        public void Shutdown() {
            if (_adapter == null) return;

            if (_rabbitMQConsumerCatchAll != null) {
                _adapter.StopConsumingAsync(_rabbitMQConsumerCatchAll);
            }

            _adapter.Disconnect();
        }
    }
}