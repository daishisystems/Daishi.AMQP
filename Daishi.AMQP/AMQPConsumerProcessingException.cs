#region Includes

using System;

#endregion

namespace Daishi.AMQP {
    [Serializable]
    public class AMQPConsumerProcessingException : Exception {
        public AMQPConsumerProcessingException(Exception innerException) :
            base("An Exception occured while consuming the Queue.", innerException) {}
    }
}