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
    private RandomTools _randomTools = new RandomTools();
    private IdGenerator _idGenerator = new IdGenerator();



    public GameHandling(StarDeckContext context)
    {
        this._context = context;
        this._gameTableHandling = new GameTableHandling(_context);
        this._gamePlayerHandling = new GamePlayerHandling(_context);
        this._playerCRUD = new PlayerCRUD(_context);
    }


    public List<OutputPlanet> GetPlanets(string gameId)
    {
        return _gameTableHandling.GetGamePlanets(gameId);
    }


    // internal OutputCard DrawCard(string gameId, string playerId)
    // {
    //     return _gamePlayerHandling.DrawCard(gameId, playerId);
    // }

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
        StarAPI.Models.Game game = _context.Game.FirstOrDefault(g => g.id == gameId);
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