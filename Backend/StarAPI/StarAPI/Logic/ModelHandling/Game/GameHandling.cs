using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
using StarAPI.DTOs;
using StarAPI.Logic.Utils;

namespace StarAPI.Logic.ModelHandling;

public class GameHandling
{
    private readonly StarDeckContext _context;
    private GameTableHandling _gameTableHandling;
    private GamePlayerHandling _gamePlayerHandling;
    private IdGenerator _idGenerator = new IdGenerator();

    private static int s_maxTurns = 10;
    private static int s_timePerTurn = 20;

    private static string s_idPrefix = "G";


    public GameHandling(StarDeckContext context)
    {
        this._context = context;
        this._gameTableHandling = new GameTableHandling(_context);
    }

    public Game SetUpGame(SetUpValues setUpValues)
    {
        try
        {
            return SettingupGame(setUpValues);
        }
        catch
        {
            throw new Exception("Error setting up game");
        }

    }

    public string SetTable()
    {
        string gameTableId = _gameTableHandling.SetupTable();
        return gameTableId;
    }
    public Game SettingupGame(SetUpValues setupValues)
    {
        string gameId = _idGenerator.GenerateId(s_idPrefix);
        AddPlayersToGame(gameId, setupValues);
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
        return newGame;
    }

    private void AddPlayersToGame(string gameId, SetUpValues setupValues)
    {
        this._gamePlayerHandling.AddPlayers(gameId, setupValues);
    }

    private void AddGame(Game newGame)
    {
        _context.Game.Add(newGame);
        _context.SaveChanges();
    }


    public List<OutputPlanet> GetPlanets(string gameId)
    {
        Game? game = GetGame(gameId);
        string gameTableId = game.gameTableId;
        return _gameTableHandling.GetGamePlanets(gameTableId);
    }

    public Game GetGame(string gameId)
    {
        try
        {
            return GettingGame(gameId);
        }
        catch (System.Exception)
        {
            throw new ArgumentException("Invalid game id");
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
}