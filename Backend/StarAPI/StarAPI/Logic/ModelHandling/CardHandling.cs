using StarAPI.Models;
using StarAPI.DTOs;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.Logic.Mappers;

namespace StarAPI.Logic.ModelHandling;


public class CardHandling
{
    private readonly StarDeckContext _context;

    private IdGenerator _idGenerator = new IdGenerator();
    private RaceHandling _raceHandling;
    private CardTypeHandling _cardTypeHandling;
    private CardMapper _cardMapper;
    private static int s_minCardNameLenght = 5;
    private static int s_maxCardNameLenght = 30;
    private static int s_minEnergyValue = -100;
    private static int s_maxEnergyValue = 100;
    private static int s_minBattleCost = 0;
    private static int s_maxBattleCost = 100;
    private static int s_maxDescriptionLenght = 1000;
    private static string s_idPrefix = "C";


    public CardHandling(StarDeckContext context)
    {
        this._context = context;
        this._cardTypeHandling = new CardTypeHandling(_context);
        this._raceHandling = new RaceHandling(_context);
        this._cardMapper = new CardMapper(_context);
    }


    public List<OutputCard> GetAllCards()
    {
        try
        {
            return GettingAllCards();
        } 
        catch (System.Exception)
        {
            throw new Exception("Error getting cards");
        }
    }


    private List<OutputCard> GettingAllCards()
    {
        List<Card> cards = _context.Card.ToList();
        return _cardMapper.FillOutputCard(cards);
    }

    public OutputCard GetCard(string id){
        try
        {
            return GetOutputCard(id);
        }
        catch (System.Exception)
        {
            throw new ArgumentException("Invalid id");
        }
    }

    private OutputCard GetOutputCard(string id)
    {
        Card? card = ExtractCard(id);
        return _cardMapper.FillOutputCard(card);
    }

    private Card ExtractCard(string id)
    {
        Card? card = _context.Card.FirstOrDefault(r => r.id == id);
        return card;
    }


    public void AddCard(InputCard inputCard)
    {
        bool isValid = CheckInputValues(inputCard);
        bool alreadyExist = NameAlreadyExists(inputCard.name);

        if(!isValid){
            throw new ArgumentException("Invalid Values");
        }
        if(alreadyExist){
            throw new ArgumentException("Card name already exist");
        }
        AddingCard(inputCard);

    }


    public void AddingCard(InputCard inputCard){
        string id = GenerateId();
        var newCard = _cardMapper.FillNewCard(inputCard, id);
        _context.Card.Add(newCard);
        _context.SaveChanges();
    }

    public void DeleteCard(string id){
        try
        {
            DeletingCard(id);
        }
        catch (System.Exception)
        {
            throw new ArgumentException("Invalid id");
        }
    }

    private void DeletingCard(string id)
    {
        Card? card = ExtractCard(id);
        _context.Card.Remove(card);
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

    private bool CheckInputValues(InputCard card){
        bool isValid = true;
        if(card.name.Length < s_minCardNameLenght || card.name.Length > s_maxCardNameLenght){
            throw new Exception("Invalid name lenght");
        }
        else if(card.energy < s_minEnergyValue || card.energy > s_maxEnergyValue){
            throw new Exception("Invalid energy value");
        }
        else if(card.cost < s_minBattleCost || card.cost > s_maxBattleCost){
            throw new Exception("Invalid battle cost");
        }
        else if(card.description.Length > s_maxDescriptionLenght){
            throw new Exception("Invalid description lenght");
        }
        return isValid;
    }


    private bool NameAlreadyExists(string cardName)
    {
        var card = _context.Card.FirstOrDefault(r => r.name == cardName);
        if(card == null){
            return false;
        }
        return true;
    }


    private bool IdAlreadyExists(string id){
        Card? card = new Card();
        card = _context.Card.FirstOrDefault(c => c.id == id);
        if(card == null){
            return false;
        }
        return true;
    }

    public List<OutputCard> GetCardsWith(string cardTypeName)
    {
        List<OutputCard> allCards = GetAllCards();
        List<OutputCard> specificCards;
        specificCards = allCards.Where(c => c.type == cardTypeName).ToList();
        return specificCards;
    }

    public OutputCard GetRandomCard()
    {
        List<OutputCard> cards = GetAllCards();
        Random random = new Random();
        int index = random.Next(cards.Count);
        return cards[index];
    }

}


