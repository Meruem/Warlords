﻿using System.Diagnostics.Contracts;
using System.Linq;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.Application.Messages.In;
using Warlords.Server.Common.Aspects;
using Warlords.Server.Domain.Models.Lobby;

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
            var lobbyId = _lobbyRepository.GetAllIds().FirstOrDefault();
            Contract.Assert(lobbyId != null, "No lobby exists");

            var lobby = _lobbyRepository.GetById(lobbyId);
            var expectedVersion = lobby.Version;

            lobby.CreateGame(message.UserName);

            _lobbyRepository.Save(lobby, expectedVersion);
        }
    }
}