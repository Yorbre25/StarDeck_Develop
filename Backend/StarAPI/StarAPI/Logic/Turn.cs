using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.Models;
using StarAPI.DataHandling.Game;

namespace StarAPI.Logic;

public class Turn
{

    private readonly StarDeckContext _context;
    GameHandling _gameHandling;


    public Turn(StarDeckContext context)
    {
        _context = context;
        _gameHandling = new GameHandling(_context);
    }

    internal TurnInfo GetTurnInfo(string gameId, string playerId)
    {
        try
        {
            return GetInfo(gameId, playerId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    private TurnInfo GetInfo(string gameId, string playerId)
    {
        Models.Game? game = _context.Game.FirstOrDefault(g => g.id == gameId);
        string rivalId = _gameHandling.GetRivalId(gameId, playerId);
        string[] planetsIds = _gameHandling.GetGamePlanets(gameId);

        TurnInfo turnInfo = new TurnInfo();
        
        turnInfo.currentTurn = game.turn;   
        turnInfo.playerMaxCardPoints = GetMaxCardPoints(gameId);
        turnInfo.playerPlanetPoints = _gameHandling.GetBattlePointsPerPlanet(planetsIds, playerId);
        turnInfo.rivalPlanetPoints = _gameHandling.GetBattlePointsPerPlanet(planetsIds,rivalId);

        return turnInfo;
    }

    internal int GetMaxCardPoints(string gameId)
    {
        Game_Player? gamePlayer = _context.Game_Player.FirstOrDefault(gp => gp.gameId == gameId);
        return gamePlayer.maxCardPoints;
    }
    
}