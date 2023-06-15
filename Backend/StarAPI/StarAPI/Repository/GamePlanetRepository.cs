using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class GamePlanetRepository : RepositoryBase<Game_Planet>, IGamePlanetRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public GamePlanetRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        // public List<Game_Planet> GetByGameId(string gameId) => 
        //     RepositoryContext.Game_Planet.Where(ct => ct.gameId == gameId).ToList();

    }
}