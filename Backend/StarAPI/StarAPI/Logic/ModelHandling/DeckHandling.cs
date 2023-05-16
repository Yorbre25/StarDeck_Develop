using StarAPI.Models;
using StarAPI.DTOs;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.Logic.Mappers;

namespace StarAPI.Logic.ModelHandling;


public class DeckHandling
{
    private readonly StarDeckContext _context;

    private IdGenerator _idGenerator = new IdGenerator();
    private DeckMapper _deckMapper;

    private static int s_minDeckNameLength = 5;
    private static int s_maxDeckNameLength = 30;

    private static int s_cardsPerDeck =18;

    private static string s_idPrefix = "D";

    public DeckHandling(StarDeckContext context)
    {
        this._context = context;
        this._deckMapper = new DeckMapper(_context);
    }

    public void AddDeck(InputDeck inputDeck)
    {
        bool isValid = CheckInputValues(inputDeck);
        bool alreadyExist = NameAlreadyExists(inputDeck.playerId, inputDeck.name);

        if(!isValid){
            throw new ArgumentException("Invalid deck name");
        }
        if(alreadyExist){
            throw new ArgumentException("Deck name already exist");
        }
        AddingDeck(inputDeck);

    }

    private void AddingDeck(InputDeck inputDeck)
    {
        string id = GenerateId();
        Deck newDeck = _deckMapper.FillNewDeck(inputDeck, id);
        _context.Deck.Add(newDeck);
        AddCardsToDeck(newDeck.id, inputDeck.cardIds);
        _context.SaveChanges();
    }



    private void AddCardsToDeck(string id, string[] cardIds)
    {
        if(cardIds.Count() != s_cardsPerDeck){
           throw new ArgumentException("Invalid number of cards");
        }
        try
        {
            AddingCardsToDeck(id, cardIds);
        }
        catch (System.Exception)
        {
            
            throw new Exception("Error adding cards to deck");
        }
    }

    private void AddingCardsToDeck(string id, string[] cardIds)
    {
        foreach (var cardId in cardIds)
        {
            Deck_Card deckCard = new Deck_Card();
            deckCard.deckId = id;
            deckCard.cardId = cardId;
            _context.Deck_Card.Add(deckCard);
        }
    }

    private bool NameAlreadyExists(string playerId, string name)
    {
        var decks = _context.Deck.Where(d => d.playerId == playerId);
        bool alreadyExists = false;
        foreach (var deck in decks)
        {
            if(deck.name == name){
                alreadyExists = true;
                break;
            }
        }
        return alreadyExists;
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
    Deck? deck = new Deck();
    deck = _context.Deck.FirstOrDefault(c => c.id == id);
    if(deck == null){
        return false;
    }
    return true;
    }

    private bool CheckInputValues(InputDeck inputDeck)
    {
        bool isValid = true;
        string deckName = inputDeck.name;
        if(deckName.Length < s_minDeckNameLength || deckName.Length > s_maxDeckNameLength)
        {
            isValid = false;
        }
        return isValid;
    }

    public List<OutputDeck> GetDecksFromPlayer(string playerId)
    {
        try
        {
            return GettingDecksFromPlayer(playerId);
        }
        catch (System.Exception)
        {
            
            throw new Exception("Error getting decks");
        }
    }

    private List<OutputDeck> GettingDecksFromPlayer(string playerId)
    {
        List<OutputDeck> outputDecks = new List<OutputDeck>();
        var decks = _context.Deck.Where(d => d.playerId == playerId);
        foreach (var deck in decks)
        {
            OutputDeck outputDeck = _deckMapper.FillOutputDeck(deck);
            outputDecks.Add(outputDeck);
        }
        return outputDecks;
    }
}