using StarAPI.DTO.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Constants;
using Contracts;

namespace StarAPI.Logic.Game;

public class PlanetsForGame
{
    public List<OutputPlanet> popularPlanets;
    public List<OutputPlanet> basicPlanets;
    public List<OutputPlanet> rarePlanets;
    private List<OutputPlanet> outputPlanets = new List<OutputPlanet>();
    private int _popularPlanetChance = Const.PopularPlanetChance;
    private int _BasicPlanetChance = Const.PopularPlanetChance + Const.BasicPlanetChance;

    


    private PlanetCRUD _planetCRUD;

    private RandomTools _randomTools = new RandomTools();

    public PlanetsForGame(IRepositoryWrapper repository)
    {
        this._planetCRUD = new PlanetCRUD(repository);
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
        GetAvaiblePlanets();
        int numPlanets = popularPlanets.Count() + basicPlanets.Count() + rarePlanets.Count();  
        if (EnoughtPlanets(numPlanets))
        {
            outputPlanets = GeneratePlanets();
        }
        else
        {
            throw new Exception("Not enought planets");
        }
        return outputPlanets;
    }

    public void GetAvaiblePlanets()
    {
        popularPlanets = _planetCRUD.GetPlanetsByType(Const.PopularPlanetType);
        basicPlanets = _planetCRUD.GetPlanetsByType(Const.BasicPlanetType);
        rarePlanets = _planetCRUD.GetPlanetsByType(Const.RarePlanetType);
    }

    public List<OutputPlanet> GeneratePlanets()
    {
        while(outputPlanets.Count() < Const.PlanetsPerGame)
        {
            List<OutputPlanet> planetsToAdd = new List<OutputPlanet>();
            Random rand = new Random();
            int number = rand.Next(0, 100);
            if(number <= _popularPlanetChance)
            {
                planetsToAdd = popularPlanets;
            }
            else if(number <= _BasicPlanetChance)
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

    public bool EnoughtPlanets(int numPlanets)
    {
        bool enoughtPlanets = false;
        if(numPlanets > Const.PlanetsPerGame)
        {
            enoughtPlanets = true;
        }
        return enoughtPlanets;
    }
}

