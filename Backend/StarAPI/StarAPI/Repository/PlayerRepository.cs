using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public PlayerRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        // public Player GetByUsername(string username) => 
        //     RepositoryContext.Player.FirstOrDefault(ct => ct.username == username);

        // public Player GetByEmail(string email) => 
        //     RepositoryContext.Player.FirstOrDefault(ct => ct.email == email);

    }
}
