#region Includes

using System.Collections.Concurrent;

#endregion

namespace Daishi.AMQP {
    public class DispatchRateDecreasedAMQPQueueMetricAnalyser : AMQPQueueMetricAnalyser {
        public DispatchRateDecreasedAMQPQueueMetricAnalyser(AMQPQueueMetricAnalyser analyser) : base(analyser) {}

        public override void Analyse(AMQPQueueMetric current, AMQPQueueMetric previous, ConcurrentBag<AMQPQueueMetric> busyQueues, ConcurrentBag<AMQPQueueMetric> quietQueues, int percentageDifference) {
            var percentageChange = PercentageChangeCalculator.Calculate(current.DispatchRate, previous.DispatchRate);

            if (percentageChange <= -percentageDifference) {
                current.AMQPQueueMetricAnalysisResult = AMQPQueueMetricAnalysisResult.DispatchRateDecreased;
                busyQueues.Add(current);
            }
            else analyser.Analyse(current, previous, busyQueues, quietQueues, percentageDifference);
        }
    }
}