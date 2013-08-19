using System;
using log4net;
using Warlords.Server.DomainF.AggregateRoot;

namespace Warlords.Server.Application.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        private readonly IEventStore _storage;

// ReSharper disable StaticFieldInGenericType
        private static readonly ILog _log = LogManager.GetLogger(typeof (Repository<T>));
// ReSharper restore StaticFieldInGenericType

        public Repository(IEventStore storage)
        {
            _storage = storage;
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            var aggregateLogInfo = aggregate.ToString();

            _log.Debug(string.Format("Starting saving events for aggregate {0}", aggregateLogInfo));
            _storage.SaveEvents(typeof (T).Name, aggregate.Id, aggregate.GetUncommittedChanges, expectedVersion);
            aggregate.MarkChangesAsCommitted();
            _log.Debug(string.Format("Saving  events ended for aggregate {0}", aggregateLogInfo));
        }

        public T GetById(Guid id)
        {
            var obj = Activator.CreateInstance<T>();
            var events = _storage.GetEventsForAggregate(typeof (T).Name, id);
            obj.LoadsFromHistory(events);
            return obj;
        }
    }
}