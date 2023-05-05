using Microsoft.EntityFrameworkCore;
using StarAPI.Logic.ModelHandling;
using StarAPI.Data;
using StarAPI.Context;

namespace StarAPI.DataTesting;


public class AddPlaceholderData
{
    private readonly StarDeckContext _context;
    private  CardHandling _cardHandling;
    private PlanetHandling _planetHandling;
    private DataForTest _dataForTest;
    private PlayerHandling _playerHandling;

    private Random random = new Random();
    

    public AddPlaceholderData(StarDeckContext context)
    {
        this._context = context;
        this._dataForTest = new DataForTest();
        this._cardHandling = new CardHandling(context);
        this._planetHandling = new PlanetHandling(context);
        this._playerHandling = new PlayerHandling(context);
    }


    public void AddData()
    {
        DropPreviousData();
        AddCards();
        AddPlanets();
        AddPlayers();
    }

    private void DropPreviousData()
    {
        _context.Database.ExecuteSqlRaw("TRUNCATE TABLE Card");
        _context.Database.ExecuteSqlRaw("TRUNCATE TABLE Planet");
        _context.Database.ExecuteSqlRaw("TRUNCATE TABLE Player");
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

    
}








