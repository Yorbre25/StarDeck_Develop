using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DataHandling.Discovery;

namespace StarAPI.Controllers
{ 

    [Route("[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly StarDeckContext context;
        private CountryHandling _countryHandling;
        private ILogger<CountryController> _logger;

        public CountryController(StarDeckContext context, ILogger<CountryController> logger) 
        {
            this.context = context;
            _countryHandling = new CountryHandling(context);
        }

        [HttpGet("GetAllCountries")]   
        public IEnumerable<Country> GetAllCountries()
        {
            return context.Country.ToList();
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

        [HttpPost("AddCountries")]
        public ActionResult AddCountriess([FromBody] List<Country> countries)
        {
            try
            {
                foreach (Country country in countries) 
                {
                    context.Country.Add(country);
                }
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
