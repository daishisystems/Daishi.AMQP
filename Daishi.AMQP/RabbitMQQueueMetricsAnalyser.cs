#region Includes

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace Daishi.AMQP {
    public class RabbitMQQueueMetricsAnalyser : AMQPQueueMetricsAnalyser {
        public RabbitMQQueueMetricsAnalyser(AMQPQueueMetricAnalyser amqpQueueMetricAnalyser,
            int percentageDifference)
            : base(amqpQueueMetricAnalyser, percentageDifference) {}

        public override void Analyse(Dictionary<string, AMQPQueueMetric> current,
            Dictionary<string, AMQPQueueMetric> previous,
            out ConcurrentBag<AMQPQueueMetric> busyQueues,
            out ConcurrentBag<AMQPQueueMetric> quietQueues) {
            busyQueues = new ConcurrentBag<AMQPQueueMetric>();
            quietQueues = new ConcurrentBag<AMQPQueueMetric>();

            var busy = busyQueues;
            var quiet = quietQueues;

            Parallel.ForEach(current, currentAmqpQueueMetric => {
                AMQPQueueMetric previousAmqpQueueMetric;
                var hasPrevious = previous.TryGetValue(currentAmqpQueueMetric.Key, out previousAmqpQueueMetric);
                if (!hasPrevious) return;

                amqpQueueMetricAnalyser.Analyse(currentAmqpQueueMetric.Value, previousAmqpQueueMetric, busy, quiet, percentageDifference);
            });
        }
    }
}