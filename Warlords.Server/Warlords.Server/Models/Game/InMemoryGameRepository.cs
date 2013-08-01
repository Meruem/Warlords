using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Warlords.Server.Models
{
    public class InMemoryGameRepository : GameRepository
    {
        private IDictionary<string, Game> _Games;

        public InMemoryGameRepository()
        {
            _Games = new ConcurrentDictionary<string, Game>();
        }

        public override Task AddGame(Game game)
        {
            return Task.Run(() => _Games.Add(game.Id, game));
        }

        public override Task<Game> GetGameById(string id)
        {
            return Task.Run(() => _Games.ContainsKey(id) ? _Games[id] : null);
        }
    }
}