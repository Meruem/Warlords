using System.Web.Mvc;

namespace Warlords.Server.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Main/

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
