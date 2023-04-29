using Microsoft.EntityFrameworkCore;
using StarAPI.Models;

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

    /// <summary>
    /// Contructor of PopulateDB
    /// </summary>
    /// <param name="_context"></param>
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

    /// <summary>
    /// Adds necesary data to the database
    /// </summary>
    /// <returns>A list of Card_Type</returns>
    public void Populate()
    {
        addRaces();
        addCardTypes();
    }

    /// <summary>
    /// Adds races to the database
    /// </summary>
    public void addRaces()
    {
        foreach(var race in _races)
        {
            _raceHandling.AddRace(race);
        }
    }

    /// <summary>
    /// Adds card types to the database
    /// </summary>
    public void addCardTypes()
    {
        foreach(var cardType in _cardTypes)
        {
            _cardTypeHandling.AddCardType(cardType);
        }
    }
}