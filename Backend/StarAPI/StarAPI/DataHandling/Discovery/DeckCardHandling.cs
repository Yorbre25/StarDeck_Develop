using StarAPI.DTO.Discovery;
using StarAPI.Logic;
using Contracts;
using System.Linq.Expressions;
using StarAPI.Models;

namespace StarAPI.DataHandling.Discovery;

public class DeckCardHandling
{
    private readonly IRepositoryWrapper _repository;
    private CardCRUD _cardCRUD;

    public DeckCardHandling(IRepositoryWrapper repository)
    {
        this._repository = repository;
        this._cardCRUD = new CardCRUD(_repository);
    }


    public List<OutputCard> GetCardsFromDeck(string deckId)
    {
        try
        {
            return GettingCardsFromDeck(deckId);
        }
        catch (System.Exception)
        {
            
            throw new Exception("Error getting cards from deck");
        }
    }

    private List<OutputCard> GettingCardsFromDeck(string deckId)
    {
        // var deckCards = _repository.Deck_Card.ToList();
        var deckCards = _repository.DeckCard.GetAll();
        string [] cardsId = deckCards.FindAll(dc => dc.deckId == deckId).Select(dc => dc.cardId).ToArray();
        return GetCards(cardsId);
    }

    //!DEBERIA ESTAR EN OTRA CLASE
    public List<OutputCard> GetCards(string[] cardsId)
    {
        List<OutputCard> outputCards = new List<OutputCard>();
        foreach (var id in cardsId)
        {
            outputCards.Add(_cardCRUD.GetCard(id));
        }
        return outputCards;
    }

        public string[] GetCardIdsFromDeck(string deckId)
    {
        try
        {
            return GettingCardIdsFromDeck(deckId);
        }
        catch (System.Exception)
        {
            
            throw new Exception("Error getting cards from deck");
        }
    }

    public string[] GettingCardIdsFromDeck(string deckId)
    {
        // var deckCards = _repository.Deck_Card.Where(dc => dc.deckId == deckId);
        var deckCards = GetDeckCards(deckId);

        string[] cardIds = new string[deckCards.Count()];
        int i = 0;
        foreach (var deckCard in deckCards)
        {
            cardIds[i] = deckCard.cardId;
            i++;
        }
        return cardIds;
    }

    private List<Deck_Card> GetDeckCards(string deckId)
    {
        List<Deck_Card> deckCards = _repository.DeckCard.GetAll();
        List<Deck_Card> deckCardsFiltered = deckCards.FindAll(dc => dc.deckId == deckId);
        return deckCardsFiltered;
    }
}