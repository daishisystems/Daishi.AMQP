#region Includes

using System.Collections.Concurrent;

#endregion

namespace Daishi.AMQP {
    public class ConsumptionRateIncreasedAMQPQueueMetricAnalyser : AMQPQueueMetricAnalyser {
        public ConsumptionRateIncreasedAMQPQueueMetricAnalyser(AMQPQueueMetricAnalyser analyser) : base(analyser) {}

        public override void Analyse(AMQPQueueMetric current, AMQPQueueMetric previous, ConcurrentBag<AMQPQueueMetric> busyQueues, ConcurrentBag<AMQPQueueMetric> quietQueues, int percentageDifference) {
            var percentageChange = PercentageChangeCalculator.Calculate(current.ConsumptionRate, previous.ConsumptionRate);

            if (percentageChange >= percentageDifference) {
                current.AMQPQueueMetricAnalysisResult = AMQPQueueMetricAnalysisResult.ConsumptionRateIncreased;
                busyQueues.Add(current);
            }
            else analyser.Analyse(current, previous, busyQueues, quietQueues, percentageDifference);
        }
    }
}