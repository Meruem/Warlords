using System;
using System.Threading.Tasks;

namespace Warlords.Server.Application.Infrastructure
{
    public class AsyncEventScheduler : IEventScheduler
    {
        public void ScheduleJob(Action job)
        {
            Task.Factory.StartNew(job);
        }
    }
}
