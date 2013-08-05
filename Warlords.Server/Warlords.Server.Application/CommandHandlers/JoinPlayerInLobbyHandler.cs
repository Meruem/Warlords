using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Raven.Client;
using Warlords.Server.Application.Commands;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.Application.ViewModels;
using Warlords.Server.Common.Aspects;
using Warlords.Server.Domain.Models.Lobby;

namespace Warlords.Server.Application.CommandHandlers
{
    public class JoinPlayerInLobbyHandler : IHandles<JoinPlayerInLobbyMessage>
    {
        private readonly IRepository<Lobby> _lobbyRepository;
        private readonly IDocumentStore _store;

        public JoinPlayerInLobbyHandler(IRepository<Lobby> lobbyRepository, IDocumentStore store)
        {
            _lobbyRepository = lobbyRepository;
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

            var lobby = _lobbyRepository.GetById(lobbyId);
            Contract.Assert(lobby != null);
            var expectedVersion = lobby.Version;

            if (lobby.IsPlayerJoined(message.UserName))
            {
                // player is already joined, just change the connection Id under which is his account recognized
                // currently not working and probably not required at all
                //lobby.RefreshPlayerConnectionId(message.UserName, message.ConnectionId);
            }
            else
            {
                lobby.JoinPlayer(message.UserName, message.ConnectionId);
                _lobbyRepository.Save(lobby, expectedVersion);
            }
        }
    }
}