using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.Application.Messages.In;
using Warlords.Server.Common.Aspects;
using Warlords.Server.Domain.Models.Lobby;

namespace Warlords.Server.Application.CommandHandlers
{
    public class JoinPlayerInLobbyHandler : IHandles<JoinPlayerInLobbyMessage>
    {
        private readonly IRepository<Lobby> _lobbyRepository;

        public JoinPlayerInLobbyHandler(IRepository<Lobby> lobbyRepository)
        {
            _lobbyRepository = lobbyRepository;
        }

        [Log]
        public void Handle(JoinPlayerInLobbyMessage message)
        {
            var lobbyId = _lobbyRepository.GetAllIds().FirstOrDefault();

            // hack :(
            //Contract.Assert(lobbyId != null, "No lobby exists");

            if (lobbyId == default(Guid)) 
            {
                var newLobby = new Lobby(Guid.NewGuid());
                _lobbyRepository.Save(newLobby, -1);
                lobbyId = newLobby.Id;
            }
            // hack end

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