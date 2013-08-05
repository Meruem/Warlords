using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Warlords.Server.Domain.Infrastructure;

namespace Warlords.Server.Application.Infrastructure.EventStore
{
    public class RavenDBEventStore : IEventStore
    {
        private readonly IDocumentStore _store;
        private readonly IHubService _publisher;

        public RavenDBEventStore(IDocumentStore store, IHubService publisher)
        {
            _store = store;
            _publisher = publisher;
        }

        public class RavenEvent
        {
            public string AggregateType { get; set; }
            public Guid AggregateId { get; set; }
            public Event Event { get; set; }
        }

        public void SaveEvents<TAggregate>(Guid aggregateId, IEnumerable<Event> events, int expectedMaxVersion)
        {
            using (var session = _store.OpenSession())
            {
                var aggregateType = typeof(TAggregate);

                var i = expectedMaxVersion; 
                foreach (var @event in events)
                {
                    i++;
                    @event.Version = i;

                    session.Store(new RavenEvent
                    {
                        AggregateType = aggregateType.Name,
                        AggregateId = aggregateId,
                        Event = @event
                    });

                    session.SaveChanges();
                    _publisher.Publish(@event);
                }
            }
        }

        public List<Event> GetEventsForAggregate<TAggregate>(Guid aggregateId)
        {
            using (var session = _store.OpenSession())
            {
                var aggregateTypeName = typeof(TAggregate).Name;
                var ravenEventList = session.Query<RavenEvent>().Where(e => e.AggregateType == aggregateTypeName && e.AggregateId == aggregateId).Select(e => e.Event).ToList();
                return ravenEventList;
            }
        }

        //public IEnumerable<LobbyGuid> GetAllIdsForAggregate<TAggregate>()
        //{
        //    using (var session = _store.OpenSession())
        //    {
        //        return session.Query<RavenEvent>().Select(e => e.AggregateId).Distinct().ToList();
        //    }
        //}
    }
}
