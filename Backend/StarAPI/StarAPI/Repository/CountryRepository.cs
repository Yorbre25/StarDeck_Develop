using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public CountryRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        // public Country GetByName(string typeName) => 
        //     RepositoryContext.Country.FirstOrDefault(ct => ct.countryName == typeName);
    }
}