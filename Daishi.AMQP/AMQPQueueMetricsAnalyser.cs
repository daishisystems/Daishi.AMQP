#region Includes

using System.Collections.Concurrent;
using System.Collections.Generic;

#endregion

namespace Daishi.AMQP {
    public abstract class AMQPQueueMetricsAnalyser {
        protected readonly AMQPQueueMetricAnalyser amqpQueueMetricAnalyser;
        protected readonly int percentageDifference;

        protected AMQPQueueMetricsAnalyser(AMQPQueueMetricAnalyser amqpQueueMetricAnalyser,
            int percentageDifference) {
            this.amqpQueueMetricAnalyser = amqpQueueMetricAnalyser;
            this.percentageDifference = percentageDifference;
        }

        public abstract void Analyse(Dictionary<string, AMQPQueueMetric> current,
            Dictionary<string, AMQPQueueMetric> previous,
            out ConcurrentBag<AMQPQueueMetric> busyQueues,
            out ConcurrentBag<AMQPQueueMetric> quietQueues);
    }
}