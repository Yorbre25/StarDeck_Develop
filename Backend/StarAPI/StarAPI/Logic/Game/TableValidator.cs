using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.Game;
using StarAPI.Logic.Utils;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Logic.Mappers;
using StarAPI.Logic;
using StarAPI.DTO.Game;
using StarAPI.Constants;


namespace StarAPI.DataHandling.Game;

public class TableValidator
{
    private GameTableHandling _gameTableHandling;

    public TableValidator(StarDeckContext context)
    {
        this._gameTableHandling = new GameTableHandling(context);
    }


    public List<OutputPlanet> GetGamePlanets(string gameId)
    {
        try
        {
            return _gameTableHandling.GetGamePlanets(gameId);
        }
        catch
        {
            throw new Exception("Error getting planets");
        }
    }
}