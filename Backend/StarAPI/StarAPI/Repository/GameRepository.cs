using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public GameRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

    }
}