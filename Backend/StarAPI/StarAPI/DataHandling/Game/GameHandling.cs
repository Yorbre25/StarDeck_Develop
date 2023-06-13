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



    public GameHandling(StarDeckContext context)
    {
        this._context = context;
    }

    public string GetRivalId(object gameId, string playerId)
    {
        StarAPI.Models.Game game = _context.Game.FirstOrDefault(g => g.id == gameId);
        string idRival = game.player1Id == playerId ? game.player2Id : game.player1Id;
        return idRival;
    }

    // internal TurnInfo GetTurnInfo(string gameId, string playerId)
    // {
    //     Models.Game game = _context.Game.FirstOrDefault(g => g.id == gameId);
    //     TurnInfo turnInfo = new TurnInfo();
    //     string rivalId = GetRivalId(gameId, playerId);

    //     turnInfo.currentTurn = game.turn;   
    //     turnInfo.playerMaxCardPoints = _gamePlayerHandling.GetMaxCardPoints(gameId);
    //     turnInfo.playerPlanetPoints = _gameTableHandling.GetBattlePointsPerPlanet(playerId);
    //     turnInfo.rivalPlanetPoints = _gameTableHandling.GetBattlePointsPerPlanet(rivalId);

    //     return turnInfo;

    // }

    public string[] GetGamePlanets(string gameId)
    {
        GameTableHandling gameTableHandling = new GameTableHandling(_context);
        List<OutputPlanet> planets = gameTableHandling.GetGamePlanets(gameId);
        string[] planetsIds = new string[planets.Count];
        for (int i = 0; i < planets.Count; i++)
        {
            planetsIds[i] = planets[i].id;
        }
        return planetsIds;
    }

    public Dictionary<string, int> GetBattlePointsPerPlanet(string[] planetsIds, string playerId)
    {
        Dictionary<string, int> pointsPerPlanet = new Dictionary<string, int>();
        foreach (string planetId in planetsIds)
        {
            pointsPerPlanet.Add(planetId, 0);
        }

        List<GameTable> cards = _context.GameTable.Where(gt => gt.playerId == playerId).ToList();
        foreach (GameTable card in cards)
        {
            pointsPerPlanet[card.planetId] += card.battlePoints;
        }
        return pointsPerPlanet;
    }

}