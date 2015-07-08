#region Includes

using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Daishi.AMQP;

#endregion

namespace Daishi.Microservices.Web {
    public class WebApiApplication : HttpApplication {
        private SimpleMathMicroservice _simpleMathMicroservice;

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            # region Microservice Init

            _simpleMathMicroservice = new SimpleMathMicroservice();
            _simpleMathMicroservice.Init();

            #endregion

            #region RabbitMQAdapter Init

            RabbitMQAdapter.Instance.Init("localhost", 5672, "guest", "guest", 100);
            RabbitMQAdapter.Instance.Connect();

            #endregion
        }

        protected void Application_End() {
            if (_simpleMathMicroservice != null) {
                _simpleMathMicroservice.Shutdown();
            }

            if (RabbitMQAdapter.Instance != null && RabbitMQAdapter.Instance.IsConnected) {
                RabbitMQAdapter.Instance.Disconnect();
            }
        }
    }
}