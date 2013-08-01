using System.Web.Mvc;
using System.Web.Security;

namespace Warlords.Server.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(string userName, string password)
        {
            var provider = Membership.Provider;
            if (provider.ValidateUser(userName, password))
            {
                FormsAuthentication.SetAuthCookie(userName, false);

                return RedirectToAction("Index", "Main");
            }

            return View();
        }

    }
}
