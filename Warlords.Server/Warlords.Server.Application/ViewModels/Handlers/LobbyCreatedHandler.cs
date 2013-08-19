using Raven.Abstractions.Data;
using Raven.Client;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.DomainF.Events;

namespace Warlords.Server.Application.ViewModels.Handlers
{
    public class LobbyCreatedHandler : IHandles<LobbyCreatedEvent>
    {
        private readonly IDocumentStore _store;

        public LobbyCreatedHandler(IDocumentStore store)
        {
            _store = store;
        }

        public void Handle(LobbyCreatedEvent message)
        {
            using (var session = _store.OpenSession())
            {
                session.Advanced.DocumentStore.DatabaseCommands.DeleteByIndex("AllPlayersIndex", new IndexQuery());

                session.Store(new CurrentLobby {LobbyId = message.LobbyGuid}, "/CurrentLobby/1");
                session.SaveChanges();
            }
        }
    }
}