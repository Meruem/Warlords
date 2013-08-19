using System;
using Warlords.Server.Application.Commands;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.DomainF;

namespace Warlords.Server.Application.CommandHandlers
{
    public class CreateLobbyHandler : IHandles<CreateLobbyMessage>
    {
        //private readonly IRepository<Lobby> _lobbyRepository;

        //public CreateLobbyHandler(IRepository<Lobby> lobbyRepository)
        //{
        //    _lobbyRepository = lobbyRepository;
        //}

        //public void Handle(CreateLobbyMessage message)
        //{
        //    var newLobby = new Lobby(Guid.NewGuid());
        //    _lobbyRepository.Save(newLobby, -1);
        //}

        private readonly IEventStore _eventStore;

        public CreateLobbyHandler(IEventStore store)
        {
            _eventStore = store;
        }

        public void Handle(CreateLobbyMessage message)
        {
            var newId = Guid.NewGuid();
            var events = Lobby.create(newId);
            _eventStore.SaveEvents("Lobby", newId, events, -1);
        }
    }
}
