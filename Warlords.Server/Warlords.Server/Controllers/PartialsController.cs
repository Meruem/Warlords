using System.Web.Mvc;

namespace Warlords.Server.Controllers
{
    public class PartialsController : Controller
    {
        //
        // GET: /Partials/

        public ActionResult Index(string id)
        {
            return PartialView(id);
        }
    }
}
