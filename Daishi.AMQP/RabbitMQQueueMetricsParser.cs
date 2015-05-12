#region Includes

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

#endregion

namespace Daishi.AMQP {
    internal static class RabbitMQQueueMetricsParser {
        public static Dictionary<string, AMQPQueueMetric> Parse(string rabbitMQQueueMetrics) {
            var root = JArray.Parse(rabbitMQQueueMetrics);
            var metrics = new ConcurrentDictionary<string, AMQPQueueMetric>();

            Parallel.ForEach(root, token => {
                var queueName = (string) token["name"];
                if (queueName.ToLowerInvariant().Contains("diagnostics")) return;
                double consumptionRate, dispatchRate;

                MessageStatsParser.Parse(token, out consumptionRate, out dispatchRate);
                metrics.TryAdd(queueName, new RabbitMQQueueMetric {
                    QueueName = queueName,
                    Length = (int) token["messages"],
                    ConsumptionRate = consumptionRate,
                    DispatchRate = dispatchRate,
                    ConsumerUtilisation = ConsumerUtilisationParser.Parse((string) token["consumer_utilisation"])
                });
            });

            return new Dictionary<string, AMQPQueueMetric>(metrics);
        }
    }
}