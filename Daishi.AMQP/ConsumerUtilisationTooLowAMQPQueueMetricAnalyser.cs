#region Includes

using System.Collections.Concurrent;

#endregion

namespace Daishi.AMQP {
    public class ConsumerUtilisationTooLowAMQPQueueMetricAnalyser : AMQPQueueMetricAnalyser {
        public ConsumerUtilisationTooLowAMQPQueueMetricAnalyser(AMQPQueueMetricAnalyser analyser) : base(analyser) {}

        public override void Analyse(AMQPQueueMetric current, AMQPQueueMetric previous, ConcurrentBag<AMQPQueueMetric> busyQueues, ConcurrentBag<AMQPQueueMetric> quietQueues, int percentageDifference) {
            if (current.ConsumerUtilisation >= 0 && current.ConsumerUtilisation < 99) {
                current.AMQPQueueMetricAnalysisResult = AMQPQueueMetricAnalysisResult.ConsumerUtilisationTooLow;
                busyQueues.Add(current);
            }
            else analyser.Analyse(current, previous, busyQueues, quietQueues, percentageDifference);
        }
    }
}