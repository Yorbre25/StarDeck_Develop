using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.Utils;
using StarAPI.DataHandling.Discovery;
using StarAPI.DTO.Discovery;
using StarAPI.Constants;
using StarAPI.Logic;

namespace StarAPI.DataHandling.Game;

public class HandCard
{
    private readonly StarDeckContext _context;
    private DeckCardHandling _deckCardHandling;
    private IdGenerator _idGenerator = new IdGenerator();
    private DeckHandling _deckHandling;
    private CardCRUD _cardCRUD;
    private const string IdPrefix = "H";


    public HandCard(StarDeckContext context)
    {
        this._context = context;
        this._deckCardHandling = new DeckCardHandling(_context);
        this._deckHandling = new DeckHandling(_context);
        this._cardCRUD = new CardCRUD(_context);
    }

    internal List<OutputCard> GetHandCards(string gameId, string playerId)
    {
        try
        {
            return GetHandCardsByPlayerId(playerId);
        }
        catch (Exception e)
        {
            throw new Exception("Error getting hand cards: " + e.Message);
        }
    }

       public List<OutputCard> GetHandCardsByPlayerId(string playerId)
    {
        var cardsIds = _context.Hand.Where(d => d.playerId == playerId).Select(d => d.cardId).ToArray();
        return _deckCardHandling.GetCards(cardsIds);
    }

    public void RemoveCardsFromHand(string playerId, string cardId)
    {
        Hand? hand = _context.Hand.FirstOrDefault(h => h.playerId == playerId && h.cardId == cardId);
        _context.Hand.Remove(hand);
    }
}