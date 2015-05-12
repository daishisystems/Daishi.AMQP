#region Includes

using System;
using System.Collections.Generic;

#endregion

namespace Daishi.AMQP {
    public abstract class AMQPConsumer {
        protected readonly string queueName;
        protected readonly ushort prefetchCount;
        protected readonly bool noAck;
        protected readonly bool createQueue;
        protected readonly int timeout;
        protected readonly bool implicitAck;
        protected readonly IDictionary<string, object> queueArgs;
        protected volatile bool stopConsuming;

        protected AMQPConsumer(string queueName, int timeout, ushort prefetchCount = 1, bool noAck = false,
            bool createQueue = true, bool implicitAck = true, IDictionary<string, object> queueArgs = null) {
            this.queueName = queueName;
            this.prefetchCount = prefetchCount;
            this.noAck = noAck;
            this.createQueue = createQueue;
            this.timeout = timeout;
            this.implicitAck = implicitAck;
            this.queueArgs = queueArgs;
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public virtual void Start(AMQPAdapter amqpAdapter) {
            stopConsuming = false;
        }

        public void Stop() {
            stopConsuming = true;
        }

        protected void OnMessageReceived(MessageReceivedEventArgs e) {
            var handler = MessageReceived;
            if (handler != null) handler(this, e);
        }
    }
}