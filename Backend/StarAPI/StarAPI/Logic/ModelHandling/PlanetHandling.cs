using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.DTOs;
using StarAPI.Logic.Utils;
using StarAPI.Context;

namespace StarAPI.Logic.ModelHandling;

public class PlanetHandling
{
    private readonly StarDeckContext _context;
    private static int s_minPlanetNameLenght = 5;
    private static int s_maxPlanetNameLenght = 30;
    private static int s_maxDescriptionLenght = 1000;
    private static bool s_defaultActivationState = true;
    private static string s_idPrefix = "P";

    private IdGenerator _idGenerator = new IdGenerator();
    private PlanetTypeHandling _planetTypeHandling;



    public PlanetHandling(StarDeckContext context)
    {
        this._context = context;
        this._planetTypeHandling = new PlanetTypeHandling(_context);
    }


    public List<OutputPlanet> GetAllPlanets()
    {
        try
        {
            return GettingAllPlanets();
        } 
        catch (System.Exception)
        {
            throw new Exception("Error getting planets");
        }
    }

    private List<OutputPlanet> GettingAllPlanets()
    {
        List<Planet> planets = _context.Planet.ToList();
        return PlanetsToOutputPlanets(planets);
    }

    private List<OutputPlanet> PlanetsToOutputPlanets(List<Planet> planets)
    {
        List<OutputPlanet> outputPlanets = new List<OutputPlanet>();
        foreach(var planet in planets)
        {
            try
            {
                outputPlanets.Add(PassPlanetValuesToOutputPlanet(planet));
                
            }
            catch (System.Exception)
            {
                continue;
            }
        }
        return outputPlanets;
    }

    private OutputPlanet PassPlanetValuesToOutputPlanet(Planet planet)
    {
        OutputPlanet outputCard = new OutputPlanet
        {
            id = planet.id,
            name = planet.name,
            type = _planetTypeHandling.GetPlanetType(planet.typeId),
            description = planet.description,
            image = "Hola"
        };
        return outputCard;
    }

    public void AddPlanet(InputPlanet inputPlanet)
    {
        bool isValid = CheckInputValues(inputPlanet);
        bool alreadyExist = NameAlreadyExists(inputPlanet.name);

        if(!isValid){
            throw new ArgumentException("Invalid Values");
        }
        if(alreadyExist){
            throw new ArgumentException("Planet name already exist");
        }
        InsertCard(inputPlanet);

    }

     private bool CheckInputValues(InputPlanet planet){
        bool isValid = true;
        if(planet.name.Length < s_minPlanetNameLenght || planet.name.Length > s_maxPlanetNameLenght){
            isValid = false;
        }
        else if(planet.description.Length > s_maxDescriptionLenght){
            isValid = false;
        }
        return isValid;
    }

    
    public void InsertCard(InputPlanet inputPlanet){
        var newPlanet = setNewPlanetValues(inputPlanet);
        _context.Planet.Add(newPlanet);
        _context.SaveChanges();
    }

    private bool NameAlreadyExists(string planetName)
    {
        var card = _context.Card.FirstOrDefault(r => r.name == planetName);
        if(card == null){
            return false;
        }
        return true;
    }

    private bool IdAlreadyExists(string id){
        Planet? planet = new Planet();
        planet = _context.Planet.FirstOrDefault(c => c.id == id);
        if(planet == null){
            return false;
        }
        return true;
    }

    private Planet setNewPlanetValues(InputPlanet newPlanet){
        Planet card = new Planet();
        string id = GenerateId();
        card.id = id;
        card.name = newPlanet.name;
        card.typeId = newPlanet.typeId;
        card.activatedPlanet = s_defaultActivationState;
        card.description = newPlanet.description;
        card.imageId = 1;
        return card;
    }

    
    private string GenerateId()
    {
        string id = "";
        bool alreadyExists = true;
        while (alreadyExists)
        {
            id = _idGenerator.GenerateId(s_idPrefix);
            alreadyExists = IdAlreadyExists(id);
        }
        return id;
    }

}