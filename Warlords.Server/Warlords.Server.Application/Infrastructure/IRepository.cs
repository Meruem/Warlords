using System;
using System.Collections.Generic;
using Warlords.Server.Domain.Infrastructure;

namespace Warlords.Server.Application.Infrastructure
{
    public interface IRepository<T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(Guid id);
        IEnumerable<Guid> GetAllIds();
    }
}