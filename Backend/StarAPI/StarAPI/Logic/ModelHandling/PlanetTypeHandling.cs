using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
namespace StarAPI.Logic.ModelHandling;


public class PlanetTypeHandling
{
    private readonly StarDeckContext _context;

    public PlanetTypeHandling(StarDeckContext context)
    {
        this._context = context;
    }


    public List<PlanetType> GetAllPlanetTypes()
    {
        return _context.PlanetType.ToList();
    }


    public string GetPlanetType(int id)
    {
        try
        {
            return GetPlanetTypeName(id);
        }
        catch (System.Exception)
        {
            throw new ArgumentException("Invalid id");
        }
    }

    public string GetPlanetTypeName(int id)
    {
        PlanetType? planetType = _context.PlanetType.FirstOrDefault(r => r.id == id);
        if (planetType == null)
        {
            throw new ArgumentException("PlanetType does not exist");
        }
        return planetType.typeName;
    }

    public void AddPlanetType(string planetTypeName)
    {
        bool isNameValid = CheckInputName(planetTypeName);
        bool alreadyExist = AlreadyExists(planetTypeName);

        if(!isNameValid){
            throw new ArgumentException("Invalid name");
        }
        if(alreadyExist){
            throw new ArgumentException("PlanetType already exist");
        }
        InsertPlanetType(planetTypeName);

    }


    public void InsertPlanetType(string raceName){
        var PlanetType = new PlanetType {typeName = raceName};
        _context.PlanetType.Add(PlanetType);
        _context.SaveChanges();
    }

    private bool CheckInputName(string raceName){
        return true;
    }
    private bool AlreadyExists(string typeName){
        var PlanetType = _context.PlanetType.FirstOrDefault(r => r.typeName == typeName);
        if(PlanetType == null){
            return false;
        }
        return true;
    }

}