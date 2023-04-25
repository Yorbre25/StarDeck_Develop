using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Context;
using StarAPI.Models;
using StarAPI.Utils;
using System.Numerics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly StarDeckContext context;

        public CountryController(StarDeckContext context) 
        {
            this.context = context;
        }
        // GET: api/<CountryController>
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            return context.Country.ToList();
        }

        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public Country Get(string id)
        {
            return context.Country.FirstOrDefault(c=> c.id == id);
        }

        // POST api/<CountryController>
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

        // PUT api/<CountryController>/5
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
