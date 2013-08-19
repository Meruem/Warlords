using System;

namespace Warlords.Server.Application.Infrastructure.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public Guid AggregateId { get; set; }
        public string AggregateType { get; set; }
    }
}
