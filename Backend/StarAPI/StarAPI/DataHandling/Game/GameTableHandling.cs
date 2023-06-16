using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.Game;
using StarAPI.Logic.Utils;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Logic.Mappers;
using StarAPI.Logic;
using StarAPI.DTO.Game;
using StarAPI.Constants;
using Contracts;

namespace StarAPI.DataHandling.Game;

public class GameTableHandling
{
    private readonly IRepositoryWrapper _repository;
    private PlanetCRUD _planetCRUD;




    public GameTableHandling(IRepositoryWrapper repository)
    {
        this._repository = repository;
        this._planetCRUD = new PlanetCRUD(_repository);
    }

    public List<OutputPlanet> GetGamePlanets(string gameId)
    {
        try
        {
            return GetPlanets(gameId);
        }
        catch
        {
            throw new Exception("Error getting planets");
        }
    }


    public List<OutputPlanet> GetPlanets(string gameId)
    {
        // List<Game_Planet> gamePlanets = _repository.Game_Planet.Where(gp => gp.gameId == gameId).ToList();
        List<Game_Planet> gamePlanets = GetGamePlanetsByGameId(gameId);
        List<OutputPlanet> listPlanets = new List<OutputPlanet>();

        foreach (Game_Planet gamePlanet in gamePlanets)
        {
            OutputPlanet planet = _planetCRUD.GetPlanet(gamePlanet.planetId);
            planet.show = gamePlanet.show;
            listPlanets.Add(planet);
        }
        return listPlanets;
    }





    public List<GameTable> GetPlayerCardsInTable(string playerId)
    {
        List<GameTable> cards = _repository.GameTable.GetAll();
        List<GameTable> playerCards = cards.FindAll(c => c.playerId == playerId);
        return playerCards;

    }      

    public List<Game_Planet> GetGamePlanetsByGameId(string gameId)   
    {
        // List<Game_Planet> gamePlanets = _repository.Game_Planet.Where(gp => gp.gameId == gameId).ToList();
        List<Game_Planet> gamePlanets = _repository.GamePlanet.GetAll();
        return gamePlanets.FindAll(gp => gp.gameId == gameId);
    }
    


    




}
