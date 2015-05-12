#region Includes

using System.Collections.Concurrent;

#endregion

namespace Daishi.AMQP {
    public class ConsumptionRateDecreasedAMQPQueueMetricAnalyser : AMQPQueueMetricAnalyser {
        public ConsumptionRateDecreasedAMQPQueueMetricAnalyser(AMQPQueueMetricAnalyser analyser) : base(analyser) {}

        public override void Analyse(AMQPQueueMetric current, AMQPQueueMetric previous, ConcurrentBag<AMQPQueueMetric> busyQueues, ConcurrentBag<AMQPQueueMetric> quietQueues, int percentageDifference) {
            var percentageChange = PercentageChangeCalculator.Calculate(current.ConsumptionRate, previous.ConsumptionRate);

            if (percentageChange <= -percentageDifference) {
                current.AMQPQueueMetricAnalysisResult = AMQPQueueMetricAnalysisResult.ConsumptionRateDecreased;
                quietQueues.Add(current);
            }
            else if (current.ConsumptionRate.Equals(0)) {
                current.AMQPQueueMetricAnalysisResult = AMQPQueueMetricAnalysisResult.ConsumptionRateZero;
                quietQueues.Add(current);
            }
            else analyser.Analyse(current, previous, busyQueues, quietQueues, percentageDifference);
        }
    }
}