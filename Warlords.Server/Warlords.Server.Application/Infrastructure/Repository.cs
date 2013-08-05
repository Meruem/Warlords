using System;
using System.Collections.Generic;
using Warlords.Server.Domain.Infrastructure;
using log4net;

namespace Warlords.Server.Application.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new()
    {
        private readonly IEventStore _storage;

// ReSharper disable StaticFieldInGenericType
        private static readonly ILog _log = LogManager.GetLogger(typeof(Repository<T>));
// ReSharper restore StaticFieldInGenericType

        public Repository(IEventStore storage)
        {
            _storage = storage;
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            var aggregateLogInfo = aggregate.ToString();

            _log.Debug(string.Format("Starting saving events for aggregate {0}", aggregateLogInfo));
            _storage.SaveEvents<T>(aggregate.Id, aggregate.GetUncommittedChanges(), expectedVersion);
            _log.Debug(string.Format("Saving  events ended for aggregate {0}", aggregateLogInfo));
        }

        public T GetById(Guid id)
        {
            var obj = new T();
            obj.Id = id;
            var e = _storage.GetEventsForAggregate<T>(id);
            obj.LoadsFromHistory(e);
            return obj;
        }

        //public IEnumerable<LobbyGuid> GetAllIds()
        //{
        //    return _storage.GetAllIdsForAggregate<T>();
        //}
    }
}