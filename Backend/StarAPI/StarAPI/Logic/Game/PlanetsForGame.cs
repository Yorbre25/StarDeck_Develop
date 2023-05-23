using StarAPI.Models;
using StarAPI.Context;
using StarAPI.DataHandling.Discovery;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Utils;

namespace StarAPI.Logic.Game;

public class PlanetsForGame
{
    private readonly StarDeckContext _context;
    private PlanetHandling _planetHandler;

    private RandomTools _randomTools = new RandomTools();

    private static string s_popularPlanetType = "Popular";
    private static string s_basicPlanetType = "Basico";
    private static string s_rarePlanetType = "Raro";
    private static int s_numberOfPlanets = 3;



    public PlanetsForGame(StarDeckContext context)
    {
        this._context = context;
        this._planetHandler = new PlanetHandling(_context);
    }

    //Top function: try and catch
    public List<OutputPlanet> GetPlanetsForNewGame()
    {
        List<OutputPlanet> planetsForNewGame;
        try
        {
            planetsForNewGame = GenerateRandomPlanets();
            return planetsForNewGame;
            // return SetHiddenPlanet(planetsForNewGame);
        }
        catch (System.Exception)
        {
            throw new Exception("Error getting planets for new game");
        }
    }

    public List<OutputPlanet> GenerateRandomPlanets()
    {
        List<OutputPlanet> popularPlanets = _planetHandler.GetPlanetsByType(s_popularPlanetType);
        List<OutputPlanet> basicPlanets = _planetHandler.GetPlanetsByType(s_basicPlanetType);
        List<OutputPlanet> rarePlanets = _planetHandler.GetPlanetsByType(s_rarePlanetType);
        List<OutputPlanet> outputPlanets = new List<OutputPlanet>();

        int numPlanets = popularPlanets.Count() + basicPlanets.Count() + rarePlanets.Count();  
        EnoughtPlanets(numPlanets);

        List<OutputPlanet> planetsToAdd = new List<OutputPlanet>();
        while(outputPlanets.Count() < s_numberOfPlanets)
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
        if(numPlanets < s_numberOfPlanets)
        {
            throw new Exception("Not enough planets in database");
        }
    }
}

