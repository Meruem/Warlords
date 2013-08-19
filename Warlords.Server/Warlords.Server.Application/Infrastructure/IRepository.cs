using System;
using Warlords.Server.DomainF.AggregateRoot;

namespace Warlords.Server.Application.Infrastructure
{
    public interface IRepository<T> where T : AggregateRoot
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(Guid id);
        //IEnumerable<LobbyGuid> GetAllIds();
    }
}