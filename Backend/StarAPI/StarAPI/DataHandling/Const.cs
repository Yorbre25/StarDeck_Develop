using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.Utils;
using StarAPI.DataHandling.Discovery;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;

namespace StarAPI.Constants;

public class Const
{
    //Hand
    public const int IntialCardsPerHand = 7;
    public const int MaxHandSize = 9;

    //General
    public const int MinNameLenght = 5;
    public const int MaxNameLenght = 30;
    public const int MaxDescriptionLenght = 1000;

    //Card
    public const int MinEnergyValue = -100;
    public const int MaxEnergyValue = 100;
    public const int MinBattleCost = 0;
    public const int MaxBattleCost = 100;

    //Planet
    public const string PopularPlanetType = "Popular";
    public const string BasicPlanetType = "Basico";
    public const string RarePlanetType = "Raro";
    public const int PlanetsPerGame = 3;
    
    //Game
    public const int CardsPerPlanet = 5;
    public const int InitialCardPoints = 5;
    

    
}