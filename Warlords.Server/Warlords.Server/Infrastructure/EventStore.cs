using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Warlords.Server.Infrastructure
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

        public void SaveEvents<TAggregate>(Guid aggregateId, IEnumerable<Event> events, int expectedVersion)
        {
            Contract.Requires(events != null);

            var aggregateType = typeof (TAggregate);
            List<Event> eventDescriptors;
            if (!_current.ContainsKey(aggregateType))
            {
                _current[aggregateType] = new AggregateStore();
            }

            var aggregateStore = _current[aggregateType];

            if (!aggregateStore.ContainsKey(aggregateId))
            {
                eventDescriptors = new List<Event>();
                aggregateStore[aggregateId] = eventDescriptors;
            }
            else
            {
                eventDescriptors = aggregateStore[aggregateId];
                var eventCount = eventDescriptors.Count;
                if (eventCount > 0)
                {
                    if (eventDescriptors[eventCount - 1].Version != expectedVersion && expectedVersion != -1)
                    {
                        throw new ConcurrencyException();
                    }
                }
            }
            var i = expectedVersion;
            foreach (var @event in events)
            {
                i++;
                @event.Version = i;
                eventDescriptors.Add(@event);
                _publisher.Publish(@event);
            }
        }

        public List<Event> GetEventsForAggregate<TAggregate>(Guid aggregateId)
        {
            var aggregateType = typeof (TAggregate);
            if (!_current.ContainsKey(aggregateType))
            {
                throw new AggregateNotFoundException();
            }

            var store = _current[aggregateType];

            if (!store.ContainsKey(aggregateId))
            {
                throw new AggregateNotFoundException();
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
    }

    public class ConcurrencyException : Exception
    {
    }
}