using System.Diagnostics.Contracts;
using System.Web.Mvc;

namespace Warlords.Server.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            Contract.Requires(filters != null);

            filters.Add(new HandleErrorAttribute());
        }
    }
}