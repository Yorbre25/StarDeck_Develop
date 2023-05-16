using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.GameLogic;
using StarAPI.Logic.Utils;
using StarAPI.DTOs;

namespace StarAPI.Logic.ModelHandling;

public class HandHandling
{
    private readonly StarDeckContext _context;
    private DeckCardHandling _deckCardHandling;
    private GameDeckCardHandling _gameDeckCardHandling;
    private IdGenerator _idGenerator = new IdGenerator();
    private DeckHandling _deckHandling;
    private const string s_idPrefix = "H";
    private const int s_intialCardsPerHand = 5;


    public HandHandling(StarDeckContext context)
    {
        this._context = context;
        this._deckCardHandling = new DeckCardHandling(_context);
        this._gameDeckCardHandling = new GameDeckCardHandling(_context);
        this._deckHandling = new DeckHandling(_context);
    }

    // public void SetupHand(string playerId)
    public List<OutputCard> SetupHand(string playerId)
    {
        try
        {
            CreateHand(playerId);
            GiveInitialCards(playerId);
            return GetHandCards(playerId);
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private List<OutputCard> GetHandCards(string playerId)
    {
        string handId = GetHandId(playerId);
        var handCards = _context.Hand_Card.ToList();
        var cardsIds = handCards.FindAll(d => d.handId == handId).Select(d => d.cardId).ToList().ToArray();
        return _deckCardHandling.GetCards(cardsIds);
    }

    private void GiveInitialCards(string playerId)
    // private string GiveInitialCards(string playerId)
    {
        string handId = GetHandId(playerId);
        string cardIdFromDeck;
        // Verificar el tamaño de la mano
        for (int i = 0; i < s_intialCardsPerHand; i++)
        {
            cardIdFromDeck = DrawCard(playerId);
            Hand_Card newHandCard = new Hand_Card();
            newHandCard.handId = handId;
            newHandCard.cardId = cardIdFromDeck;
            _context.Hand_Card.Add(newHandCard);
            _context.SaveChanges();
        }

    }

    
    // private string DrawCard(string playerId)
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
        string id = GenerateId();
        Hand newHand = new Hand();
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
            id = _idGenerator.GenerateId(s_idPrefix);
            alreadyExists = IdAlreadyExists(id);
        }
        return id;
    }

    
    private bool IdAlreadyExists(string id){
        Game? game;
        game = _context.Game.FirstOrDefault(c => c.id == id);
        if(game == null){
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



    // public void AddHands(SetUpValues setUpValues)
    // {
    //     string[] gameDeckIds = new string[2];
    //     string[] cardsFromDeck = GetCardIds(setUpValues.player1DeckId);
    //     AddCardsToDeck(setUpValues.player1DeckId, cardsFromDeck);

    //     string[] cardsFromDeck2 = GetCardIds(setUpValues.player2DeckId);
    //     AddCardsToDeck(setUpValues.player2DeckId, cardsFromDeck2);


    // }
}