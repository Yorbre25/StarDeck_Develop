using Microsoft.EntityFrameworkCore;
using StarAPI.Models;

namespace StarAPI.Logic.AdminLogic;

/// <summary>
/// Class who creates, deletes and adds races to the database
/// </summary>
public class RaceHandling
{
    private readonly StarDeckContext _context;

    /// <summary>
    /// RaceHandling constructor. Initializes the DBContext
    /// </summary>
    /// <param name="context"></param>
    public RaceHandling(StarDeckContext context)
    {
        this._context = context;
    }

    /// <summary>
    /// Returns all races from the database
    /// </summary>
    /// <returns>A list of Race</returns>
    public List<Race> GetAllRaces()
    {
        return _context.Race.ToList();
    }


    /// <summary>
    /// Returns a race with the given id
    /// </summary>
    /// <param name="id">Id of race to be searched</param>
    /// <returns></returns>
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
        Race? race = _context.Race.FirstOrDefault(r => r.id == id);
        if (race == null)
        {
            throw new ArgumentException("Race does not exist");
        }
        return race.name;
    }


    /// <summary>
    /// Top function for adding a race
    /// </summary>
    /// <param name="raceName"> New race name</param>
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

    /// <summary>
    /// Inserts the new race to the database
    /// </summary>
    /// <param name="raceName"> New race name</param>
    public void InsertRace(string raceName){
        var race = new Race {name = raceName};
        _context.Race.Add(race);
        _context.SaveChanges();
    }

    /// <summary>
    /// Checks if the name is valid
    /// </summary>
    /// <param name="raceName"> New race name</param>
    /// <returns></returns>
    private bool CheckInputName(string raceName){
        return true;
    }

    /// <summary>
    /// Checks if the race already exists
    /// </summary>
    /// <param name="raceName"> Name of the race</param>
    /// <returns></returns>
    private bool AlreadyExists(string raceName){
        var race = _context.Race.FirstOrDefault(r => r.name == raceName);
        if(race == null){
            return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the race already exists
    /// </summary>
    /// <param name="id"> Id of race to look</param>
    /// <returns></returns>
    private bool AlreadyExists(int id){
        Race? race = new Race();
        race = _context.Race.FirstOrDefault(r => r.id == id);
        if(race == null){
            return false;
        }
        return true;
    }

    /// <summary>
    /// Top function for deleting 
    /// </summary>
    /// <param name="id">Id of race to be removed</param>
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

    /// <summary>
    /// Removes race with the given id
    /// </summary>
    /// <param name="id">Id of race to be removed</param>
    private void RemoveRace(int id)
    {
        var race = _context.Race.FirstOrDefault(r => r.id == id);
        _context.Race.Remove(race);
        _context.SaveChanges();
    }

}


