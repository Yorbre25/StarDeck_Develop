using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;

namespace StarAPI.DataHandling.Discovery;

public class CountryHandling
{
    private readonly StarDeckContext _context;

    public CountryHandling(StarDeckContext context)
    {
        this._context = context;
    }

    public List<Country> GetAllCountries()
    {
        return _context.Country.ToList();
    }


    public void AddCountry(string countryName)
    {
        bool isNameValid = CheckInputName(countryName);
        bool alreadyExist = AlreadyExists(countryName);

        if(!isNameValid){
            throw new ArgumentException("Invalid name");
        }
        if(alreadyExist){
            throw new ArgumentException("Country already exist");
        }
        AddingCountry(countryName);

    }

    public void AddingCountry(string countryName){
        var country = new Country {countryName = countryName};
        _context.Country.Add(country);
        _context.SaveChanges();
    }

    public string GetCountry(int id)
    {
        try
        {
            return GetCountryName(id);
        }
        catch (System.Exception)
        {
            throw new ArgumentException("Invalid id");
        }
    }

    private bool CheckInputName(string countryName){
        return true;
    }

    private bool AlreadyExists(string countryName){
        var race = _context.Country.FirstOrDefault(r => r.countryName == countryName);
        if(race == null){
            return false;
        }
        return true;
    }

    private bool AlreadyExists(int id){
        Country? race = new Country();
        race = _context.Country.FirstOrDefault(r => r.id == id);
        if(race == null){
            return false;
        }
        return true;
    }


    public string GetCountryName(int id)
    {
        Country? country = _context.Country.FirstOrDefault(r => r.id == id);
        if (country == null)
        {
            throw new ArgumentException("Race does not exist");
        }
        return country.countryName;
    }


}


