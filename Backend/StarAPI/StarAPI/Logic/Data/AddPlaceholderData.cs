using Microsoft.EntityFrameworkCore;
using StarAPI.Logic.ModelHandling;
using StarAPI.Data;
using StarAPI.Context;

namespace StarAPI.DataTesting;


public class AddPlaceholderData
{
    private readonly StarDeckContext _context;
    private CardHandling _cardHandling;
    private PlanetTypeHandling _planetTypeHandling;
    private PlanetHandling _planetHandling;
    private DataForTest _dataForTest;
    private PlayerHandling _playerHandling;

    private DeckHandling _deckHandling;

    private Random random = new Random();
    

    public AddPlaceholderData(StarDeckContext context)
    {
        this._context = context;
        this._dataForTest = new DataForTest();
        this._cardHandling = new CardHandling(context);
        this._planetHandling = new PlanetHandling(context);
        this._deckHandling = new DeckHandling(context);
        this._playerHandling = new PlayerHandling(context);
    }


    public void AddData()
    {
        DropPreviousData();
        AddCards();
        AddPlanets();
        AddPlayers();
        AddDecks();
    }

    private void DropPreviousData()
    {
        _context.Database.ExecuteSqlRaw("Delete Deck");
        _context.Database.ExecuteSqlRaw("Delete Card");
        _context.Database.ExecuteSqlRaw("Delete Planet");
        _context.Database.ExecuteSqlRaw("Delete Player");
    }

    private void AddCards()
    {   
        var cards = _dataForTest.basicCards.Concat(_dataForTest.advancedCards);
        foreach(var card in cards)
        {
            _cardHandling.AddCard(card);
        }
    }

    private void AddPlanets()
    {
        foreach(var planet in _dataForTest.planets)
        {
            _planetHandling.AddPlanet(planet);
        }
    }

    private void AddPlayers()
    {
        foreach(var player in _dataForTest.players)
        {
            _playerHandling.AddPlayer(player);
        }
        setPlayersRanking();
    }

    private void setPlayersRanking()
    {
        string [] ids = _playerHandling.GetAllPlayersIds().ToArray();
        int randomNumber;
        foreach(var id in ids)
        {
            randomNumber = random.Next(1, 501);
            _playerHandling.SetPlayerRanking(id, randomNumber);
        }
    }
    public void AddDecks()
    {
        string [] ids = _playerHandling.GetAllPlayersIds().ToArray();
        var decks = _dataForTest.decks;
        for(int i = 0; i < decks.Length; i++)
        {
            decks[i].playerId = ids[i];
            decks[i].cardIds = GenerateCardsForDeck();
            // Console.WriteLine(decks[i].cardIds);
            _deckHandling.AddDeck(decks[i]);
        }
    }

    public string[] GenerateCardsForDeck()
    {
        string [] cardIds = new string[18];
        var allCards = _cardHandling.GetAllCards();

        for(int i = 0; i < 18; i++)
        {
            cardIds[i] = allCards[i].id;
        }   
        return cardIds;
    }

    
}








