using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.Logic.ModelHandling;

namespace StarAPI.Controllers
{ 

    [Route("[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly StarDeckContext context;
        private CountryHandling _countryHandling;

        public CountryController(StarDeckContext context) 
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
                return BadRequest(e.Message);
            }
        }
    }
}
