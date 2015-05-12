#region Includes

using System;
using System.Collections.Generic;

#endregion

namespace Daishi.AMQP {
    public class AMQPQueueMetricsAnalysedEventArgs : EventArgs {
        public List<AMQPQueueMetric> BusyQueues { get; set; }
        public List<AMQPQueueMetric> QuietQueues { get; set; }
    }
}