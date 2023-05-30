using StarAPI.Models;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.Logic.Mappers;

namespace StarAPI.DataHandling.Discovery;


public class CardHandling
{
    private readonly StarDeckContext _context;

    private IdGenerator _idGenerator = new IdGenerator();
    private RaceHandling _raceHandling;
    private CardTypeHandling _cardTypeHandling;
    private CardMapper _cardMapper;
    private static string s_idPrefix = "C";


    public CardHandling(StarDeckContext context)
    {
        this._context = context;
        this._cardMapper = new CardMapper(_context);
    }


    public List<OutputCard> GetAllCards()
    {
        List<Card> cards = _context.Card.ToList();
        return _cardMapper.FillOutputCard(cards);
    }

    public OutputCard GetCard(string id){
        Card? card = _context.Card.FirstOrDefault(r => r.id == id);
        return _cardMapper.FillOutputCard(card);
    }

    public void AddCard(InputCard inputCard)
    {
        string id = GenerateId();
        var newCard = _cardMapper.FillNewCard(inputCard, id);
        _context.Card.Add(newCard);
        _context.SaveChanges();
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
        Card? card = _context.Card.FirstOrDefault(r => r.id == id);
        _context.Card.Remove(card);
        _context.SaveChanges();
    }

    public bool NameAlreadyExists(string cardName)
    {
        var card = _context.Card.FirstOrDefault(r => r.name == cardName);
        if(card == null){
            return false;
        }
        return true;
    }


    private bool IdAlreadyExists(string id){
        Card? card = _context.Card.FirstOrDefault(c => c.id == id);
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


