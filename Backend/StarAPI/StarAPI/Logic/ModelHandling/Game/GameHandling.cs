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

    public string SetupTable()
    {
        string gameTableId = _gameTableHandling.SetupTable();
        return gameTableId;
    }
    public OutputSetupValues SettingupGame(SetUpValues setupValues)
    {   
        string deckId1 = setupValues.player1DeckId;
        string deckId2 = setupValues.player2DeckId;
        string gameId = _idGenerator.GenerateId(s_idPrefix);
        string tableId = SetupTable();
        AddPlayersToGame(setupValues);
        
        Game newGame = _gameMapper.FillNewGame(setupValues, gameId, tableId);
        AddGame(newGame);
        return _gameMapper.FillOutputSetupValues(newGame, deckId1, deckId2);
    }


    private void AddPlayersToGame(SetUpValues setupValues)
    {
        string player1Id = setupValues.player1Id;
        string player1DeckId = setupValues.player1DeckId;
        _gamePlayerHandling.AddPlayer(player1Id, player1DeckId);
        string player2Id = setupValues.player2Id;
        string player2DeckId = setupValues.player2DeckId;
        _gamePlayerHandling.AddPlayer(player2Id, player2DeckId);
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



    public void EndGame(string gameId)
    // public List<Hand_Card> EndGame(string gameId)
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

    internal void EndingGame(string gameId)
    // internal List<Hand_Card> EndingGame(string gameId)
    {
        Game game = GetGame(gameId);
        string gameTableId = game.gameTableId;
        string player1Id = game.player1Id;
        string player2Id = game.player2Id;

        DeleteGame(game);
        _gameTableHandling.Delete(gameTableId);
        _gamePlayerHandling.Delete(player1Id);
        _gamePlayerHandling.Delete(player2Id);
        _context.SaveChanges();
    }

    internal void SetupHands(string gameId)
    {
        var game = GetGame(gameId);
        string player1Id = game.player1Id;
        string player2Id = game.player2Id;
        _gamePlayerHandling.SetupHands(gameId, player1Id);
        _gamePlayerHandling.SetupHands(gameId, player2Id);
    }

    internal List<OutputCard> GetHandCards(string gameId, string playerId)
    {
        return _gamePlayerHandling.GetHandCards(gameId, playerId);
    }
}