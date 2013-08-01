using System.Diagnostics.Contracts;
using System.Linq;
using Warlords.Server.Infrastructure;
using Warlords.Server.Messages.In;
using Warlords.Server.Models.Lobby;

namespace Warlords.Server.MessageHandlers
{
    public class CreateGameHandler : IHandles<CreateGameMessage>
    {
        private readonly IRepository<Lobby> _lobbyRepository;

        public CreateGameHandler(IRepository<Lobby> lobbyRepository)
        {
            _lobbyRepository = lobbyRepository;
        }

        public void Handle(CreateGameMessage message)
        {
            var lobbyId = _lobbyRepository.GetAllIds().FirstOrDefault();
            Contract.Assert(lobbyId != null, "No lobby exists");

            var lobby = _lobbyRepository.GetById(lobbyId);
            var expectedVersion = lobby.Version;

            var player = lobby.GetPlayerByName(message.PlayerName);
            lobby.CreateGame(player);

            _lobbyRepository.Save(lobby, expectedVersion);
        }
    }
}