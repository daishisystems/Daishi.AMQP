#region Includes

using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

#endregion

namespace Daishi.AMQP {
    public abstract class RabbitMQConsumer : AMQPConsumer {
        protected RabbitMQConsumer(string queueName, int timeout, ushort prefetchCount = 1, bool noAck = false,
            bool createQueue = true, bool implicitAck = true, IDictionary<string, object> queueArgs = null)
            : base(queueName, timeout, prefetchCount, noAck, createQueue, implicitAck, queueArgs) {}

        protected void Start(AMQPAdapter amqpAdapter, bool catchAllExceptions) {
            base.Start(amqpAdapter);
            try {
                var connection = (IConnection) amqpAdapter.GetConnection();

                using (var channel = connection.CreateModel()) {
                    if (createQueue) channel.QueueDeclare(queueName, true, false, false, queueArgs);
                    channel.BasicQos(0, prefetchCount, false);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queueName, noAck, consumer);

                    while (!stopConsuming) {
                        try {
                            BasicDeliverEventArgs basicDeliverEventArgs;
                            var messageIsAvailable = consumer.Queue.Dequeue(timeout, out basicDeliverEventArgs);

                            if (!messageIsAvailable) continue;
                            var payload = basicDeliverEventArgs.Body;

                            var message = Encoding.UTF8.GetString(payload);
                            OnMessageReceived(new MessageReceivedEventArgs {
                                Message = message,
                                EventArgs = basicDeliverEventArgs
                            });

                            if (implicitAck && !noAck) channel.BasicAck(basicDeliverEventArgs.DeliveryTag, false);
                        }
                        catch (Exception exception) {
                            OnMessageReceived(new MessageReceivedEventArgs {
                                Exception = new AMQPConsumerProcessingException(exception)
                            });
                            if (!catchAllExceptions) Stop();
                        }
                    }
                }
            }
            catch (Exception exception) {
                OnMessageReceived(new MessageReceivedEventArgs {
                    Exception = new AMQPConsumerInitialisationException(exception)
                });
            }
        }
    }
}