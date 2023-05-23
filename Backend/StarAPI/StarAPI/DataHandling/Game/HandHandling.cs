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

    public void SetupHand(string gameId, string playerId)
    {
        try
        {
            CreateHand(gameId, playerId);
            _context.SaveChanges();
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }



    private void GiveInitialCards(string gameId, string playerId)
    {
        string cardId;
        int handSize = SetHandSize(playerId);
        for (int i = 0; i < handSize; i++)
        {
            cardId = PickRandomCard(playerId);
            Hand newHandCard = new Hand()
            {
                gameId = gameId,
                playerId = playerId,
                cardId = cardId
            };
            _context.Hand.Add(newHandCard);
            _context.SaveChanges();
        }
    }

    private int SetHandSize(string playerId)
    {
        int numCardsInDeck = _gameDeckCardHandling.NumCardsInDeck(playerId);
        if(numCardsInDeck < Const.IntialCardsPerHand){
            return numCardsInDeck;
        }
        return Const.IntialCardsPerHand;
    }

    private int GetHandSize(string playerId)
    {
        return _context.Hand.Count(h => h.playerId == playerId);
    }



    private string PickRandomCard(string playerId)
    {
        return _gameDeckCardHandling.DrawCard(playerId);
    }

    public OutputCard DrawCard(string gameId, string playerId)
    {
        int numCardsInDeck = _gameDeckCardHandling.NumCardsInDeck(playerId);
        int handSize = GetHandSize(playerId);

        if(numCardsInDeck == 0 || handSize == Const.MaxHandSize){
            return null;
        }
        return DrawCardFromDeck(gameId, playerId);
    }

    private OutputCard DrawCardFromDeck(string gameId, string playerId)
    {
        string cardId = PickRandomCard(playerId);
        Hand newHandCard = new Hand()
        {
            gameId = gameId,
            playerId = playerId,
            cardId = cardId
        };
        _context.Hand.Add(newHandCard);
        _context.SaveChanges();
        return _cardCRUD.GetCard(cardId);
    }

    internal void CreateHand(string gameId, string playerId)

    {
        bool alreadyExist = PlayerAlreadyHasHand(playerId);
        if(alreadyExist){
            throw new ArgumentException("Player already has a hand");
        }
        GiveInitialCards(gameId, playerId);
    }

 

    private string GenerateId()
    {
        string id = "";
        bool alreadyExists = true;
        while (alreadyExists)
        {
            id = _idGenerator.GenerateId(IdPrefix);
            alreadyExists = IdAlreadyExists(id);
        }
        return id;
    }

    
    private bool IdAlreadyExists(string id){
        StarAPI.Models.Game? game = _context.Game.FirstOrDefault(c => c.id == id);
        if (game == null){
            return false;
        }
        return true;
    }

    private bool PlayerAlreadyHasHand(string playerId)
    {
        var hand = _context.Hand.FirstOrDefault(d => d.playerId == playerId);
        bool alreadyHas = false;
        if(hand != null){
            alreadyHas = true;
        }
        return alreadyHas;
    }

    internal void EndGame(string gameId)
    {
        Hand hand = GetHandByGameId(gameId);
        if (hand == null)
        {
            return;
        }
        DeleteHand(gameId);   
    }

    private Hand GetHandByGameId(string gameId)
    {
        return _context.Hand.FirstOrDefault(d => d.gameId == gameId);
    }

    public List<OutputCard> GetHandCardsByPlayerId(string playerId)
    {
        var cardsIds = _context.Hand.Where(d => d.playerId == playerId).Select(d => d.cardId).ToArray();
        return _deckCardHandling.GetCards(cardsIds);
    }
    private void DeleteHand(string gameId)
    {
        List<Hand> cards = _context.Hand.Where(h => h.gameId == gameId).ToList();
        _context.Hand.RemoveRange(cards);
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
        //El get lo debería hacer otra función
        Hand hand = _context.Hand.FirstOrDefault(h => h.playerId == playerId && h.cardId == cardId);
        if(hand == null){
            throw new ArgumentException("Card not found in hand");
        }
        _context.Hand.Remove(hand);
        _context.SaveChanges();
    }
}