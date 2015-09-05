#region Includes

using System;
using System.Linq;
using System.Threading;
using Daishi.AMQP;

#endregion

namespace Daishi.Microservices {
    public class QueueWatchMicroservice : Microservice {
        private RabbitMQAdapter _adapter;
        private RabbitMQConsumerCatchAll _rabbitMQConsumerCatchAll;

        public void Init() {

            var amqpQueueMetricsManager = new RabbitMQQueueMetricsManager(false, "localhost", 15672, "paul", "password");
            AMQPQueueMetricsAnalyser amqpQueueMetricsAnalyser = new RabbitMQQueueMetricsAnalyser(
                new ConsumerUtilisationTooLowAMQPQueueMetricAnalyser(
                    new ConsumptionRateIncreasedAMQPQueueMetricAnalyser(
                        new DispatchRateDecreasedAMQPQueueMetricAnalyser(
                            new QueueLengthIncreasedAMQPQueueMetricAnalyser(
                                new ConsumptionRateDecreasedAMQPQueueMetricAnalyser(
                                    new StableAMQPQueueMetricAnalyser()))))), 20);
            AMQPConsumerNotifier amqpConsumerNotifier = new RabbitMQConsumerNotifier(RabbitMQAdapter.Instance, "monitor");           
            var queueWatch = new QueueWatch(amqpQueueMetricsManager, amqpQueueMetricsAnalyser, amqpConsumerNotifier, 5000);
            queueWatch.AMQPQueueMetricsAnalysed += QueueWatchOnAMQPQueueMetricsAnalysed;

            queueWatch.StartAsync();
        }

        private void QueueWatchOnAMQPQueueMetricsAnalysed(object sender, AMQPQueueMetricsAnalysedEventArgs e) {

            Console.Clear();
            if (e.BusyQueues.Any()) {
                foreach (var busy in e.BusyQueues)
                    Console.WriteLine(string.Concat(busy.QueueName, ": ", busy.AMQPQueueMetricAnalysisResult));
            }
            if (!e.QuietQueues.Any()) return;
            foreach (var quiet in e.QuietQueues)
                Console.WriteLine(string.Concat(quiet.QueueName, ": ", quiet.AMQPQueueMetricAnalysisResult));

            Console.WriteLine("-----");

            int workerThreads, ioThreads;
            ThreadPool.GetAvailableThreads(out workerThreads, out ioThreads);
            Console.WriteLine(string.Concat("Worker Threads: ", workerThreads));
            Console.WriteLine(string.Concat("IO Threads: ", ioThreads));

            Console.WriteLine("-----");
        }

        public void OnMessageReceived(object sender, MessageReceivedEventArgs e) {
            throw new NotImplementedException();
        }

        public void Shutdown() {
            throw new NotImplementedException();
        }
    }
}