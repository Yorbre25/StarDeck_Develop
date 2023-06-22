using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
using Contracts;

namespace StarAPI.DataHandling.Discovery;

public class CountryHandling
{
    private readonly IRepositoryWrapper _repository;

    public CountryHandling(IRepositoryWrapper repository)
    {
        this._repository = repository;
    }

    public List<Country> GetAllCountries()
    {
        // return _repository.Country.ToList();
        return _repository.Country.GetAll();
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
        // _repository.Country.Add(country);
        // _repository.SaveChanges();
        _repository.Country.Add(country);
        _repository.Save();
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
        var races = _repository.Race.GetAll();
        var race = races.FirstOrDefault(r => r.name == countryName);
        if(race == null){
            return false;
        }
        return true;
    }

    private bool AlreadyExists(int id){
        Country? race = new Country();
        race = _repository.Country.Get(id);
        if(race == null){
            return false;
        }
        return true;
    }


    public string GetCountryName(int id)
    {
        // Country? country = _repository.Country.FirstOrDefault(r => r.id == id);
        Country? country = _repository.Country.Get(id);
        if (country == null)
        {
            throw new ArgumentException("Race does not exist");
        }
        return country.countryName;
    }


}


