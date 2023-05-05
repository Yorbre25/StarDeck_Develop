using StarAPI.Models;
using StarAPI.DTOs;
using StarAPI.Logic.Utils;
using StarAPI.Context;

namespace StarAPI.Logic.ModelHandling;


public class CardHandling
{
    private readonly StarDeckContext _context;

    private IdGenerator _idGenerator = new IdGenerator();
    private RaceHandling _raceHandling;
    private CardTypeHandling _cardTypeHandling;
    private static int s_minCardNameLenght = 5;
    private static int s_maxCardNameLenght = 30;
    private static bool s_defaultActivationState = true;
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
        return CardsToOutputCards(cards);
    }

    private List<OutputCard> CardsToOutputCards(List<Card> cards)
    {
        List<OutputCard> outputCards = new List<OutputCard>();
        foreach(var card in cards)
        {
            outputCards.Add(PassCardValuesToOutputCard(card));
        }
        return outputCards;
    }

    private OutputCard PassCardValuesToOutputCard(Card card)
    {
        OutputCard outputCard = new OutputCard
        {
            id = card.id,
            name = card.name,
            energy = card.energy,
            cost = card.cost,
            type = _cardTypeHandling.GetCardType(card.typeId),
            race = _raceHandling.GetRace(card.raceId),
            description = card.description,
            image = "Hola"
        };
        return outputCard;
    }

    public OutputCard GetCard(string id){
        try
        {
            return GettingCard(id);
        }
        catch (System.Exception)
        {
            throw new ArgumentException("Invalid id");
        }
    }

    private OutputCard GettingCard(string id)
    {
        Card? card = ExtractCard(id);
        return PassCardValuesToOutputCard(card);
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
        InsertCard(inputCard);

    }


    public void InsertCard(InputCard inputCard){
        var newCard = SetNewCardValues(inputCard);
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

    private Card SetNewCardValues(InputCard newCard){
        Card card = new Card();
        string id = GenerateId();
        card.id = id;
        card.name = newCard.name;
        card.energy = newCard.energy;
        card.cost = newCard.cost;
        card.typeId = newCard.typeId;
        card.raceId = newCard.raceId;
        card.activatedCard = s_defaultActivationState;
        card.description = newCard.description;
        // card.imageId = 1;
        return card;
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

}


