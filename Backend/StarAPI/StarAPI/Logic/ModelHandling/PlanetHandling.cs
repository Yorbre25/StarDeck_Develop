using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.DTOs;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.Logic.Mappers;

namespace StarAPI.Logic.ModelHandling;

public class PlanetHandling
{
    private readonly StarDeckContext _context;
    private PlanetMapper _planetMapper;
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
        this._planetMapper = new PlanetMapper(_context);
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

    public OutputPlanet GetPlanet(string id){
        try
        {
            return GetOutputPlanet(id);
        }
        catch (System.Exception)
        {
            throw new Exception("Error getting planet");
        }
    }

    public List<OutputPlanet> GetPlanets(string[] ids)
    {
        try
        {
            return GettingPlanets(ids);
        }
        catch (System.Exception)
        {
            throw new Exception("Error getting planets");
        }
    }

    private List<OutputPlanet> GettingPlanets(string[] ids)
    {
        List<OutputPlanet> planets = new List<OutputPlanet>();
        foreach(var id in ids)
        {
            planets.Add(GetPlanet(id));
        }
        return planets;
    }

    private OutputPlanet GetOutputPlanet(string id)
    {
        Planet? planet = _context.Planet.FirstOrDefault(p => p.id == id);
        return _planetMapper.FillOutputPlanet(planet);
    }

    private List<OutputPlanet> GettingAllPlanets()
    {
        List<Planet> planets = _context.Planet.ToList();
        return _planetMapper.FillOutputPlanet(planets);
    }

    public List<OutputPlanet> GetPlanetsByType(string planetType)
    {
        try
        {
            return GettingPlanetsByType(planetType);
        }
        catch (System.Exception)
        {
            throw new Exception("Error getting planets by type");
        }
    }

    public List<OutputPlanet> GettingPlanetsByType(string planetType)
    {
        List<OutputPlanet> allPlanets = GetAllPlanets();
        return allPlanets.Where(p => p.type == planetType).ToList();
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

    
    public void InsertCard(InputPlanet inputPlanet)
    {
        string id = _idGenerator.GenerateId(s_idPrefix);
        var newPlanet = _planetMapper.FillNewPlanet(inputPlanet, id);
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