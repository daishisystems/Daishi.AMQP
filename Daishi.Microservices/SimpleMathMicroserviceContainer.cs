#region Includes

using System.ServiceProcess;

#endregion

namespace Daishi.Microservices {
    partial class SimpleMathMicroserviceContainer : ServiceBase {
        private SimpleMathMicroservice _simpleMathMicroservice;

        public SimpleMathMicroserviceContainer() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            _simpleMathMicroservice = new SimpleMathMicroservice();
            _simpleMathMicroservice.Init();
        }

        protected override void OnStop() {
            _simpleMathMicroservice.Shutdown();
        }
    }
}