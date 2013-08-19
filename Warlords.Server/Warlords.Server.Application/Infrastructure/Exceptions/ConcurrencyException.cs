using System;

namespace Warlords.Server.Application.Infrastructure.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public Guid AggregateId { get; set; }
        public string AggregateType { get; set; }
        public int ActualMaxVersion { get; set; }
        public int ExpectedMaxVersion { get; set; }
    }
}
