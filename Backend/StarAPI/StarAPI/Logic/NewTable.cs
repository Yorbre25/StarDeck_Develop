using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Mappers;
using StarAPI.Constants;
using StarAPI.DataHandling.Discovery;
using StarAPI.DTO.Game;
using StarAPI.Logic.Game;

namespace StarAPI.Logic;

public class NewTable
{

    private readonly StarDeckContext _context;
    private PlanetsForGame _planetsForGame;
    public NewTable(StarDeckContext context)
    {
        _context = context;
    }

    internal void SetupTable(string gameId)
    {
        try
        {
            Setup(gameId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void Setup(string gameId)
    {
        _planetsForGame = new PlanetsForGame(_context);
        List<OutputPlanet> planets = _planetsForGame.GetPlanetsForNewGame();
        List<Game_Planet> gamePlanetEntities = SetGamePlanetValues(gameId, planets);
        SetHiddenPlanet(gamePlanetEntities);
        _context.Game_Planet.AddRange(gamePlanetEntities);
        _context.SaveChanges();
    }

    private List<Game_Planet> SetGamePlanetValues(string gameId, List<OutputPlanet> planets)
    {
        List<Game_Planet> gamePlanetEntities = new List<Game_Planet>();
        foreach(var planet in planets)
        {
            Game_Planet gamePlanet = new Game_Planet();
            gamePlanet.gameId = gameId;
            gamePlanet.planetId = planet.id;
            gamePlanet.show = true;
            gamePlanetEntities.Add(gamePlanet);
        }
        return gamePlanetEntities;
    }

    private void SetHiddenPlanet(List<Game_Planet> gamePlanetEntities)
    {
        int last = gamePlanetEntities.Count() - 1;
        Game_Planet lastPlanet = gamePlanetEntities[last];
        lastPlanet.show = false;
    }
}