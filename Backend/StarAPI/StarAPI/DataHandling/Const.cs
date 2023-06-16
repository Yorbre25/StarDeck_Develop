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

    //Player
    public const int DefaultXP = 0;
    public const int DefaultRanking = 0;
    public const bool DefaultInGameState = false;
    public const bool DefaultActivationState = true;
    public const int DefaultCoins = 0;


    //Card
    public const int MinEnergyValue = -100;
    public const int MaxEnergyValue = 100;
    public const int MinBattleCost = 0;
    public const int MaxBattleCost = 100;

    //Deck
    public const int CardsPerDeck = 18;
    public const int MinDeckNameLength = 5;
    public const int MaxDeckNameLength = 30;

    //Planet
    public const string PopularPlanetType = "Popular";
    public const string BasicPlanetType = "Basico";
    public const string RarePlanetType = "Raro";
    public const int PlanetsPerGame = 3;
    
    //Game
    public const int MaxTurns = 5;
    public const int TimePerTurn = 20;
    public const int CardsPerPlanet = 5;
    public const int InitialCardPoints = 10;
    public const int ExtraCardPointsPerTurn = 2;
    public const int XpGain = 1;
    public const int EndTurnCounter = 2;
    public const int EndGameCounter = 2;
    public const string Tie = "Tie";
    
}