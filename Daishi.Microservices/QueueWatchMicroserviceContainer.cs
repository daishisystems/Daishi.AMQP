#region Includes

using System.ServiceProcess;

#endregion

namespace Daishi.Microservices {
    partial class QueueWatchMicroserviceContainer : ServiceBase {
        public QueueWatchMicroserviceContainer() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            // TODO: Add code here to start your service.
        }

        protected override void OnStop() {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}