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
        // public PlanetType GetByName(string name)
        // {
        //     return RepositoryContext.PlanetType.FirstOrDefault(ct => ct.typeName == name);
        // }

    }
}