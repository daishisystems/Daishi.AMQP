#region Includes

using System;
using System.Collections.Concurrent;

#endregion

namespace Daishi.AMQP {
    public class QueuePool {
        private static readonly QueuePool _instance = new QueuePool(
            () => new RabbitMQQueue {
                Name = Guid.NewGuid().ToString(),
                IsNew = true
            });

        private readonly Func<AMQPQueue> _amqpQueueGenerator;
        private readonly ConcurrentBag<AMQPQueue> _amqpQueues;

        static QueuePool() {}

        public static QueuePool Instance
        {
            get { return _instance; }
        }

        private QueuePool(Func<AMQPQueue> amqpQueueGenerator) {
            _amqpQueueGenerator = amqpQueueGenerator;
            _amqpQueues = new ConcurrentBag<AMQPQueue>();

            var manager = new RabbitMQQueueMetricsManager(false, "localhost", 15672, "paul", "password");
            var queueMetrics = manager.GetAMQPQueueMetrics();

            foreach (var queueMetric in queueMetrics.Values) {
                Guid queueName;
                var isGuid = Guid.TryParse(queueMetric.QueueName, out queueName);

                if (isGuid) {
                    _amqpQueues.Add(new RabbitMQQueue {IsNew = false, Name = queueName.ToString()});
                }
            }
        }

        public AMQPQueue Get() {
            AMQPQueue queue;

            var queueIsAvailable = _amqpQueues.TryTake(out queue);
            return queueIsAvailable ? queue : _amqpQueueGenerator();
        }

        public void Put(AMQPQueue queue) {
            _amqpQueues.Add(queue);
        }
    }
}