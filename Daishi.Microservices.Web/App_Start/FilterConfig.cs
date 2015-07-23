#region Includes

using System.Web.Mvc;

#endregion

namespace Daishi.Microservices.Web {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}