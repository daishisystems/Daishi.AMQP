#region Includes

using System.Collections.Generic;

#endregion

namespace Daishi.AMQP {
    public class RabbitMQConsumerFactory : AMQPConsumerFactory {
        private readonly bool _catchAllConsumerExceptions;

        public RabbitMQConsumerFactory(bool catchAllConsumerExceptions) {
            _catchAllConsumerExceptions = catchAllConsumerExceptions;
        }

        public override AMQPConsumer CreateAMQPConsumer(string queueName, int timeout, ushort prefetchCount = 1, bool noAck = false,
            bool createQueue = true, bool implicitAck = true, IDictionary<string, object> queueArgs = null) {
            switch (_catchAllConsumerExceptions) {
                case true:
                    return new RabbitMQConsumerCatchAll(queueName, timeout, prefetchCount, noAck, createQueue, implicitAck, queueArgs);
                default:
                    return new RabbitMQConsumerCatchOne(queueName, timeout, prefetchCount, noAck, createQueue, implicitAck, queueArgs);
            }
        }
    }
}