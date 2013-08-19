using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Warlords.Server.Application.Infrastructure.Exceptions;
using Warlords.Server.ApplicationF;
using Warlords.Server.DomainF.Events;

namespace Warlords.Server.Application.Infrastructure.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private class AggregateStore : Dictionary<Guid, List<Event>>
        {
        }

        private readonly Dictionary<string, AggregateStore> _current = new Dictionary<string, AggregateStore>();

        public IEnumerable<Event> SaveEvents(string aggregateType, Guid aggregateId, int expectedMaxVersion, IEnumerable<Event> events)
        {
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
            var saveEvents = events as IList<Event> ?? events.ToList();
            foreach (var @event in saveEvents)
            {
                storedEvents.Add(@event);
            }

            return saveEvents;
        }

        private void CheckForConcurrencyConflict(Guid aggregateId, int expectedMaxVersion, List<Event> eventDescriptors,
                                                             string aggregateType)
        {
            //Contract.Requires(eventDescriptors != null);
            //var eventCount = eventDescriptors.Count;
            //if (eventCount > 0)
            //{
            //    var actualMaxVersion = eventDescriptors[eventCount - 1].Version;
            //    if (actualMaxVersion != expectedMaxVersion && expectedMaxVersion != -1)
            //    {
            //        throw new ConcurrencyException
            //            {
            //                AggregateId = aggregateId,
            //                AggregateType = aggregateType,
            //                ActualMaxVersion = actualMaxVersion,
            //                ExpectedMaxVersion = expectedMaxVersion
            //            };
            //    }
            //}
        }

        public IEnumerable<Event> GetEventsForAggregate(string aggregateType, Guid aggregateId)
        {
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

        public IEnumerable<Guid> GetAllIdsForAggregate(string aggregateType)
        {
            if (!_current.ContainsKey(aggregateType))
            {
                return new Collection<Guid>();
            }

            return _current[aggregateType].Keys;
        }
    }
}