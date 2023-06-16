using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class CardTypeRepository : RepositoryBase<CardType>, ICardTypeRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public CardTypeRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        // public CardType GetByName(string typeName) => 
        //     RepositoryContext.CardType.FirstOrDefault(ct => ct.typeName == typeName);
    }
}