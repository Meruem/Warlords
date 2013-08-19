using Raven.Client;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.DomainF.Events;

namespace Warlords.Server.Application.ViewModels.Handlers
{
    public class PlayerJoinedHandler : IHandles<PlayerJoinedEvent>
    {
        private readonly IDocumentStore _store;

        public PlayerJoinedHandler(IDocumentStore store)
        {
            _store = store;
        }

        public void Handle(PlayerJoinedEvent message)
        {
            using (var session = _store.OpenSession())
            {
                session.Store(new Player { Name = message.UserName});
                session.SaveChanges();
            }
        }
    }
}