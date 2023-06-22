using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public CardRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        // public Card GetByName(string name)
        // {
        //     return RepositoryContext.Card.FirstOrDefault(ct => ct.name == name);
        // }
    }
}