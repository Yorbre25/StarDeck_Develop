using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class GameDeckRepository : RepositoryBase<Game_Deck>, IGameDeckRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public GameDeckRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        // public List<Game_Deck> GetByPlayerId(string playerId)
        // {
        //     return FindByCondition(c => c.playerId == playerId).ToList();
        // }
    }
}