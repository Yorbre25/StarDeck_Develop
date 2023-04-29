using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Utils;
using System.Numerics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarAPI.Controllers
{ 

    /// <summary>
    /// This class is the controller for the Country table. 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly StarDeckContext context;

        public CountryController(StarDeckContext context) 
        {
            this.context = context;
        }

        /// <summary>
        /// This method is used to get all countries from the Country table.
        /// </summary>
        /// <returns>All countries</returns>
        // GET: api/<CountryController>
        [HttpGet]   
        public IEnumerable<Country> Get()
        {
            return context.Country.ToList();
        }

        /// <summary>
        /// This method is used to get an specific country from the Country table.
        /// </summary>
        /// <param name="id">Id of country to be searched</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Country Get(string id)
        {
            return context.Country.FirstOrDefault(c=> c.id == id);
        }

        /// <summary>
        /// This method is used to add a new country to the Country table.
        /// </summary>
        [HttpPost]
        public ActionResult Post([FromBody] Country country) 
        {
            try
            {
             
                context.Country.Add(country);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    
        /// <summary>
        /// This method is used to update an specific country from the Country table. 
        /// </summary>
        /// <param name="id">Id of country to be updated</param>
        /// <param name="country">New country name</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Country country)
        {
            if (country.id == id)
            {
                context.Entry(country).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// This method is used to delete an specific country from the Country table. 
        /// </summary>
        /// <param name="id">Id of country to be deleted</param>
        /// <returns></returns>
        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var country = context.Country.FirstOrDefault(c => c.id == id);
            if (country != null)
            {
                context.Country.Remove(country);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
