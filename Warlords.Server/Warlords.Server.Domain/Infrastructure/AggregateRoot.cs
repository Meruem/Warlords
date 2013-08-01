using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using log4net;

namespace Warlords.Server.Domain.Infrastructure
{
    public abstract class AggregateRoot
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(AggregateRoot));

        private readonly List<Event> _changes = new List<Event>();

        public abstract Guid Id { get; set; }
        public int Version { get; internal set; }

        public static Func<Guid, AggregateRoot> CreateMethod { get; set; }

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadsFromHistory(IEnumerable<Event> history)
        {
            Contract.Requires(history != null);
            foreach (var e in history) ApplyChange(e, false);
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(Event @event, bool isNew)
        {
            Contract.Requires(isNew || @event != null);

            _log.Debug(isNew
                           ? string.Format("Applying new event {0} on aggregate {1}", @event, this)
                           : string.Format("Reconstructing aggregate {1} by applying event {0}", @event, this));

            this.AsDynamic().Apply(@event);

            _log.Debug("Event applied.");
            if (isNew) 
            {
                _changes.Add(@event);
            }
            else
            {
                Version = @event.Version;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(GetType().Name);
            builder.Append("(Id: ");
            builder.Append(Id);
            builder.Append(", Version: ");
            builder.Append(Version);
            builder.Append(")");

            return builder.ToString();
        }
    }
}