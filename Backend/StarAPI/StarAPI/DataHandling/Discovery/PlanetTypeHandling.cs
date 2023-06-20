using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
using Contracts;

namespace StarAPI.DataHandling.Discovery;


public class PlanetTypeHandling
{
    private readonly IRepositoryWrapper _repository;

    public PlanetTypeHandling(IRepositoryWrapper repository)
    {
        this._repository = repository;
    }


    public List<PlanetType> GetAllPlanetTypes()
    {
        // return _repository.PlanetType.ToList();
        return _repository.PlanetType.GetAll();
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
        // PlanetType? planetType = _repository.PlanetType.FirstOrDefault(r => r.id == id);
        PlanetType? planetType = _repository.PlanetType.Get(id);
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
        // _repository.PlanetType.Add(PlanetType);
        // _repository.SaveChanges();
        _repository.PlanetType.Add(PlanetType);
        _repository.Save();
    }

    private bool CheckInputName(string raceName){
        return true;
    }
    private bool AlreadyExists(string typeName){
        var planetTypes = _repository.PlanetType.GetAll();
        var planetType = planetTypes.FirstOrDefault(r => r.typeName == typeName);
        if(planetType == null){
            return false;
        }
        return true;
    }

    private bool AlreadyExists(int id){
        PlanetType? planetType = new PlanetType();
        // planetType = _repository.PlanetType.FirstOrDefault(r => r.id == id);
        planetType = _repository.PlanetType.Get(id);
        if(planetType == null){
            return false;
        }
        return true;
    }

    public void DeletePlanetType(int id)
    {
        bool alreadyExists = AlreadyExists(id);
        if(!alreadyExists)
        {
            throw new ArgumentNullException("PlanetType does not exist");
        }
        else
        {
            DeletingCardType(id);
        }
    }

    private void DeletingCardType(int id)
    {
        // var planetType = _repository.PlanetType.FirstOrDefault(r => r.id == id);
        // _repository.PlanetType.Remove(planetType);
        // _repository.SaveChanges();
        var planetType = _repository.PlanetType.Get(id);
        _repository.PlanetType.Delete(planetType);
        _repository.Save();
    }
}