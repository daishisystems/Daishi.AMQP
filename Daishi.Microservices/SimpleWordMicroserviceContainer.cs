#region Includes

using System.ServiceProcess;

#endregion

namespace Daishi.Microservices {
    partial class SimpleWordMicroserviceContainer : ServiceBase {
        private SimpleWordMicroservice _simpleWordMicroservice;

        public SimpleWordMicroserviceContainer() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            _simpleWordMicroservice = new SimpleWordMicroservice();
            _simpleWordMicroservice.Init();
        }

        protected override void OnStop() {
            _simpleWordMicroservice.Shutdown();
        }
    }
}