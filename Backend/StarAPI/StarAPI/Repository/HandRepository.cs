using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class HandRepository : RepositoryBase<Hand>, IHandRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public HandRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        // public List<Hand> GetByPlayerId(string playerId)
        // {
        //     return FindByCondition(c => c.playerId == playerId).ToList();
        // }

    }
}