using System;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics.Contracts;
using System.Linq;
using Warlords.Server.Infrastructure;
using Warlords.Server.Messages.In;
using Warlords.Server.Models.Lobby;

namespace Warlords.Server.MessageHandlers
{
    public class JoinPlayerInLobbyHandler : IHandles<JoinPlayerInLobbyMessage>, ISignalRAwareHandler
    {
       private readonly IRepository<Lobby> _lobbyRepository;

        public IHubConnectionContext Clients { get; set; }

        public JoinPlayerInLobbyHandler(IRepository<Lobby> lobbyRepository)
        {
            _lobbyRepository = lobbyRepository;
        }


        public void Handle(JoinPlayerInLobbyMessage message)
        {
            var lobbyId = _lobbyRepository.GetAllIds().FirstOrDefault();

            // hack :(
            //Contract.Assert(lobbyId != null, "No lobby exists");

            if (lobbyId == default(Guid)) 
            {
                var newLobby = new Lobby();
                _lobbyRepository.Save(newLobby, -1);
            }
            
            var lobby = _lobbyRepository.GetById(lobbyId);
            Contract.Assert(lobby != null);
            var expectedVersion = lobby.Version;

            if (lobby.IsPlayerJoined(message.PlayerName))
            {
                lobby.RefreshPlayerConnectionId(message.PlayerName, message.ConnectionId);
            }
            else
            {
                lobby.JoinPlayer(message.PlayerName, message.ConnectionId);
                _lobbyRepository.Save(lobby, expectedVersion);
            }
        }
    }
}