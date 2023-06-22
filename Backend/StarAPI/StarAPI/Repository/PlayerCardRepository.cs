using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class PlayerCardRepository : RepositoryBase<Player_Card>, IPlayerCardRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public PlayerCardRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

    }
}