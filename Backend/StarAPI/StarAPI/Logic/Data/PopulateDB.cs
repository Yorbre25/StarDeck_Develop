using Microsoft.EntityFrameworkCore;
using StarAPI.Logic.ModelHandling;
using StarAPI.Context;
using StarAPI.Data;

namespace StarAPI.DataTesting;


public class PopulateDB
{
    private readonly StarDeckContext _context;
    private  RaceHandling _raceHandling;
    private CardTypeHandling _cardTypeHandling;
    private PlanetTypeHandling _planetTypeHandling;
    private CountryHandling _countryHandling;

    private DataForTest _dataForTest;

    public PopulateDB(StarDeckContext context)
    {
        this._context = context;
        this._dataForTest = new DataForTest();
        this._raceHandling = new RaceHandling(context);
        this._cardTypeHandling = new CardTypeHandling(context);
        this._planetTypeHandling = new PlanetTypeHandling(context);
        this._countryHandling = new CountryHandling(context);


    }


    public void Populate()
    {
        dropPreviousData();
        addRaces();
        addCardTypes();
        addPlanetTypes();
        addCountries();
    }

    private void dropPreviousData()
    {
        _context.Database.ExecuteSqlRaw("Delete Deck");
        _context.Database.ExecuteSqlRaw("Delete Card");
        _context.Database.ExecuteSqlRaw("Delete Player");
        _context.Database.ExecuteSqlRaw("Delete Planet");
        _context.Database.ExecuteSqlRaw("Delete CardType");
        _context.Database.ExecuteSqlRaw("Delete PlanetType");
        _context.Database.ExecuteSqlRaw("Delete Race");
        _context.Database.ExecuteSqlRaw("Delete Country");
    }

    public void addRaces()
    {
        foreach(var race in _dataForTest.races)
        {
            _raceHandling.AddRace(race);
        }
    }

    public void addCardTypes()
    {
        foreach(var cardType in _dataForTest.cardTypes)
        {
            _cardTypeHandling.AddCardType(cardType);
        }
    }

    public void addPlanetTypes()
    {
        foreach(var planetType in _dataForTest.planetTypes)
        {
            _planetTypeHandling.AddPlanetType(planetType);
        }
    }

    public void addCountries()
    {
        foreach(var country in _dataForTest.countries)
        {
            _countryHandling.AddCountry(country);
        }
        _context.SaveChanges();
    }
}