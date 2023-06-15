using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class DeckCardRepository : RepositoryBase<Deck_Card>, IDeckCardRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public DeckCardRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

    }
}