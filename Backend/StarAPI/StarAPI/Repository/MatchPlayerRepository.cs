using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class MatchPlayerRepository : RepositoryBase<Match_Player>, IMatchPlayerRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public MatchPlayerRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

    }
}