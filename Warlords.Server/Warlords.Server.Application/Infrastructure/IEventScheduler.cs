using System;

namespace Warlords.Server.Application.Infrastructure
{
    public interface IEventScheduler
    {
        void ScheduleJob(Action job);
    }
}
