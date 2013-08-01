using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warlords.Server.Application.Commands;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.Domain.Models.Lobby;

namespace Warlords.Server.Application.CommandHandlers
{
    public class CreateLobbyHandler : IHandles<CreateLobbyMessage>
    {
        private readonly IRepository<Lobby> _lobbyRepository;

        public CreateLobbyHandler(IRepository<Lobby> lobbyRepository)
        {
            _lobbyRepository = lobbyRepository;
        }

        public void Handle(CreateLobbyMessage message)
        {
            var newLobby = new Lobby(Guid.NewGuid());
            _lobbyRepository.Save(newLobby, -1);

        }
    }
}
