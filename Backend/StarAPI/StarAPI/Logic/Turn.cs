using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.Models;
using StarAPI.DataHandling.Game;
using Contracts;

namespace StarAPI.Logic;

public class Turn
{

    private readonly IRepositoryWrapper _repository;
    GameHandling _gameHandling;


    public Turn(IRepositoryWrapper repository)
    {
        _repository = repository;
        _gameHandling = new GameHandling(_repository);
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
        // Models.Game? game = _repository.Game.FirstOrDefault(g => g.id == gameId);
        Models.Game game = GetGame(gameId);
        string rivalId = _gameHandling.GetRivalId(gameId, playerId);
        string[] planetsIds = _gameHandling.GetGamePlanets(gameId);

        TurnInfo turnInfo = new TurnInfo();
        
        turnInfo.currentTurn = game.turn;   
        turnInfo.playerMaxCardPoints = GetMaxCardPoints(gameId);
        turnInfo.playerPlanetPoints = _gameHandling.GetBattlePointsPerPlanet(planetsIds, playerId);
        turnInfo.rivalPlanetPoints = _gameHandling.GetBattlePointsPerPlanet(planetsIds,rivalId);

        return turnInfo;
    }

    private Models.Game GetGame(string gameId)
    {
        GameHandling gameHandling = new GameHandling(_repository);
        return gameHandling.GetGame(gameId);
    }

    internal int GetMaxCardPoints(string gameId)
    {
        // Game_Player? gamePlayer = _repository.Game_Player.FirstOrDefault(gp => gp.gameId == gameId);
        Game_Player gamePlayer = GetGamePlayer(gameId);
        return gamePlayer.maxCardPoints;
    }

    private Game_Player GetGamePlayer(string gameId)
    {
        GamePlayerHandling gamePlayerHandling = new GamePlayerHandling(_repository);
        return gamePlayerHandling.GetGamePlayerByGameId(gameId);
    }
}