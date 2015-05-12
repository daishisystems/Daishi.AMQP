#region Includes

using System.Collections.Generic;

#endregion

namespace Daishi.AMQP {
    public class RabbitMQConsumerCatchAll : RabbitMQConsumer {
        public RabbitMQConsumerCatchAll(string queueName, int timeout, ushort prefetchCount = 1, bool noAck = false,
            bool createQueue = true, bool implicitAck = true, IDictionary<string, object> queueArgs = null) :
                base(queueName, timeout, prefetchCount, noAck, createQueue, implicitAck, queueArgs) {}

        public override void Start(AMQPAdapter amqpAdapter) {
            base.Start(amqpAdapter);
            Start(amqpAdapter, true);
        }
    }
}