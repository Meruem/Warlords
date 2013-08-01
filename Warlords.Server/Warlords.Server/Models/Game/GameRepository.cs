using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Warlords.Server.Models
{
    public abstract class GameRepository
    {
        public abstract Task<Game> GetGameById(string id);
        public abstract Task AddGame(Game game); 
    }
}