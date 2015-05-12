#region Includes

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace Daishi.AMQP {
    public abstract class AMQPQueueMetricsManager {
        protected readonly bool isSecure;
        protected readonly string hostName;
        protected readonly int port;
        protected readonly string userName;
        protected readonly string password;

        protected AMQPQueueMetricsManager(bool isSecure, string hostName,
            int port, string userName, string password) {
            this.isSecure = isSecure;
            this.hostName = hostName;
            this.port = port;
            this.userName = userName;
            this.password = password;
        }

        public abstract Dictionary<string, AMQPQueueMetric> GetAMQPQueueMetrics();

        public abstract Task<Dictionary<string, AMQPQueueMetric>> GetAMQPQueueMetricsAsync();
    }
}