﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Warlords.Server.Application.Infrastructure;

namespace Warlords.Server.App_Start
{
    public class StartCommands
    {
        public static void PublishStartCommands()
        {
            var hubService = GlobalHost.DependencyResolver.GetService(typeof (IHubService)) as IHubService;
            
            
            //hubService.Send();
        }
    }
}