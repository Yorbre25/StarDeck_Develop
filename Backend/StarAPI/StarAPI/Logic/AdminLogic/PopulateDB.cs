using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;

namespace StarAPI.Logic.AdminLogic;

/// <summary>
/// Class who creates, deletes and adds card types to the database
/// </summary>
public class PopulateDB
{
    private readonly StarDeckContext _context;
    private  RaceHandling _raceHandling;
    private CardTypeHandling _cardTypeHandling;

    private string[] _races;
    private string[] _cardTypes;

    public PopulateDB(StarDeckContext _context)
    {
        this._raceHandling = new RaceHandling(_context);
        this._cardTypeHandling = new CardTypeHandling(_context);

        _races = new string[]
        {
            "Human",
            "Trisolariano",
            "Robot",
            "Marciano",
            "Ciborg"
        };

        _cardTypes= new string[]
        {
            "Basica",
            "Normal",
            "Rara",
            "Muy Rara",
            "Ultra Rara"
        };
    }


    public void Populate()
    {
        // dropPreviousData();
        addRaces();
        addCardTypes();
    }

    private void dropPreviousData()
    {
        _context.Database.ExecuteSqlRaw("TRUNCATE TABLE Race");
        _context.Database.ExecuteSqlRaw("TRUNCATE TABLE Card_Type");
    }

    public void addRaces()
    {
        foreach(var race in _races)
        {
            _raceHandling.AddRace(race);
        }
    }

    public void addCardTypes()
    {
        foreach(var cardType in _cardTypes)
        {
            _cardTypeHandling.AddCardType(cardType);
        }
    }
}