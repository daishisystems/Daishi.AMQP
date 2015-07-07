#region Includes

using Daishi.Microservices.TestHarness;
using Microsoft.Owin;
using Owin;

#endregion

[assembly: OwinStartup(typeof (Startup))]

namespace Daishi.Microservices.TestHarness {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}