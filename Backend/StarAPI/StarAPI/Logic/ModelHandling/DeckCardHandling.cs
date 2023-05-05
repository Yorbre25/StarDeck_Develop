using StarAPI.Models;
using StarAPI.DTOs;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTOs;

namespace StarAPI.Logic.ModelHandling;

public class DeckCardHandling
{
    private readonly StarDeckContext _context;
    private CardHandling _cardHandling;

    public DeckCardHandling(StarDeckContext context)
    {
        this._context = context;
        this._cardHandling = new CardHandling(_context);
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
        var deckCards = _context.Deck_Card.ToList();
        string [] cardsId = deckCards.FindAll(dc => dc.deckId == deckId).Select(dc => dc.cardId).ToArray();
        return GetCards(cardsId);
    }

    private List<OutputCard> GetCards(string[] cardsId)
    {
        List<OutputCard> outputCards = new List<OutputCard>();
        foreach (var id in cardsId)
        {
            outputCards.Add(_cardHandling.GetCard(id));
        }
        return outputCards;
    }
}