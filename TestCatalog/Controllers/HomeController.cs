using System.Web.Mvc;

namespace TestCatalog.Controllers
{
    public class HomeController : Controller
    {
        protected ActionResult RequestView(string viewName)
        {
            return RequestView(viewName, null, null);
        }

        protected ActionResult RequestView(string viewName, object model)
        {
            return RequestView(viewName, null, model);
        }

        protected ActionResult RequestView(string viewName, string masterName, object model)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(viewName, model);
            }

            return View(viewName, masterName, model);
        }

        public ActionResult Index()
        {
            return RequestView("Index");
        }
    }
}