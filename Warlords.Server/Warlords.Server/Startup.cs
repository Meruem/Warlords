using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;

namespace Warlords.Server
{
    public class Startup
    {
        // The name *MUST* be Configuration
        public void Configuration(IAppBuilder app)
        {
            app.MapHubs();
        }
    }
}