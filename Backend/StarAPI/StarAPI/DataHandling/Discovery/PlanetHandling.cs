using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Mappers;

namespace StarAPI.DataHandling.Discovery;

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
        List<Planet> planets = _context.Planet.ToList();
        return _planetMapper.FillOutputPlanet(planets);
    }


    public OutputPlanet GetPlanet(string id){
        Planet? planet = _context.Planet.FirstOrDefault(p => p.id == id);
        return _planetMapper.FillOutputPlanet(planet);
    }

    public List<OutputPlanet> GetPlanetsByType(string planetType)
    {
        List<OutputPlanet> allPlanets = GetAllPlanets();
        return allPlanets.Where(p => p.type == planetType).ToList();
    }


    public void AddPlanet(InputPlanet inputPlanet)
    {
        string id = GenerateId();
        var newPlanet = _planetMapper.FillNewPlanet(inputPlanet, id);
        _context.Planet.Add(newPlanet);
        _context.SaveChanges();
    }


    public bool NameAlreadyExists(string planetName)
    {
        var card = _context.Planet.FirstOrDefault(r => r.name == planetName);
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