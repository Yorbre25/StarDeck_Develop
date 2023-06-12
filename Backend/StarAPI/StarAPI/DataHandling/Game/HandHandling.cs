using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.Utils;
using StarAPI.DataHandling.Discovery;
using StarAPI.DTO.Discovery;
using StarAPI.Constants;
using StarAPI.Logic;

namespace StarAPI.DataHandling.Game;

public class HandHandling
{
    private readonly StarDeckContext _context;
    private DeckCardHandling _deckCardHandling;
    private GameDeckCardHandling _gameDeckCardHandling;
    private IdGenerator _idGenerator = new IdGenerator();
    private DeckHandling _deckHandling;
    private CardCRUD _cardCRUD;
    private const string IdPrefix = "H";


    public HandHandling(StarDeckContext context)
    {
        this._context = context;
        this._deckCardHandling = new DeckCardHandling(_context);
        this._gameDeckCardHandling = new GameDeckCardHandling(_context);
        this._deckHandling = new DeckHandling(_context);
        this._cardCRUD = new CardCRUD(_context);
    }





    private Hand GetHandByGameId(string gameId)
    {
        return _context.Hand.FirstOrDefault(d => d.gameId == gameId);
    }


    private Hand GetHand(string playerId)
    {
        return _context.Hand.FirstOrDefault(d => d.playerId == playerId);
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
}