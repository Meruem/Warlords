using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using Ninject;
using TechTalk.SpecFlow;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.App_Start;
using Warlords.Server.Hubs;

namespace Warlords.Server.Specs.Infrastructure
{
    public static class TestInit
    {
        private static readonly IKernel _kernel = NinjectWebCommon.CreateKernel();

        public static void InitServer()
        {
            _kernel.Rebind<IClientSender>().To<TestClientSender>();
            _kernel.Rebind<IEventScheduler>().To<SyncEventScheduler>();

            var messageHub = _kernel.Get<MessageHub>();

            var mockUser = new Mock<IPrincipal>();
            mockUser.Setup(foo => foo.Identity.Name).Returns("Me");

            var mockRequest = new Mock<IRequest>();
            mockRequest.Setup(r => r.User).Returns(mockUser.Object);

            messageHub.Context = new HubCallerContext(mockRequest.Object, "newId");
            var context = new HubConnectionContext { All = new MethodCallStoreDynamic() };
            messageHub.Clients = context;

            var eventStore = _kernel.Get<IEventStore>();
            ScenarioContext.Current.SetEventStore(eventStore);

            ScenarioContext.Current.SetMessageHub(messageHub);
        }
    }
}
