using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Warlords.Server.Application.Commands;
using Warlords.Server.Domain.Events;
using Warlords.Server.Specs.Infrastructure;
using models = Warlords.Server.Domain.Models;

namespace Warlords.Server.Specs.Lobby
{
    [Binding]
    public class JoinLobbySteps
    {
        [Given(@"The server is running")]
        public void GivenTheServerIsRunning()
        {
            TestInit.InitServer();
        }
        
        [When(@"I join the lobby")]
        public void WhenIJoinTheLobby()
        {
            var messageHub = ScenarioContext.Current.MessageHub();
            var message = new JoinPlayerInLobbyMessage();
            messageHub.HandleMessage("JoinPlayerInLobbyMessage", JsonConvert.SerializeObject(message));
        }
        
        [Then(@"I'm on a list of players joined in lobby")]
        public void ThenImonAListOfPlayersJoinedInLobby()
        {
            var eventStore = ScenarioContext.Current.EventStore();
            var lobbyId = eventStore.GetAllIdsForAggregate<models.Lobby.Lobby>().First();
            var lobbyEvents = eventStore.GetEventsForAggregate<models.Lobby.Lobby>(lobbyId);

            Assert.IsTrue(lobbyEvents.Cast<PlayerJoinedEvent>().Any(pje => pje.UserName == "Me" && pje.ConnectionId == "newId"));
        }
        
        [Then(@"Everybody receives message notifying about me joining")]
        public void ThenEverybodyReceivesMessageNotifyingAboutMeJoining()
        {
            Assert.IsTrue(ClientSenderHelper.CheckMethodWasCalled("playerJoined"));
        }
    }
}
