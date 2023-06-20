using StarAPI.Models;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Game;
using Contracts;

namespace StarAPI.Logic;

public class NewTable
{

    private readonly IRepositoryWrapper _repository;
    private PlanetsForGame _planetsForGame;
    public NewTable(IRepositoryWrapper repository)
    {
        _repository = repository;
    }

    internal void SetupTable(string gameId)
    {
        try
        {
            Setup(gameId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void Setup(string gameId)
    {
        _planetsForGame = new PlanetsForGame(_repository);
        List<OutputPlanet> planets = _planetsForGame.GetPlanetsForNewGame();
        List<Game_Planet> gamePlanetEntities = SetGamePlanetValues(gameId, planets);
        SetHiddenPlanet(gamePlanetEntities);
        // _repository.Game_Planet.AddRange(gamePlanetEntities);
        // _repository.SaveChanges();
        _repository.GamePlanet.Add(gamePlanetEntities);
        _repository.Save();

    }

    public List<Game_Planet> SetGamePlanetValues(string gameId, List<OutputPlanet> planets)
    {
        List<Game_Planet> gamePlanetEntities = new List<Game_Planet>();
        foreach(var planet in planets)
        {
            Game_Planet gamePlanet = new Game_Planet();
            gamePlanet.gameId = gameId;
            gamePlanet.planetId = planet.id;
            gamePlanet.show = true;
            gamePlanetEntities.Add(gamePlanet);
        }
        return gamePlanetEntities;
    }

    private void SetHiddenPlanet(List<Game_Planet> gamePlanetEntities)
    {
        int last = gamePlanetEntities.Count() - 1;
        Game_Planet lastPlanet = gamePlanetEntities[last];
        lastPlanet.show = false;
    }
}