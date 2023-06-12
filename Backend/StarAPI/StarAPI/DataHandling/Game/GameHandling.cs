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
    // internal OutputTableLayout GetLayout(object gameId, string playerId)
    // {
    //     string rivalId = GetRivalId(gameId, playerId);
    //     return _gameTableHandling.GetLayout(playerId, rivalId);
    // }

    public string GetRivalId(object gameId, string playerId)
    {
        StarAPI.Models.Game game = _context.Game.FirstOrDefault(g => g.id == gameId);
        string idRival = game.player1Id == playerId ? game.player2Id : game.player1Id;
        return idRival;
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

}