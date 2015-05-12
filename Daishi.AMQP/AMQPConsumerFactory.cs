#region Includes

using System.Collections.Generic;

#endregion

namespace Daishi.AMQP {
    public abstract class AMQPConsumerFactory {
        public abstract AMQPConsumer CreateAMQPConsumer(string queueName, int timeout, ushort prefetchCount = 1, bool noAck = false,
            bool createQueue = true, bool implicitAck = true, IDictionary<string, object> queueArgs = null);
    }
}