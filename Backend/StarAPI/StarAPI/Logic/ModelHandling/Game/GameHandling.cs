using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
using StarAPI.DTOs;
using StarAPI.Logic.Utils;
using StarAPI.Logic.Mappers;

namespace StarAPI.Logic.ModelHandling;

public class GameHandling
{
    private readonly StarDeckContext _context;
    private GameTableHandling _gameTableHandling;
    private GamePlayerHandling _gamePlayerHandling;
    private GameMapper _gameMapper;
    private RandomTools _randomTools = new RandomTools();
    private IdGenerator _idGenerator = new IdGenerator();

    private static int s_maxTurns = 10;
    private static int s_timePerTurn = 20;

    private static string s_idPrefix = "G";


    public GameHandling(StarDeckContext context)
    {
        this._context = context;
        this._gameTableHandling = new GameTableHandling(_context);
        this._gamePlayerHandling = new GamePlayerHandling(_context);
        this._gameMapper = new GameMapper(_context);
    }

    public OutputSetupValues SetUpGame(SetUpValues setUpValues)
    {
        try
        {
            return SettingupGame(setUpValues);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    public string SetTable()
    {
        string gameTableId = _gameTableHandling.SetupTable();
        return gameTableId;
    }
    public OutputSetupValues SettingupGame(SetUpValues setupValues)
    {   
        AddPlayersToGame(setupValues);
        string deckId1 = setupValues.player1DeckId;
        string deckId2 = setupValues.player2DeckId;
        string gameId = _idGenerator.GenerateId(s_idPrefix);
        
        Game newGame = new Game();
        newGame.id = gameId;
        newGame.timeStarted = DateTime.Now;
        newGame.player1Id = setupValues.player1Id;
        newGame.player2Id = setupValues.player2Id;
        newGame.maxTurns = 10;
        newGame.timePerTurn = 10;
        newGame.turn = 0;
        newGame.gameTableId = SetTable();
        AddGame(newGame);
        return _gameMapper.FillOutputSetupValues(newGame, deckId1, deckId2);
    }


    private string[] AddPlayersToGame(SetUpValues setupValues)
    {
        return this._gamePlayerHandling.AddPlayers(setupValues);
    }

    private void AddGame(Game newGame)
    {
        _context.Game.Add(newGame);
        _context.SaveChanges();
    }

    private void DeleteGame(Game game)
    {
        _context.Game.Remove(game);
    }


    public List<OutputPlanet> GetPlanets(string gameId)
    {
        Game? game = GetGame(gameId);
        string gameTableId = game.gameTableId;
        List<OutputPlanet> gamePlanets = _gameTableHandling.GetGamePlanets(gameTableId);
        //*Provitional
        return SetHiddenPlanet(gamePlanets);
    }

    //*provitional function
    private List<OutputPlanet> SetHiddenPlanet(List<OutputPlanet> planetsForNewGame)
    {
        _randomTools.GetRandomElement<OutputPlanet>(planetsForNewGame).show = true;
        return planetsForNewGame;
    }

    public Game GetGame(string gameId)
    {
        try
        {
            return GettingGame(gameId);
        }
        catch (System.Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    private Game GettingGame(string gameId)
    {
        Game? game = _context.Game.FirstOrDefault(g => g.id == gameId);
        return game;
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

    
    private bool IdAlreadyExists(string id){
        Game? game;
        game = _context.Game.FirstOrDefault(c => c.id == id);
        if(game == null){
            return false;
        }
        return true;
    }

    internal GameTable GetGameTable(string gameTableId)
    {
        return _gameTableHandling.GetGameTable(gameTableId);
    }

    internal void EndingGame(string gameId)
    {
        Game game = GetGame(gameId);
        DeleteGame(game);
        _gameTableHandling.Delete(game.gameTableId);
        // string[] gamePlayerIds = new string[2];
        // gamePlayerIds[0] = game.player1Id;
        // gamePlayerIds[1] = game.player2Id;
        // _gamePlayerHandling.EndGame(gamePlayerIds);
        _context.SaveChanges();
    }

    public void EndGame(string gameId)
    {
        try
        {
            EndingGame(gameId);
        }
        catch (System.Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    internal List<OutputCard> SetupHand(string gameId, string playerId)
    {
        return _gamePlayerHandling.SetupHand(gameId, playerId);
    }
}