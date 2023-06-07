using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Models;
using StarAPI.DTO.Game;
using StarAPI.Logic.Utils;
using StarAPI.Logic.Mappers;
using StarAPI.Logic;
using StarAPI.Constants;

namespace StarAPI.DataHandling.Game;

public class GameHandling
{
    private readonly StarDeckContext _context;
    private GameTableHandling _gameTableHandling;
    private GamePlayerHandling _gamePlayerHandling;
    private PlayerCRUD _playerCRUD;
    private GameMapper _gameMapper;
    private RandomTools _randomTools = new RandomTools();
    private IdGenerator _idGenerator = new IdGenerator();

    private static string s_idPrefix = "G";


    public GameHandling(StarDeckContext context)
    {
        this._context = context;
        this._gameTableHandling = new GameTableHandling(_context);
        this._gamePlayerHandling = new GamePlayerHandling(_context);
        this._playerCRUD = new PlayerCRUD(_context);
        this._gameMapper = new GameMapper(_context);
    }

    public OutputSetupValues SetUpGame(SetupValues setupValues)
    {   
        string deckId1 = setupValues.player1DeckId;
        string deckId2 = setupValues.player2DeckId;
        string gameId = GenerateId();
        _gameTableHandling.SetupTable(gameId);
        SetupPlayers(setupValues, gameId);
        
        StarAPI.Models.Game newGame = _gameMapper.FillNewGame(setupValues, gameId);
        AddGame(newGame);
        return _gameMapper.FillOutputSetupValues(newGame, deckId1, deckId2);
    }


    private void SetupPlayers(SetupValues setupValues, string gameId)
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

    public List<OutputPlanet> GetPlanets(string gameId)
    {
        return _gameTableHandling.GetGamePlanets(gameId);
    }

    private void DeleteGame(StarAPI.Models.Game game)
    {
        _context.Game.Remove(game);
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



    public WinnerInfo EndGame(string gameId)
    {
        StarAPI.Models.Game game = _context.Game.FirstOrDefault(g => g.id == gameId);
        string winnerId = DeclareWinner(game.player1Id, game.player2Id);
        _playerCRUD.IncreaseWins(winnerId, game.xpGain);


        //Delete
        _context.Game.Remove(game);
        _gameTableHandling.EndGame(gameId);
        _gamePlayerHandling.EndGame(gameId);
        _context.SaveChanges();

        WinnerInfo winnerInfo = new WinnerInfo();
        winnerInfo.winnerId = winnerId;
        winnerInfo.xpGain = game.xpGain;
        return winnerInfo; 
    }

    private string DeclareWinner(string player1Id, string player2Id)
    {
       return _gameTableHandling.DeclareWinner(player1Id, player2Id);
    }

    internal void SetupHands(string gameId)
    {
        var game = _context.Game.FirstOrDefault(g => g.id == gameId);
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

    internal void EndTurn(InputTableLayout tableLayout)
    {
        string gameId = tableLayout.gameId;
        string[] playerIds = GetPlayersIds(gameId);
        Models.Game game = _context.Game.FirstOrDefault(g => g.id == gameId);
        game.turn++;
        _gamePlayerHandling.IncreaseCardPoints(playerIds[0]);
        _gamePlayerHandling.IncreaseCardPoints(playerIds[1]);
        _context.SaveChanges();

    }

    internal OutputTableLayout GetLayout(object gameId, string playerId)
    {
        string rivalId = GetRivalId(gameId, playerId);
        return _gameTableHandling.GetLayout(playerId, rivalId);
    }

    private string[] GetPlayersIds(string gameId)
    {
        string[] playersIds = new string[2];
        StarAPI.Models.Game game = _context.Game.FirstOrDefault(g => g.id == gameId);
        playersIds[0] = game.player1Id;
        playersIds[1] = game.player2Id;
        return playersIds;
    }
    private string GetRivalId(object gameId, string playerId)
    {
        string[] playersIds = new string[2];
        StarAPI.Models.Game game = _context.Game.FirstOrDefault(g => g.id == gameId.ToString());
        string rivalId = playersIds.FirstOrDefault(p => p != playerId);
        return rivalId;
    }

    internal TurnInfo GetTurnInfo(string gameId, string playerId)
    {
        Models.Game game = _context.Game.FirstOrDefault(g => g.id == gameId);
        TurnInfo turnInfo = new TurnInfo();
        string rivalId = GetRivalId(gameId, playerId);

        turnInfo.currentTurn = game.turn;   
        turnInfo.playerMaxCardPoints = _gamePlayerHandling.GetMaxCardPoints(gameId);
        turnInfo.playerPlanetPoints = _gameTableHandling.GetBattlePointsPerPlanet(playerId);
        turnInfo.rivalPlanetPoints = _gameTableHandling.GetBattlePointsPerPlanet(rivalId);

        return turnInfo;

    }

    internal int GetEndTurnCounter(string gameId)
    {
        Models.Game game = _context.Game.FirstOrDefault(g => g.id == gameId);
        return game.endTurnCounter;
    }

    internal void ResetEndTurnCounter(string gameId)
    {
        Models.Game game = _context.Game.FirstOrDefault(g => g.id == gameId);
        game.endTurnCounter = Const.EndTurnCounter;
        _context.SaveChanges();
    }

    internal void DecreaseEndTurnCounter(string gameId)
    {
        Models.Game game = _context.Game.FirstOrDefault(g => g.id == gameId);
        game.endTurnCounter--;
        _context.SaveChanges();
    }
}