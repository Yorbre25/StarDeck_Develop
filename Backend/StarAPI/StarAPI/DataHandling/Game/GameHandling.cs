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
using Contracts;

namespace StarAPI.DataHandling.Game;

public class GameHandling
{
    private readonly IRepositoryWrapper _repository;



    public GameHandling(IRepositoryWrapper repository)
    {
        this._repository = repository;
    }

    public string GetRivalId(string gameId, string playerId)
    {
        Models.Game game = GetGame(gameId);
        string idRival = game.player1Id == playerId ? game.player2Id : game.player1Id;
        return idRival;
    }
    
    public string[] GetGamePlanets(string gameId)
    {
        GameBoardHandling gameTableHandling = new GameBoardHandling(_repository);
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

        List<GameTable> cards = GetPlayerCardsInTable(playerId);
        foreach (GameTable card in cards)
        {
            pointsPerPlanet[card.planetId] += card.battlePoints;
        }
        return pointsPerPlanet;
    }

    private List<GameTable> GetPlayerCardsInTable(string playerId)
    {
        GameBoardHandling gameTableHandling = new GameBoardHandling(_repository);
        return gameTableHandling.GetPlayerCardsInTable(playerId);
    }

    public Models.Game GetGame(string gameId)
    {
        List<Models.Game> games = _repository.Game.GetAll();
        return games.FirstOrDefault(g => g.id == gameId);
    }

}