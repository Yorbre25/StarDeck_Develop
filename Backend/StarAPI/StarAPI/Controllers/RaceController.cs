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
        
        /// <summary>
        /// This method adds a race to the dabe base
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Race> Get()
        {
            return context.Race.ToList();
        }

        /// <summary>
        /// This method returns a race
        /// </summary>
        /// <param name="id">Id of race to be returned</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Race Get(string id)
        {
            return context.Race.FirstOrDefault(r => r.race == id);
        }

        
        /// <summary>
        /// This method adds a race
        /// </summary>
        /// <param name="race"> Name of the race to be added</param>
        /// <returns></returns>
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

        /// <summary>
        /// This method updates a race
        /// </summary>
        /// <param name="id">Id of race to be updated</param>
        /// <param name="race">New name of race</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Race race)
        {
            if (race.race == id)
            {
                context.Entry(race).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// This method deletes a race
        /// </summary>
        /// <param name="id">Id of race to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var race = context.Race.FirstOrDefault(r => r.race == id);
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
