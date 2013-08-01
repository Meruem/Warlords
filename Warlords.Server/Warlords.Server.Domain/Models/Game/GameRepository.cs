using System.Threading.Tasks;

namespace Warlords.Server.Domain.Models.Game
{
    public abstract class GameRepository
    {
        public abstract Task<Game> GetGameById(string id);
        public abstract Task AddGame(Game game); 
    }
}