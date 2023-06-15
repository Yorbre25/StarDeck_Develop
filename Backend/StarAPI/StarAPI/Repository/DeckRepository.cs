using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class DeckRepository : RepositoryBase<Deck>, IDeckRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public DeckRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        // public Deck GetByName(string name)
        // {
        //     return RepositoryContext.Deck.FirstOrDefault(ct => ct.name == name);
        // }

    }
}