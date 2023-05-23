using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Constants;

namespace StarAPI.Logic.Game;

public class PlanetsForGame
{
    private readonly StarDeckContext _context;
    private PlanetCRUD _planetCRUD;

    private RandomTools _randomTools = new RandomTools();

    public PlanetsForGame(StarDeckContext context)
    {
        this._context = context;
        this._planetCRUD = new PlanetCRUD(_context);
    }

    public List<OutputPlanet> GetPlanetsForNewGame()
    {
        List<OutputPlanet> planetsForNewGame;
        try
        {
            planetsForNewGame = GenerateRandomPlanets();
            return planetsForNewGame;
        }
        catch (System.Exception)
        {
            throw new Exception("Error getting planets for new game");
        }
    }

    public List<OutputPlanet> GenerateRandomPlanets()
    {
        List<OutputPlanet> popularPlanets = _planetCRUD.GetPlanetsByType(Const.PopularPlanetType);
        List<OutputPlanet> basicPlanets = _planetCRUD.GetPlanetsByType(Const.BasicPlanetType);
        List<OutputPlanet> rarePlanets = _planetCRUD.GetPlanetsByType(Const.RarePlanetType);
        List<OutputPlanet> outputPlanets = new List<OutputPlanet>();

        int numPlanets = popularPlanets.Count() + basicPlanets.Count() + rarePlanets.Count();  
        EnoughtPlanets(numPlanets);

        List<OutputPlanet> planetsToAdd = new List<OutputPlanet>();
        while(outputPlanets.Count() < Const.PlanetsPerGame)
        {
            Random rand = new Random();
            int number = rand.Next(0, 100);
            if(number <= 50)
            {
                planetsToAdd = popularPlanets;
            }
            else if(number <= 85)
            {
                planetsToAdd = basicPlanets;
            }
            else
            {
                planetsToAdd = rarePlanets;
            }
            outputPlanets = AddPlanet(outputPlanets, planetsToAdd);
        }
        return outputPlanets;
    }

    private List<OutputPlanet> AddPlanet(List<OutputPlanet> randomPlanets, List<OutputPlanet> planetsToAdd)
    {
        if(planetsToAdd.Count() > 0)
        {
            Random rand = new Random();
            int index = rand.Next(0, planetsToAdd.Count());
            randomPlanets.Add(planetsToAdd[index]);
            planetsToAdd.RemoveAt(index);
        }
        return randomPlanets;
    }

    private void EnoughtPlanets(int numPlanets)
    {
        if(numPlanets < Const.PlanetsPerGame)
        {
            throw new Exception("Not enough planets in database");
        }
    }
}

