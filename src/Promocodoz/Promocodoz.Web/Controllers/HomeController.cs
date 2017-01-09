using System.Web.Mvc;

namespace Promocodoz.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Docs()
        {
            return View();
        }
    }
}