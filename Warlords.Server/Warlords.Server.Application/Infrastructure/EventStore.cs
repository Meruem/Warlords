using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Warlords.Server.Domain.Infrastructure;

namespace Warlords.Server.Application.Infrastructure
{
    public class EventStore : IEventStore
    {
        private class AggregateStore : Dictionary<Guid, List<Event>>
        {
        }

        private readonly IHubService _publisher;

        public EventStore(IHubService publisher)
        {
            _publisher = publisher;
        }

        private readonly Dictionary<Type, AggregateStore> _current = new Dictionary<Type, AggregateStore>();

        public void SaveEvents<TAggregate>(Guid aggregateId, IEnumerable<Event> events, int expectedMaxVersion)
        {
            var aggregateType = typeof (TAggregate);
            List<Event> storedEvents;
            if (!_current.ContainsKey(aggregateType))
            {
                _current[aggregateType] = new AggregateStore();
            }

            var aggregateStore = _current[aggregateType];

            if (!aggregateStore.ContainsKey(aggregateId))
            {
                storedEvents = new List<Event>();
                aggregateStore[aggregateId] = storedEvents;
            }
            else
            {
                storedEvents = aggregateStore[aggregateId];
                CheckForConcurrencyConflict(aggregateId, expectedMaxVersion, storedEvents, aggregateType);
            }
            var i = expectedMaxVersion;
            foreach (var @event in events)
            {
                i++;
                @event.Version = i;
                storedEvents.Add(@event);
                _publisher.Publish(@event);
            }
        }

        private void CheckForConcurrencyConflict(Guid aggregateId, int expectedMaxVersion, List<Event> eventDescriptors,
                                                             Type aggregateType)
        {
            Contract.Requires(eventDescriptors != null);
            var eventCount = eventDescriptors.Count;
            if (eventCount > 0)
            {
                var actualMaxVersion = eventDescriptors[eventCount - 1].Version;
                if (actualMaxVersion != expectedMaxVersion && expectedMaxVersion != -1)
                {
                    throw new ConcurrencyException
                        {
                            AggregateId = aggregateId,
                            AggregateType = aggregateType,
                            ActualMaxVersion = actualMaxVersion,
                            ExpectedMaxVersion = expectedMaxVersion
                        };
                }
            }
        }

        public List<Event> GetEventsForAggregate<TAggregate>(Guid aggregateId)
        {
            var aggregateType = typeof (TAggregate);
            if (!_current.ContainsKey(aggregateType))
            {
                throw new AggregateNotFoundException { AggregateId = aggregateId, AggregateType = aggregateType };
            }

            var store = _current[aggregateType];

            if (!store.ContainsKey(aggregateId))
            {
                throw new AggregateNotFoundException { AggregateId = aggregateId, AggregateType = aggregateType};
            }

            return store[aggregateId].ToList();
        }

        public IEnumerable<Guid> GetAllIdsForAggregate<TAggregate>()
        {
            var aggregateType = typeof (TAggregate);
            if (!_current.ContainsKey(aggregateType))
            {
                return new Collection<Guid>();
            }

            return _current[aggregateType].Keys;
        }
    }

    public class AggregateNotFoundException : Exception
    {
        public Guid AggregateId { get; set; }
        public Type AggregateType { get; set; }
    }

    public class ConcurrencyException : Exception
    {
        public Guid AggregateId { get; set; }
        public Type AggregateType { get; set; }
        public int ActualMaxVersion { get; set; }
        public int ExpectedMaxVersion { get; set; }
    }
}