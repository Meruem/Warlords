using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raven.Client;
using Warlords.Server.ApplicationF;
using Warlords.Server.Infrastructure;

namespace Warlords.Server.Controllers
{
    public class PlayerController : RavenDbController
    {
        // GET api/<controller>
        public Task<IList<Player>> Get()
        {
            return Session.Query<Player>().ToListAsync();
        }

        // GET api/<controller>/name
        public Task<Player> Get(string name)
        {
            return
                Session.Query<Player>().Where(p => p.Name == name).FirstOrDefaultAsync();
        }
    }
}