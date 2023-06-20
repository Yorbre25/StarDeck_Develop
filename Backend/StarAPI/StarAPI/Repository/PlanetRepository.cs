using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class PlanetRepository : RepositoryBase<Planet>, IPlanetRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public PlanetRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        // public Planet GetByName(string name)
        // {
        //     return RepositoryContext.Planet.FirstOrDefault(ct => ct.name == name);
        // }

    }
}