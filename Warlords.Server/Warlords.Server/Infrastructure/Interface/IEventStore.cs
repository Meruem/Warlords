using System;
using System.Collections.Generic;

namespace Warlords.Server.Infrastructure
{
    public interface IEventStore
    {
        void SaveEvents<TAggregate>(Guid aggregateId, IEnumerable<Event> events, int expectedVersion);
        List<Event> GetEventsForAggregate<TAggregate>(Guid aggregateId);
        IEnumerable<Guid> GetAllIdsForAggregate<TAggregate>();
    }
}