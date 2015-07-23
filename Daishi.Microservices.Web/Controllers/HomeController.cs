#region Includes

using System.Web.Mvc;

#endregion

namespace Daishi.Microservices.Web.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}