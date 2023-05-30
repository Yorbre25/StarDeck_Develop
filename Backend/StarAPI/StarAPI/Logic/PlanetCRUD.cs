using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Mappers;
using StarAPI.Constants;
using StarAPI.DataHandling.Discovery;

namespace StarAPI.Logic;

public class PlanetCRUD
{
    private PlanetHandling _planetHandling;

    public PlanetCRUD(StarDeckContext context)
    {
        this._planetHandling = new PlanetHandling(context);
    }


    public List<OutputPlanet> GetAllPlanets()
    {
        try
        {
            return _planetHandling.GetAllPlanets();
        } 
        catch (System.Exception)
        {
            throw new Exception("Error getting planets");
        }
    }

    public OutputPlanet GetPlanet(string id){
        try
        {
            return _planetHandling.GetPlanet(id);
        }
        catch (System.Exception)
        {
            throw new Exception("Error getting planet");
        }
    }
    public List<OutputPlanet> GetPlanetsByType(string planetType)
    {
        try
        {
            return _planetHandling.GetPlanetsByType(planetType);
        }
        catch (System.Exception)
        {
            throw new Exception("Error getting planets by type");
        }
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
        _planetHandling.AddPlanet(inputPlanet);

    }

     private bool CheckInputValues(InputPlanet planet){
        bool isValid = true;
        if(planet.name.Length < Const.MinNameLenght || planet.name.Length > Const.MaxNameLenght){
            isValid = false;
        }
        else if(planet.description.Length > Const.MaxDescriptionLenght){
            isValid = false;
        }
        return isValid;
    }

    
    private bool NameAlreadyExists(string planetName)
    {
        return _planetHandling.NameAlreadyExists(planetName);
    }

    
}