namespace Daishi.AMQP {
    public enum AMQPQueueMetricAnalysisResult {
        QueueLengthIncreased,
        ConsumptionRateIncreased,
        DispatchRateDecreased,
        ConsumerUtilisationTooLow,
        ConsumptionRateDecreased,
        ConsumptionRateZero,
        Stable
    }
}