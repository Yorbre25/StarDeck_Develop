using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.Game;
using StarAPI.Logic.Utils;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Logic.Mappers;

namespace StarAPI.DataHandling.Game;

public class GameTableHandling
{
    private readonly StarDeckContext _context;
    private PlanetHandling _planetHandling;
    private PlanetsForGame _planetsForGame;
    private IdGenerator _idGenerator = new IdGenerator();
    private GameTableMapper _gameTableMapper;

    private static string s_idPrefix = "GT";
    private static int planetsPerGame = 3;



    public GameTableHandling(StarDeckContext context)
    {
        this._context = context;
        this._planetsForGame = new PlanetsForGame(_context);
        this._planetHandling = new PlanetHandling(_context);
        this._gameTableMapper = new GameTableMapper(_context);
    }

    //Creates a new game table for the game
    // and returns the id of the game table
    public void SetupTable(string gameId)
    {
        List<Game_Planet> planets = new List<Game_Planet>();
        string[] planetsId = SetupPlanets();
        for (int i = 0; i < planetsId.Length; i++)
        {
            Game_Planet planet = new Game_Planet();
            planet.gameId = gameId;
            planet.planetId = planetsId[i];
            planet.show = true;
        }
        SetHiddenPlanet(planets);
    }

    private void SetHiddenPlanet(List<Game_Planet> planets)
    {
        //get last planet
        Game_Planet lastPlanet = planets[planets.Count() - 1];
        lastPlanet.show = false;
    }

    // Generates planets for the game table
    // and returns the planets
    public string[] SetupPlanets()
    {
        List<OutputPlanet> listPlanets = _planetsForGame.GetPlanetsForNewGame();
        string[] planetIds = new string[listPlanets.Count];

        for (int i = 0; i < listPlanets.Count; i++)
        {
            planetIds[i] = listPlanets[i].id;
        }
        return planetIds;
    }

    public List<OutputPlanet> GetGamePlanets(string gameId)
    {
        try
        {
            return GettingGamePlanets(gameId);
        }
        catch
        {
            throw new Exception("Error getting planets");
        }
    }

    private List<OutputPlanet> GettingGamePlanets(string gameId)
    {
        string[] planetIds = _context.Game_Planet.Where(gp => gp.gameId == gameId).Select(gp => gp.planetId).ToArray();
        List<OutputPlanet> listPlanets = new List<OutputPlanet>();

        listPlanets.Add(_planetHandling.GetPlanet(planetIds[0]));
        listPlanets.Add(_planetHandling.GetPlanet(planetIds[1]));
        listPlanets.Add(_planetHandling.GetPlanet(planetIds[2]));

        return listPlanets;
    }

    internal void EndGame(string gameId)
    {
        List<Game_Planet> planets = _context.Game_Planet.Where(gt => gt.gameId == gameId).ToList();
        _context.Game_Planet.RemoveRange(planets);
    }
}