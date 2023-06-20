using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class PlanetTypeRepository : RepositoryBase<PlanetType>, IPlanetTypeRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public PlanetTypeRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

    }
}