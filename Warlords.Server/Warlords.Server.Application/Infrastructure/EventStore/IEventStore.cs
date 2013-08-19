using System;
using System.Collections.Generic;
using Warlords.Server.DomainF.Events;

namespace Warlords.Server.Application.Infrastructure
{
    public interface IEventStore
    {
        void SaveEvents(string aggregateType, Guid aggregateId, IEnumerable<Event> events, int expectedMaxVersion);
        List<Event> GetEventsForAggregate(string aggregateType, Guid aggregateId);
    }
}