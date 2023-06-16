using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class RaceRepository : RepositoryBase<Race>, IRaceRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public RaceRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        // public Race GetByName(string name)
        // {
        //     return RepositoryContext.Race.FirstOrDefault(ct => ct.name == name);
        // }
    }
}