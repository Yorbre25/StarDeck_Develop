using StarAPI.Models;
using StarAPI.Logic.Utils;
using StarAPI.DTO.Discovery;
using StarAPI.Context;
using StarAPI.Logic.Mappers;
using Contracts;
using System.Linq.Expressions;
using StarAPI.Constants;

namespace StarAPI.DataHandling.Discovery;


public class DeckHandling
{
    private readonly IRepositoryWrapper _repository;

    private IdGenerator _idGenerator = new IdGenerator();
    private DeckMapper _deckMapper;

    private static string s_idPrefix = "D";

    public DeckHandling(IRepositoryWrapper repository)
    {
        this._repository = repository;
        this._deckMapper = new DeckMapper(_repository);
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
        _repository.Deck.Add(newDeck);
        // AddCardsToDeck(newDeck.id, inputDeck.cardIds);
        AddCardsToDeck(newDeck.id, inputDeck.cardIds);
        _repository.Save();
    }



    private void AddCardsToDeck(string id, string[] cardIds)
    {
        if(cardIds.Count() != Const.CardsPerDeck){
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
            // _repository.Deck_Card.Add(deckCard);
            _repository.DeckCard.Add(deckCard);
        }
    }

    private bool NameAlreadyExists(string playerId, string name)
    {
        // var decks = _repository.Deck.Where(d => d.playerId == playerId);
        var decks = GetDeckByPlayerId(playerId);
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
    // deck = _repository.Deck.FirstOrDefault(c => c.id == id);
    deck = _repository.Deck.Get(id);
    if(deck == null){
        return false;
    }
    return true;
    }

    private bool CheckInputValues(InputDeck inputDeck)
    {
        bool isValid = true;
        string deckName = inputDeck.name;
        if(deckName.Length < Const.MinDeckNameLength || deckName.Length > Const.MaxDeckNameLength)
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
        // var decks = _repository.Deck.Where(d => d.playerId == playerId);
        var decks = GetDeckByPlayerId(playerId);
        foreach (var deck in decks)
        {
            OutputDeck outputDeck = _deckMapper.FillOutputDeck(deck);
            outputDecks.Add(outputDeck);
        }
        return outputDecks;
    }

    List<Deck> GetDeckByPlayerId(string playerId)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(Deck), "t");
        Expression property = Expression.Property(parameter, "playerId");
        ConstantExpression idValue = Expression.Constant(playerId, typeof(string));
        Expression condition = Expression.Equal(property, idValue);
        Expression<Func<Deck, bool>> lambdaExpression = Expression.Lambda<Func<Deck, bool>>(condition, parameter);
        return _repository.Deck.FindByCondition(lambdaExpression);
    }

    internal string GetDeckName(string id)
    {
        var deck = GetDeck(id);
        return deck.name;
    }

    private Deck GetDeck(string id)
    {
        return _repository.Deck.Get(id);
    }
}