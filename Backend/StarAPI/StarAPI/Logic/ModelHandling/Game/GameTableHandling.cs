using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.GameLogic;
using StarAPI.Logic.Utils;
using StarAPI.DTOs;
using StarAPI.Logic.Mappers;

namespace StarAPI.Logic.ModelHandling;

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
    public string SetupTable()
    {
        string id = GenerateId();
        string[] planetsId = SetupPlanets();
        GameTable newGameTable = _gameTableMapper.FillNewGameTable(planetsId);
        newGameTable.id = id;
        AddGameTable(newGameTable);
        return id;
    }

    private void AddGameTable(GameTable newGameTable)
    {
        _context.GameTable.Add(newGameTable);
        _context.SaveChanges();
    }

    private string GenerateId()
    {
        string id = "";
        bool alreadyExists = true;
        while (alreadyExists)
        {
            id = _idGenerator.GenerateId(s_idPrefix);
            alreadyExists = IdAlreadyExists(id);
        }
        return id;
    }

    private bool IdAlreadyExists(string id)
    {
        GameTable? gameTable = new GameTable();
        gameTable = _context.GameTable.FirstOrDefault(c => c.id == id);
        if(gameTable == null){
            return false;
        }
        return true;
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

    public List<OutputPlanet> GetGamePlanets(string tableId)
    {
        try
        {
            return GettingGamePlanets(tableId);
        }
        catch
        {
            throw new Exception("Error getting planets");
        }
    }

    private List<OutputPlanet> GettingGamePlanets(string tableId)
    {
        string[] planetIds = new string[planetsPerGame];
        List<OutputPlanet> listPlanets = new List<OutputPlanet>();

        GameTable gameTable = GetGameTable(tableId);
        planetIds[0] = gameTable.planet1Id;
        planetIds[1] = gameTable.planet2Id;
        planetIds[2] = gameTable.planet3Id;
        
        listPlanets.Add(_planetHandling.GetPlanet(planetIds[0]));
        listPlanets.Add(_planetHandling.GetPlanet(planetIds[1]));
        listPlanets.Add(_planetHandling.GetPlanet(planetIds[2]));

        return listPlanets;
    }

    public GameTable GetGameTable(string tableId)
    {
        try
        {
            return GettingGameTable(tableId);
        }
        catch
        {
            throw new Exception("Error getting game table");
        }
    }

    private GameTable GettingGameTable(string tableId)
    {
        return _context.GameTable.FirstOrDefault(c => c.id == tableId);
    }

    private GameTable AddPlanetsToTable(string tableId, List<OutputPlanet> listPlanets)
    {
        string[] planetIds = new string[listPlanets.Count];
        for (int i = 0; i < listPlanets.Count; i++)
        {
            planetIds[i] = listPlanets[i].id;
        }
        GameTable gameTable = GetGameTable(tableId);
        gameTable.planet1Id = planetIds[0];
        gameTable.planet2Id = planetIds[1];
        gameTable.planet3Id = planetIds[2];
        _context.SaveChanges();
        return gameTable;
    }

    internal void Delete(string gameTableId)
    {
        GameTable gameTable = GetGameTable(gameTableId);
        _context.GameTable.Remove(gameTable);
    }
}