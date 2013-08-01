using System;
using System.Collections.Generic;

namespace Warlords.Server.Infrastructure
{
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(Guid id);
        IEnumerable<Guid> GetAllIds();
    }
}