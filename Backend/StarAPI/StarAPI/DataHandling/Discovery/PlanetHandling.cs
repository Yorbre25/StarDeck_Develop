using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Mappers;
using Contracts;

namespace StarAPI.DataHandling.Discovery;

public class PlanetHandling
{
    private readonly IRepositoryWrapper _repository;
    private PlanetMapper _planetMapper;
    private static int s_minPlanetNameLenght = 5;
    private static int s_maxPlanetNameLenght = 30;
    private static int s_maxDescriptionLenght = 1000;
    private static bool s_defaultActivationState = true;
    private static string s_idPrefix = "P";

    private IdGenerator _idGenerator = new IdGenerator();
    private PlanetTypeHandling _planetTypeHandling;



    public PlanetHandling(IRepositoryWrapper repository)
    {
        this._repository = repository;
        this._planetTypeHandling = new PlanetTypeHandling(repository);
        this._planetMapper = new PlanetMapper(repository);
    }


    public List<OutputPlanet> GetAllPlanets()
    {
        // List<Planet> planets = _repository.Planet.ToList();
        List<Planet> planets = _repository.Planet.GetAll();
        return _planetMapper.FillOutputPlanet(planets);
    }


    public OutputPlanet GetPlanet(string id){
        // Planet? planet = _repository.Planet.FirstOrDefault(p => p.id == id);
        Planet? planet = _repository.Planet.Get(id);
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
        // _repository.Planet.Add(newPlanet);
        // _repository.SaveChanges();
        _repository.Planet.Add(newPlanet);
        _repository.Save();
    }


    public bool NameAlreadyExists(string planetName)
    {
        // var planet = _repository.Planet.FirstOrDefault(r => r.name == planetName);
        var planets = _repository.Planet.GetAll();
        var planet = planets.Find(r => r.name == planetName);
        if(planet == null){
            return false;
        }
        return true;
    }

    private bool IdAlreadyExists(string id){
        // Planet? planet = new Planet();
        // planet = _repository.Planet.FirstOrDefault(c => c.id == id);
        Planet? planet = _repository.Planet.Get(id);
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