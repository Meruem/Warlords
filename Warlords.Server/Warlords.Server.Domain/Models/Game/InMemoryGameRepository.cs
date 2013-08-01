using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Warlords.Server.Domain.Models.Game
{
    public class InMemoryGameRepository : GameRepository
    {
        private readonly IDictionary<string, Game> _games;

        public InMemoryGameRepository()
        {
            _games = new ConcurrentDictionary<string, Game>();
        }

        public override Task AddGame(Game game)
        {
            return Task.Run(() => _games.Add(game.Id, game));
        }

        public override Task<Game> GetGameById(string id)
        {
            return Task.Run(() => _games.ContainsKey(id) ? _games[id] : null);
        }
    }
}