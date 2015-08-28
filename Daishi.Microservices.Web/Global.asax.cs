#region Includes

using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Daishi.AMQP;

#endregion

namespace Daishi.Microservices.Web {
    public class WebApiApplication : HttpApplication {
        private readonly List<SimpleMathMicroservice> _simpleMathMicroservices = new List<SimpleMathMicroservice>();

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            # region Microservice Init

            for (var i = 0; i < 10; i++) {
                var simpleMathMicroservice = new SimpleMathMicroservice();
                _simpleMathMicroservices.Add(simpleMathMicroservice);

                simpleMathMicroservice.Init();
            }

            #endregion

            #region RabbitMQAdapter Init

            RabbitMQAdapter.Instance.Init("localhost", 5672, "guest", "guest", 100);
            RabbitMQAdapter.Instance.Connect();

            #endregion
        }

        protected void Application_End() {
            foreach (var simpleMathMicroservice in _simpleMathMicroservices) {
                simpleMathMicroservice.Shutdown();
            }

            if (RabbitMQAdapter.Instance != null && RabbitMQAdapter.Instance.IsConnected) {
                RabbitMQAdapter.Instance.Disconnect();
            }
        }
    }
}