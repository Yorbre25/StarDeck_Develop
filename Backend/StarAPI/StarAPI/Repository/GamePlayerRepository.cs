using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class GamePlayerRepository : RepositoryBase<Game_Player>, IGamePlayerRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public GamePlayerRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

    }
}