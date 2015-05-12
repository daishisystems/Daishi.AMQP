#region Includes

using System.Collections.Concurrent;

#endregion

namespace Daishi.AMQP {
    public abstract class AMQPConsumerNotifier {
        protected readonly AMQPAdapter amqpAdapter;
        protected readonly string exchangeName;

        protected AMQPConsumerNotifier(AMQPAdapter amqpAdapter, string exchangeName) {
            this.amqpAdapter = amqpAdapter;
            this.exchangeName = exchangeName;
        }

        public abstract void Notify(ConcurrentBag<AMQPQueueMetric> busyQueues,
            ConcurrentBag<AMQPQueueMetric> quietQueues);
    }
}