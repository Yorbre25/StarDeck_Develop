using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DataHandling.Discovery;
using Contracts;

namespace StarAPI.Controllers
{ 

    [Route("[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private CountryHandling _countryHandling;
        private ILogger<CountryController> _logger;

        public CountryController(IRepositoryWrapper repository) 
        {
            _countryHandling = new CountryHandling(repository);
        }

        [HttpGet("GetAllCountries")]   
        public IEnumerable<Country> GetAllCountries()
        {
            return _countryHandling.GetAllCountries();
        }

        [HttpPost("AddCountry")]
        public ActionResult AddCountry(string countryName) 
        {
            try
            {
                _countryHandling.AddCountry(countryName);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error crating country from data base");
                return BadRequest(e.Message);
            }
        }

        // [HttpPost("AddCountries")]
        // public ActionResult AddCountriess([FromBody] List<Country> countries)
        // {
        //     try
        //     {
        //         foreach (Country country in countries) 
        //         {
        //             repository.Country.Add(country);
        //         }
        //         repository.SaveChanges();
        //         return Ok();
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
    }
}
