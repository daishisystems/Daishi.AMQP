#region Includes

using System;
using Daishi.AMQP;
using Daishi.Math;

#endregion

namespace Daishi.Microservices {
    public class SimpleMathMicroservice : Microservice {
        private RabbitMQAdapter _adapter;
        private RabbitMQConsumerCatchAll _rabbitMQConsumerCatchAll;
        private RabbitMQConsumerCatchAll _autoScaleConsumerCatchAll;

        public void Init() {
            _adapter = RabbitMQAdapter.Instance;
            _adapter.Init("localhost", 5672, "guest", "guest", 50);

            _rabbitMQConsumerCatchAll = new RabbitMQConsumerCatchAll("Math", 10);
            _rabbitMQConsumerCatchAll.MessageReceived += OnMessageReceived;

            _autoScaleConsumerCatchAll = new RabbitMQConsumerCatchAll("AutoScale", 10);
            _autoScaleConsumerCatchAll.MessageReceived += _autoScaleConsumerCatchAll_MessageReceived;

            _adapter.Connect();
            _adapter.ConsumeAsync(_autoScaleConsumerCatchAll);
            _adapter.ConsumeAsync(_rabbitMQConsumerCatchAll);
        }

        void _autoScaleConsumerCatchAll_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("I need to scale out!"); // todo: Add another consumer...
        }

        public void OnMessageReceived(object sender, MessageReceivedEventArgs e) {
            var paramaters = e.Message.Split(',');

            var number = Convert.ToInt32(paramaters[0]);
            var result = Functions.Double(number);

            var queueName = paramaters[1];
            _adapter.Publish(result.ToString(), queueName);
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

/*todo: All logic is now covered from a UT perspective. All that remains is
  to implement a Test Harness. Remember to push this to Vlad.*/