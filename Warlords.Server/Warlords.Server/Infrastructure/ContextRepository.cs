using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;

namespace Warlords.Server.Infrastructure
{
    public class ContextRepository : IContextRepository
    {
        private readonly Dictionary<Guid, IHubConnectionContext> _repository = new Dictionary<Guid, IHubConnectionContext>();

        public void Add(Guid id, IHubConnectionContext context)
        {
            _repository[id] = context;
        }

        public IHubConnectionContext GetContextById(Guid id)
        {
            if (!_repository.ContainsKey(id))
            {
                throw new InvalidOperationException(string.Format("No context for id {0} is stored", id));
            }

            return _repository[id];
        }
    }
}