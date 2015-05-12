#region Includes

using System;
using RabbitMQ.Client.Events;

#endregion

namespace Daishi.AMQP {
    public class MessageReceivedEventArgs : EventArgs {
        public string Message { get; set; }
        public BasicDeliverEventArgs EventArgs { get; set; }
        public Exception Exception { get; set; }
    }
}