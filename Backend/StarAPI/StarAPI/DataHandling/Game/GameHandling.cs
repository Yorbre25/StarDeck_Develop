using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Models;
using StarAPI.DTO.Game;
using StarAPI.Logic.Utils;
using StarAPI.Logic.Mappers;

namespace StarAPI.DataHandling.Game;

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


    public OutputSetupValues SetUpGame(SetUpValues setupValues)
    {   
        string deckId1 = setupValues.player1DeckId;
        string deckId2 = setupValues.player2DeckId;
        string gameId = _idGenerator.GenerateId(s_idPrefix);
        _gameTableHandling.SetupTable(gameId);
        SetupPlayers(setupValues, gameId);
        
        StarAPI.Models.Game newGame = _gameMapper.FillNewGame(setupValues, gameId);
        AddGame(newGame);
        return _gameMapper.FillOutputSetupValues(newGame, deckId1, deckId2);
    }


    private void SetupPlayers(SetUpValues setupValues, string gameId)
    {
        string player1Id = setupValues.player1Id;
        string player1DeckId = setupValues.player1DeckId;
        _gamePlayerHandling.SetupPlayer(player1Id, player1DeckId, gameId);
        string player2Id = setupValues.player2Id;
        string player2DeckId = setupValues.player2DeckId;
        _gamePlayerHandling.SetupPlayer(player2Id, player2DeckId, gameId);
    }

    private void AddGame(StarAPI.Models.Game newGame)
    {
        _context.Game.Add(newGame);
        _context.SaveChanges();
    }

    private void DeleteGame(StarAPI.Models.Game game)
    {
        _context.Game.Remove(game);
    }


    public List<OutputPlanet> GetPlanets(string gameId)
    {
        List<OutputPlanet> gamePlanets = _gameTableHandling.GetGamePlanets(gameId);
        return gamePlanets;
    }


    public StarAPI.Models.Game GetGame(string gameId)
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

    private StarAPI.Models.Game GettingGame(string gameId)
    {
        StarAPI.Models.Game? game = _context.Game.FirstOrDefault(g => g.id == gameId);
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
        StarAPI.Models.Game? game;
        game = _context.Game.FirstOrDefault(c => c.id == id);
        if(game == null){
            return false;
        }
        return true;
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

    internal void EndingGame(string gameId)
    {
        StarAPI.Models.Game game = GetGame(gameId);

        DeleteGame(game);
        _gameTableHandling.EndGame(gameId);
        _gamePlayerHandling.EndGame(gameId);
        _context.SaveChanges();
    }

    internal void SetupHands(string gameId)
    {
        var game = GetGame(gameId);
        string player1Id = game.player1Id;
        string player2Id = game.player2Id;
        _gamePlayerHandling.SetupHand(gameId, player1Id);
        _gamePlayerHandling.SetupHand(gameId, player2Id);
    }

    internal List<OutputCard> GetHandCards(string gameId, string playerId)
    {
        return _gamePlayerHandling.GetHandCards(gameId, playerId);
    }

    internal OutputCard DrawCard(string gameId, string playerId)
    {
        return _gamePlayerHandling.DrawCard(gameId, playerId);
    }
}