using System;

namespace Warlords.Server.Application.Infrastructure
{
    public class SyncEventScheduler : IEventScheduler
    {
        public void ScheduleJob(Action job)
        {
            job();
        }
    }
}
