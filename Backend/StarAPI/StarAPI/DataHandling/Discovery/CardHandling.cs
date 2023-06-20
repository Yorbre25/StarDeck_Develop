using StarAPI.Models;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.Logic.Mappers;
using Contracts;

namespace StarAPI.DataHandling.Discovery;


public class CardHandling
{
    private readonly IRepositoryWrapper _repository;

    private IdGenerator _idGenerator = new IdGenerator();
    private RaceHandling _raceHandling;
    private CardTypeHandling _cardTypeHandling;
    private CardMapper _cardMapper;
    private static string s_idPrefix = "C";


    public CardHandling(IRepositoryWrapper repository)
    {
        this._repository = repository;
        this._cardMapper = new CardMapper(_repository);
    }


    public List<OutputCard> GetAllCards()
    {
        List<Card> cards = _repository.Card.GetAll();
        return _cardMapper.FillOutputCard(cards);
    }

    public OutputCard GetCard(string id){
        Card? card = _repository.Card.Get(id);
        return _cardMapper.FillOutputCard(card);
    }

    public void AddCard(InputCard inputCard)
    {
        string id = GenerateId();
        var newCard = _cardMapper.FillNewCard(inputCard, id);
        _repository.Card.Add(newCard);
        _repository.Save();
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

    public void DeleteCard(string id){
        Card? card = _repository.Card.Get(id);
        _repository.Card.Delete(card);
        _repository.Save();
    }

    public bool NameAlreadyExists(string cardName)
    {
        var cards = _repository.Card.GetAll();
        var card = cards.FirstOrDefault(r => r.name == cardName);
        if(card == null){
            return false;
        }
        return true;
    }


    private bool IdAlreadyExists(string id){
        Card? card = _repository.Card.Get(id);
        if(card == null){
            return false;
        }
        return true;
    }

    public List<OutputCard> GetCardsByType(string cardTypeName)
    {
        List<OutputCard> allCards = GetAllCards();
        List<OutputCard> specificCards;
        specificCards = allCards.Where(c => c.type == cardTypeName).ToList();
        return specificCards;
    }

}


