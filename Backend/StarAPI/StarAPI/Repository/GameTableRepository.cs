using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class GameTableRepository : RepositoryBase<GameTable>, IGameTableRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public GameTableRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

    }
}