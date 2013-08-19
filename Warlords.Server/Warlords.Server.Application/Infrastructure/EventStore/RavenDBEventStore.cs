using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Warlords.Server.ApplicationF;
using Warlords.Server.DomainF.Events;

namespace Warlords.Server.Application.Infrastructure.EventStore
{
    public class RavenDBEventStore : IEventStore
    {
        private readonly IDocumentStore _store;

        public RavenDBEventStore(IDocumentStore store)
        {
            _store = store;
        }

        public class RavenEvent
        {
            public string AggregateType { get; set; }
            public Guid AggregateId { get; set; }
            public Event Event { get; set; }
        }

        public IEnumerable<Event> SaveEvents(string aggregateType, Guid aggregateId, int expectedMaxVersion, IEnumerable<Event> events)
        {
            using (var session = _store.OpenSession())
            {
                var saveEvents = events as IList<Event> ?? events.ToList();
                foreach (var @event in saveEvents)
                {
                    session.Store(new RavenEvent
                    {
                        AggregateType = aggregateType,
                        AggregateId = aggregateId,
                        Event = @event
                    });

                    session.SaveChanges();
                }

                return saveEvents;
            }
        }

        public IEnumerable<Event> GetEventsForAggregate(string aggregateType, Guid aggregateId)
        {
            using (var session = _store.OpenSession())
            {
                var ravenEventList = session.Query<RavenEvent>().Where(e => e.AggregateType == aggregateType && e.AggregateId == aggregateId).Select(e => e.Event).ToList();
                return ravenEventList;
            }
        }
    }
}
