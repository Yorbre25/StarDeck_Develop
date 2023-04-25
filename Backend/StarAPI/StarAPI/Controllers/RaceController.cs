using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Context;
using StarAPI.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly StarDeckContext context;

        public RaceController(StarDeckContext context)
        {
            this.context = context;
        }
        // GET: api/<RaceController>
        [HttpGet]
        public IEnumerable<Race> Get()
        {
            return context.Race.ToList();
        }

        // GET api/<RaceController>/5
        [HttpGet("{id}")]
        public Race Get(int id)
        {
            return context.Race.FirstOrDefault(r => r.race_id == id);
        }

        // POST api/<RaceController>
        [HttpPost]
        public ActionResult Post([FromBody] Race race)
        {
            try
            {

                context.Race.Add(race);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<RaceController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Race race)
        {
            if (race.race_id == id)
            {
                context.Entry(race).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<RaceController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var race = context.Race.FirstOrDefault(r => r.race_id == id);
            if (race != null)
            {
                context.Race.Remove(race);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
