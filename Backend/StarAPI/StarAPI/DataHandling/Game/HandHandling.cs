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



    // private int GetHandSize(string playerId)
    // {
    //     return _context.Hand.Count(h => h.playerId == playerId);
    // }



    // private string PickRandomCard(string playerId)
    // {
    //     return _gameDeckCardHandling.DrawCard(playerId);
    // }

    // public OutputCard DrawCard(string gameId, string playerId)
    // {
        // int numCardsInDeck = _gameDeckCardHandling.NumCardsInDeck(playerId);
        // int handSize = GetHandSize(playerId);

        // if(numCardsInDeck == 0 || handSize == Const.MaxHandSize){
        //     return null;
        // }
        // return DrawCardFromDeck(gameId, playerId);
    // }

    // private OutputCard DrawCardFromDeck(string gameId, string playerId)
    // {
    //     string cardId = PickRandomCard(playerId);
    //     Hand newHandCard = new Hand()
    //     {
    //         gameId = gameId,
    //         playerId = playerId,
    //         cardId = cardId
    //     };
    //     _context.Hand.Add(newHandCard);
    //     _context.SaveChanges();
    //     return _cardCRUD.GetCard(cardId);
    // }


    private Hand GetHandByGameId(string gameId)
    {
        return _context.Hand.FirstOrDefault(d => d.gameId == gameId);
    }


    private Hand GetHand(string playerId)
    {
        return _context.Hand.FirstOrDefault(d => d.playerId == playerId);
    }

    internal void RemoveCardFromHand(string playerId, string cardId)
    {
        try 
        {
            RemovingCardFromHand(playerId, cardId);
        }
        catch(Exception e)
        {
            throw new Exception("Error deleting card from hand: ");
        }
    }

    private void RemovingCardFromHand(string playerId, string cardId)
    {
        Hand hand = _context.Hand.FirstOrDefault(h => h.playerId == playerId && h.cardId == cardId);
        if(hand == null){
            throw new ArgumentException("Card not found in hand");
        }
        _context.Hand.Remove(hand);
        _context.SaveChanges();
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