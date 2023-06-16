using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
using Contracts;

namespace StarAPI.DataHandling.Discovery;

public class RaceHandling
{
    private readonly IRepositoryWrapper _context;

    public RaceHandling(IRepositoryWrapper context)
    {
        this._context = context;
    }

    public List<Race> GetAllRaces()
    {
        // return _context.Race.ToList();
        return _context.Race.GetAll();
    }


    public string GetRace(int id){
        try
        {
            return GetRaceName(id);
        }
        catch (System.Exception)
        {
            throw new ArgumentException("Invalid id");
        }
    }

    public string GetRaceName(int id)
    {
        // Race? race = _context.Race.Get(r => r.id == id);
        Race? race = _context.Race.Get(id);
        if (race == null)
        {
            throw new ArgumentException("Race does not exist");
        }
        return race.name;
    }


    public void AddRace(string raceName)
    {
        bool isNameValid = CheckInputName(raceName);
        bool alreadyExist = AlreadyExists(raceName);

        if(!isNameValid){
            throw new ArgumentException("Invalid name");
        }
        if(alreadyExist){
            throw new ArgumentException("Race already exist");
        }
        InsertRace(raceName);

    }

    public void InsertRace(string raceName){
        var race = new Race {name = raceName};
        // _context.Race.Add(race);
        // _context.SaveChanges();
        _context.Race.Add(race);
        _context.Save();
    }

    private bool CheckInputName(string raceName){
        return true;
    }

    private bool AlreadyExists(string raceName){
        var races = _context.Race.GetAll();
        var race = races.FirstOrDefault(r => r.name == raceName);
        if(race == null){
            return false;
        }
        return true;
    }

    private bool AlreadyExists(int id){
        Race? race = new Race();
        // race = _context.Race.FirstOrDefault(r => r.id == id);
        race = _context.Race.Get(id);
        if(race == null){
            return false;
        }
        return true;
    }

    public void DeleteRace(int id)
    {
        bool alreadyExists = AlreadyExists(id);
        if(!alreadyExists)
        {
            throw new ArgumentNullException("Race does not exist");
        }
        else
        {
            RemoveRace(id);
        }
    }

    private void RemoveRace(int id)
    {
        // var race = _context.Race.FirstOrDefault(r => r.id == id);
        // _context.Race.Remove(race);
        // _context.SaveChanges();
        var race = _context.Race.Get(id);
        _context.Race.Delete(race);
        _context.Save();
    }

}


