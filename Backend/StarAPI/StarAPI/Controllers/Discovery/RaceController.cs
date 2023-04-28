using Microsoft.AspNetCore.Mvc;
using StarAPI.Models;
using StarAPI.Logic.AdminLogic;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly RaceHandling raceHandling;

        public RaceController()
        {
        }
        
        /// <summary>
        /// Sends all races from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Race> GetAllRaces()
        {
            return raceHandling.GetAllRaces();
        }

        /// <summary>
        /// This method returns a race
        /// </summary>
        /// <param name="id">Id of race to be returned</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Race? GetById(int id)
        {
            return raceHandling.GetRace(id);
        }

        
        /// <summary>
        /// This method adds a race
        /// </summary>
        /// <param name="raceName"> Name of the race to be added</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddRace(string raceName)
        {
            try
            {
                raceHandling.AddRace(raceName);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        /// <summary>
        /// This method deletes a race
        /// </summary>
        /// <param name="id">Id of race to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                raceHandling.DeleteRace(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
