#region Includes

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

#endregion

namespace Daishi.AMQP {
    public class QueueWatch : IDisposable {
        private readonly AMQPQueueMetricsManager _amqpQueueMetricsManager;
        private readonly AMQPQueueMetricsAnalyser _amqpQueueMetricsAnalyser;
        private readonly AMQPConsumerNotifier _amqpConsumerNotifier;
        private readonly int _interval;

        private Timer _timer;
        private volatile bool _stop;
        private Dictionary<string, AMQPQueueMetric> _currentAMQPQueueMetrics;
        private Dictionary<string, AMQPQueueMetric> _previousAMQPQueueMetrics;
        private ConcurrentBag<AMQPQueueMetric> _busyQueues;
        private ConcurrentBag<AMQPQueueMetric> _quietQueues;

        public QueueWatch(AMQPQueueMetricsManager amqpQueueMetricsManager,
            AMQPQueueMetricsAnalyser amqpQueueMetricsAnalyser, AMQPConsumerNotifier amqpConsumerNotifier,
            int interval) {
            _amqpQueueMetricsManager = amqpQueueMetricsManager;
            _amqpQueueMetricsAnalyser = amqpQueueMetricsAnalyser;
            _amqpConsumerNotifier = amqpConsumerNotifier;
            _interval = interval;
        }

        public event EventHandler<AMQPQueueMetricsAnalysedEventArgs> AMQPQueueMetricsAnalysed;

        public void Start() {
            _stop = false;
            _timer = new Timer(o => {
                _currentAMQPQueueMetrics = _amqpQueueMetricsManager.GetAMQPQueueMetrics();
                Monitor();
                if (!_stop)
                    _timer.Change(_interval, Timeout.Infinite);
            }, null, _interval, Timeout.Infinite);
        }

        public void StartAsync() {
            _timer = new Timer(async o => {
                _currentAMQPQueueMetrics = await _amqpQueueMetricsManager.GetAMQPQueueMetricsAsync();
                Monitor();
                if (!_stop)
                    _timer.Change(_interval, Timeout.Infinite);
            }, null, _interval, Timeout.Infinite);
        }

        public void Stop() {
            _stop = true;
            if (_timer != null)
                _timer.Dispose();
        }

        void IDisposable.Dispose() {
            Stop();
        }

        private void Monitor() {
            if (_previousAMQPQueueMetrics == null) {
                _previousAMQPQueueMetrics = _currentAMQPQueueMetrics;
                return;
            }
            _amqpQueueMetricsAnalyser.Analyse(_currentAMQPQueueMetrics, _previousAMQPQueueMetrics,
                out _busyQueues, out _quietQueues);

            _amqpConsumerNotifier.Notify(_busyQueues, _quietQueues);
            _previousAMQPQueueMetrics = _currentAMQPQueueMetrics;

            OnAMQPQueueMetricsAnalysed(new AMQPQueueMetricsAnalysedEventArgs {
                BusyQueues = new List<AMQPQueueMetric>(_busyQueues),
                QuietQueues = new List<AMQPQueueMetric>(_quietQueues)
            });
        }

        private void OnAMQPQueueMetricsAnalysed(AMQPQueueMetricsAnalysedEventArgs e) {
            var handler = AMQPQueueMetricsAnalysed;
            if (handler != null) handler(this, e);
        }
    }
}