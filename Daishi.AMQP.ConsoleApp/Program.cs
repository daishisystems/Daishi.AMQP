#region Includes

using System;
using RabbitMQ.Client.Events;

#endregion

namespace Daishi.AMQP.ConsoleApp {
    internal class Program {
        private static void Main(string[] args) {
            var adapter = RabbitMQAdapter.Instance;

            adapter.Init("hostName", 1234, "userName", "password", 50);
            adapter.Connect();

            var message = "Hello, World!";
            adapter.Publish(message, "queueName");

            string output;
            BasicDeliverEventArgs eventArgs;
            adapter.TryGetNextMessage("queueName", out output, out eventArgs, 50);

            var consumer = new RabbitMQConsumerCatchAll("queueName", 10);
            consumer.MessageReceived += consumer_MessageReceived;
            adapter.ConsumeAsync(consumer);

            Console.ReadLine();

            adapter.StopConsumingAsync(consumer);
        }

        private static void consumer_MessageReceived(object sender, MessageReceivedEventArgs e) {
            throw new NotImplementedException();
        }
    }
}