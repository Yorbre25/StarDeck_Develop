using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.Utils;
using StarAPI.DataHandling.Discovery;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;

namespace StarAPI.DataHandling.Game;

public class HandHandling
{
    private readonly StarDeckContext _context;
    private DeckCardHandling _deckCardHandling;
    private GameDeckCardHandling _gameDeckCardHandling;
    private IdGenerator _idGenerator = new IdGenerator();
    private DeckHandling _deckHandling;
    private const string IdPrefix = "H";
    private const int IntialCardsPerHand = 7;


    public HandHandling(StarDeckContext context)
    {
        this._context = context;
        this._deckCardHandling = new DeckCardHandling(_context);
        this._gameDeckCardHandling = new GameDeckCardHandling(_context);
        this._deckHandling = new DeckHandling(_context);
    }

    public void SetupHand(string playerId)
    {
        try
        {
            CreateHand(playerId);
            GiveInitialCards(playerId);
            _context.SaveChanges();
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }



    private void GiveInitialCards(string playerId)
    {
        string handId = GetHandId(playerId);
        string cardId;
        int handSize = SetHandSize(playerId);
        for (int i = 0; i < handSize; i++)
        {
            cardId = DrawCard(playerId);
            Hand_Card newHandCard = new Hand_Card();
            newHandCard.handId = handId;
            newHandCard.cardId = cardId;

            _context.Hand_Card.Add(newHandCard);
            _context.SaveChanges();
        }
    }

    private int SetHandSize(string playerId)
    {
        int numCardsInDeck = _gameDeckCardHandling.NumCardsInDeck(playerId);
        if(numCardsInDeck < IntialCardsPerHand){
            return numCardsInDeck;
        }
        return IntialCardsPerHand;
    }



    private string DrawCard(string playerId)
    {
        return _gameDeckCardHandling.DrawCard(playerId);
    }

    private string GetHandId(string playerId)
    {
        Hand hand = _context.Hand.FirstOrDefault(d => d.playerId == playerId);
        return hand.id;
    }

    internal string CreateHand(string playerId)

    {
        bool alreadyExist = PlayerAlreadyHasHand(playerId);
        if(alreadyExist){
            throw new ArgumentException("Player already has a hand");
        }
        return CreatingHand(playerId);
    }

    private string CreatingHand(string playerId)
    {
        Hand newHand = new Hand();
        string id = GenerateId();
        newHand.id = id;
        newHand.playerId = playerId;
        
        _context.Hand.Add(newHand);
        _context.SaveChanges();
        return id;
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

    internal void Delete(string playerId)
    // internal List<Hand_Card> Delete(string playerId)
    {
        Hand hand = GetHand(playerId);
        if (hand == null)
        {
            return;
        }
        string id = hand.id;
        DeleteCardsFromHand(id);
        DeleteHand(hand);   
    }

    private void DeleteCardsFromHand(string handId)
    // private List<Hand_Card> DeleteCardsFromHand(string handId)
    {
        List<Hand_Card> handCards = GetHandCards(handId);
        _context.Hand_Card.RemoveRange(handCards);
        _context.SaveChanges();
        // return handCards;
    }

    public List<OutputCard> GetHandCardsByPlayerId(string playerId)
    {
        string handId = GetHandId(playerId);
        var handCards = _context.Hand_Card.ToList();
        var cardsIds = handCards.FindAll(d => d.handId == handId).Select(d => d.cardId).ToArray();
        return _deckCardHandling.GetCards(cardsIds);
    }

    private List<Hand_Card> GetHandCards(string handId)
    {
        var handCards = _context.Hand_Card.ToList();
        return handCards.FindAll(d => d.handId == handId);
    }

    private void DeleteHand(Hand hand)
    {
        _context.Hand.Remove(hand);
    }

    private Hand GetHand(string playerId)
    {
        return _context.Hand.FirstOrDefault(d => d.playerId == playerId);
    }





    // public void AddHands(SetUpValues setUpValues)
    // {
    //     string[] gameDeckIds = new string[2];
    //     string[] cardsFromDeck = GetCardIds(setUpValues.player1DeckId);
    //     AddCardsToDeck(setUpValues.player1DeckId, cardsFromDeck);

    //     string[] cardsFromDeck2 = GetCardIds(setUpValues.player2DeckId);
    //     AddCardsToDeck(setUpValues.player2DeckId, cardsFromDeck2);


    // }
}