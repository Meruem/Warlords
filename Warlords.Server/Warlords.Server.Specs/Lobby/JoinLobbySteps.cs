using System;
using log4net.Filter;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Ninject;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Principal;
using TechTalk.SpecFlow;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.App_Start;
using Warlords.Server.Application.Messages.In;
using Warlords.Server.Hubs;

namespace Warlords.Server.Specs.Lobby
{
    [Binding]
    public class JoinLobbySteps
    {
        private readonly IKernel _kernel = NinjectWebCommon.CreateKernel();

        public class MockAll : DynamicObject
        {
            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                List<string> methods = null;

                if (ScenarioContext.Current.ContainsKey("CalledMethods"))
                {
                    methods = ScenarioContext.Current["CalledMethods"] as List<string>;
                }

                methods = methods ?? new List<string>();

                methods.Add(binder.Name);

                ScenarioContext.Current["CalledMethods"] = methods;

                result = null;
                return true;
            }
        }

        public class TestClientSender : IClientSender
        {
            public MockAll Mocker { get; set; }

            public TestClientSender()
            {
                Mocker = new MockAll();;
            }

            public void SendToAllClients(string hubName, Action<dynamic> action)
            {
                action(Mocker);
            }

            public void SendToClient(string connectionId, string hubName, Action<dynamic> action)
            {
                throw new NotImplementedException();
            }
        }

        [Given(@"The server is running")]
        public void GivenTheServerIsRunning()
        {
            var messageHub = _kernel.Get<MessageHub>();

            var mockUser = new Mock<IPrincipal>();
            mockUser.Setup(foo => foo.Identity.Name).Returns("Me");

            var mockRequest = new Mock<IRequest>();
            mockRequest.Setup(r => r.User).Returns(mockUser.Object);

            messageHub.Context = new HubCallerContext(mockRequest.Object, "newId");
            var context = new HubConnectionContext {All = new MockAll()};
            messageHub.Clients = context;

            ScenarioContext.Current["messageHub"] = messageHub;
        }
        
        [When(@"I join the lobby")]
        public void WhenIJoinTheLobby()
        {
            _kernel.Rebind<IClientSender>().To<TestClientSender>();

            var messageHub = ScenarioContext.Current["messageHub"] as MessageHub;
            var message = new JoinPlayerInLobbyMessage();
            Assert.IsNotNull(messageHub);
            messageHub.HandleMessage("JoinPlayerInLobbyMessage", JsonConvert.SerializeObject(message));
        }
        
        [Then(@"I'm on a list of players joined in lobby")]
        public void ThenImonAListOfPlayersJoinedInLobby()
        {
            //var lobby = _kernel.Get<Domain.Models.Lobby.Lobby>();
            //Assert.IsTrue(lobby.Players.Any(p => p == "Me"));
        }
        
        [Then(@"Everybody receives message notifying about me joining")]
        public void ThenEverybodyReceivesMessageNotifyingAboutMeJoining()
        {
            var methods = ScenarioContext.Current["CalledMethods"] as List<string>;
            Assert.IsNotNull(methods);
            Assert.IsTrue(methods.Any(m => m == "playerJoined"));
        }

        [Given(@"I'm already joined in lobby")]
        public void GivenImAlreadyJoinedInLobby()
        {
            var lobby = _kernel.Get<Domain.Models.Lobby.Lobby>();
            lobby.Players.Add("Me");
        }

        [Then(@"My connection id is refreshed to new value")]
        public void ThenMyConnectionIdIsRefreshedToNewValue()
        {
            var lobby = _kernel.Get<Domain.Models.Lobby.Lobby>();
            var me = lobby.Players.FirstOrDefault(p => p == "Me");

            Assert.IsNotNull(me);
            //Assert.IsTrue(me.GetConnectionId() == "newId");
        }
    }
}
