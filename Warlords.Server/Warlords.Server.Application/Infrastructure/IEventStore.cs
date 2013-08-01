using System;
using System.Collections.Generic;
using Warlords.Server.Domain.Infrastructure;

namespace Warlords.Server.Application.Infrastructure
{
    public interface IEventStore
    {
        void SaveEvents<TAggregate>(Guid aggregateId, IEnumerable<Event> events, int expectedMaxVersion);
        List<Event> GetEventsForAggregate<TAggregate>(Guid aggregateId);
        IEnumerable<Guid> GetAllIdsForAggregate<TAggregate>();
    }
}