#region Includes

using System.Collections.Concurrent;

#endregion

namespace Daishi.AMQP {
    public class QueueLengthIncreasedAMQPQueueMetricAnalyser : AMQPQueueMetricAnalyser {
        public QueueLengthIncreasedAMQPQueueMetricAnalyser(AMQPQueueMetricAnalyser analyser) : base(analyser) {}

        public override void Analyse(AMQPQueueMetric current, AMQPQueueMetric previous, ConcurrentBag<AMQPQueueMetric> busyQueues, ConcurrentBag<AMQPQueueMetric> quietQueues, int percentageDifference) {
            if (current.Length > 100) {
                var percentageChange = PercentageChangeCalculator.Calculate(current.Length, previous.Length);

                if (percentageChange >= percentageDifference) {
                    current.AMQPQueueMetricAnalysisResult = AMQPQueueMetricAnalysisResult.QueueLengthIncreased;
                    busyQueues.Add(current);
                }
                else analyser.Analyse(current, previous, busyQueues, quietQueues, percentageDifference);
            }
            else
                analyser.Analyse(current, previous, busyQueues, quietQueues, percentageDifference);
        }
    }
}