using System;
using System.Diagnostics.Contracts;
using Warlords.Server.Application.Commands;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.Common.Aspects;
using Warlords.Server.DomainF;

namespace Warlords.Server.Application.CommandHandlers
{
    public class CreateGameHandler : IHandles<CreateGameMessage>
    {
        private readonly IRepository<Lobby> _lobbyRepository;

        public CreateGameHandler(IRepository<Lobby> lobbyRepository)
        {
            _lobbyRepository = lobbyRepository;
        }

        [Log]
        public void Handle(CreateGameMessage message)
        {
            Guid lobbyId = Guid.NewGuid();
            Contract.Assert(lobbyId != null, "No lobby exists");

            var lobby = _lobbyRepository.GetById(lobbyId);
            var expectedVersion = lobby.Version;

            //lobby.CreateGame(message.UserName);

            _lobbyRepository.Save(lobby, expectedVersion);
        }
    }
}