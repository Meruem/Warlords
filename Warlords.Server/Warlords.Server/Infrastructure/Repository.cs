using System;
using System.Collections.Generic;

namespace Warlords.Server.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new() 
    {
        private readonly IEventStore _storage;

        public Repository(IEventStore storage)
        {
            _storage = storage;
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            _storage.SaveEvents<T>(aggregate.Id, aggregate.GetUncommittedChanges(), expectedVersion);
        }

        public T GetById(Guid id)
        {
            var obj = new T();
            var e = _storage.GetEventsForAggregate<T>(id);
            obj.LoadsFromHistory(e);
            return obj;
        }

        public IEnumerable<Guid> GetAllIds()
        {
            return _storage.GetAllIdsForAggregate<T>();
        }
    }
}