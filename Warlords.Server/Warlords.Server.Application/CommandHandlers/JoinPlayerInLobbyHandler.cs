using System;
using System.Diagnostics.Contracts;
using Raven.Client;
using Warlords.Server.Application.Commands;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.Application.ViewModels;
using Warlords.Server.Common.Aspects;
using Warlords.Server.DomainF;

namespace Warlords.Server.Application.CommandHandlers
{
    public class JoinPlayerInLobbyHandler : IHandles<JoinPlayerInLobbyMessage>
    {
        private readonly IEventStore _eventStore;
        private readonly IDocumentStore _store;

        public JoinPlayerInLobbyHandler(IEventStore eventStore, IDocumentStore store)
        {
            _eventStore = eventStore;
            _store = store;
        }

        [Log]
        public void Handle(JoinPlayerInLobbyMessage message)
        {
            Guid lobbyId;
            using (var session = _store.OpenSession())
            {
                var currentlobby = session.Load<CurrentLobby>("/CurrentLobby/1");
                Contract.Assert(currentlobby != null, "No lobby exists");
                lobbyId = currentlobby.LobbyId;
            }

            Contract.Assert(lobbyId != null, "No lobby exists");

            var events = _eventStore.GetEventsForAggregate("Lobby", lobbyId);
            var lobby = Lobby.replayLobby(events);

            Contract.Assert(lobby != null);
            //var expectedVersion = lobby.Version;

            //if (lobby.IsPlayerJoined(message.UserName))
            //{
                // player is already joined, just change the connection Id under which is his account recognized
                // currently not working and probably not required at all
                //lobby.RefreshPlayerConnectionId(message.UserName, message.ConnectionId);
            //}
            //else
            //{
                var newEvents = Lobby.joinPlayer(message.UserName, message.ConnectionId, lobby);
                _eventStore.SaveEvents("Lobby", lobbyId, newEvents, -1);
            //}
        }
    }
}