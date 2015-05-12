namespace Daishi.AMQP {
    public abstract class AMQPQueueMetric {
        public string QueueName { get; set; }
        public int Length { get; set; }
        public double ConsumptionRate { get; set; }
        public double DispatchRate { get; set; }
        public int ConsumerUtilisation { get; set; }
        public AMQPQueueMetricAnalysisResult AMQPQueueMetricAnalysisResult { get; set; }
    }
}