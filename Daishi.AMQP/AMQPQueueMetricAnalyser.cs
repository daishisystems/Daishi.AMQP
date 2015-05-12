#region Includes

using System.Collections.Concurrent;

#endregion

namespace Daishi.AMQP {
    public abstract class AMQPQueueMetricAnalyser {
        protected readonly AMQPQueueMetricAnalyser analyser;

        protected AMQPQueueMetricAnalyser(AMQPQueueMetricAnalyser analyser) {
            this.analyser = analyser;
        }

        public abstract void Analyse(AMQPQueueMetric current, AMQPQueueMetric previous, ConcurrentBag<AMQPQueueMetric> busyQueues, ConcurrentBag<AMQPQueueMetric> quietQueues, int percentageDifference);
    }
}