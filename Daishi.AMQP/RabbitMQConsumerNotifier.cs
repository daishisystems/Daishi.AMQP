#region Includes

using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace Daishi.AMQP {
    public class RabbitMQConsumerNotifier : AMQPConsumerNotifier {
        public RabbitMQConsumerNotifier(AMQPAdapter amqpAdapter, string exchangeName) : base(amqpAdapter, exchangeName) {}

        public override void Notify(ConcurrentBag<AMQPQueueMetric> busyQueues,
            ConcurrentBag<AMQPQueueMetric> quietQueues) {
            if (busyQueues.Any()) {
                if (!amqpAdapter.IsConnected)
                    amqpAdapter.Connect();

                Parallel.ForEach(busyQueues, amqpQueueMetric => amqpAdapter.Publish(ScaleMessage.Create(ScaleDirective.Out),
                    exchangeName, amqpQueueMetric.QueueName));
            }

            if (!quietQueues.Any()) return;
            if (!amqpAdapter.IsConnected)
                amqpAdapter.Connect();

            Parallel.ForEach(quietQueues.Where(q => !q.AMQPQueueMetricAnalysisResult.Equals(AMQPQueueMetricAnalysisResult.Stable)), amqpQueueMetric => amqpAdapter.Publish(ScaleMessage.Create(ScaleDirective.In),
                exchangeName, amqpQueueMetric.QueueName));
        }
    }
}