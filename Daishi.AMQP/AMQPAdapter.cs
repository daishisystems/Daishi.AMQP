#region Includes

using System;
using System.Collections.Generic;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

#endregion

namespace Daishi.AMQP {
    public abstract class AMQPAdapter : IDisposable {
        protected string hostName;
        protected string virtualHost;
        protected int port;
        protected string userName;
        protected string password;
        protected ushort heartbeat;

        public abstract bool IsConnected { get; }

        public abstract AMQPAdapter Init(string hostName, int port, string userName, string password, ushort heartbeat);

        public abstract AMQPAdapter Init(string hostName, string virtualHost, int port, string userName, string password, ushort heartbeat);

        public abstract void Connect();
        public abstract void Disconnect();

        public abstract object GetConnection();

        public abstract void Publish(string message, string queueName, bool createQueue = true,
            IBasicProperties messageProperties = null, IDictionary<string, object> queueArgs = null);

        public abstract void Publish(string message, string exchangeName, string routingKey,
            IBasicProperties messageProperties = null);

        public abstract bool TryGetNextMessage(string queueName, out string message, out BasicDeliverEventArgs eventArgs, int timeout,
            bool createQueue = true, bool noAck = false, bool implicitAck = true, IDictionary<string, object> queueArgs = null);

        public abstract void AcknowledgeMessage(ulong deliveryTag);

        public void ConsumeAsync(AMQPConsumer consumer) {
            if (!IsConnected) Connect();

            var thread = new Thread(o => consumer.Start(this));
            thread.Start();

            while (!thread.IsAlive)
                Thread.Sleep(1);
        }

        public void StopConsumingAsync(AMQPConsumer consumer) {
            consumer.Stop();
        }

        void IDisposable.Dispose() {
            Disconnect();
        }
    }
}