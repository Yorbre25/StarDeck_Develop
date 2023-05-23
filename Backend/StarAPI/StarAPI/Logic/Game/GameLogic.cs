using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Models;
using StarAPI.DataHandling.Game;
using StarAPI.DTO.Game;

namespace StarAPI.Logic.Game;

public class GameLogic
{
    private readonly StarDeckContext _context;

    private GameHandling _gameHandling;


    public GameLogic(StarDeckContext context)
    {
        _gameHandling = new GameHandling(context);
    }

    public OutputSetupValues SetUpGame(SetupValues setUpValues)
    {
        try
        {
            return _gameHandling.SetUpGame(setUpValues);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    // public List<OutputPlanet> GetPlanets(string gameId)
    // {
    //     try
    //     {
    //         return _gameHandling.GetPlanets(gameId);
    //     }
    //     catch (Exception e)
    //     {
    //         throw new Exception(e.Message);
    //     }
    // }


    internal void SetupHands(string gameId)
    {
        try
        {
            _gameHandling.SetupHands(gameId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    internal List<OutputCard> GetHandCards(string gameId, string playerId)
    {
        try
        {
            return _gameHandling.GetHandCards(gameId, playerId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    internal OutputCard DrawCard(string gameId, string playerId)
    {
        try
        {
            return _gameHandling.DrawCard(gameId, playerId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    // internal void PlaceCard(InputPlaceCard inputPlaceCard)
    // {
    //     try
    //     {
    //         _gameHandling.PlaceCard(inputPlaceCard);
    //     }
    //     catch (Exception e)
    //     {
    //         throw new Exception(e.Message);
    //     }
    // }
    public void EndGame(string gameId)
    {
        try
        {
            _gameHandling.EndGame(gameId);
        }
        catch (System.Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }
}