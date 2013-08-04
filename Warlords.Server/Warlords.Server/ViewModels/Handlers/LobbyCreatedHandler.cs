using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.Domain.Events;

namespace Warlords.Server.ViewModels.Handlers
{
    public class LobbyCreatedHandler : IHandles<LobbyCreatedEvent>
    {
        private IDocumentStore Store;

        public void Handle(LobbyCreatedEvent message)
        {
            throw new NotImplementedException();
        }
    }
}