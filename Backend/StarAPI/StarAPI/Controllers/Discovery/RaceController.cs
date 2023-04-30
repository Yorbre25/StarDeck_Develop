using Microsoft.AspNetCore.Mvc;
using StarAPI.Models;
using StarAPI.Logic.ModelHandling;
using StarAPI.Context;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarAPI.Controllers
{
    /// <summary>
    /// This is class is used to handle all requests of the Race table
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly StarDeckContext _context;
        private  RaceHandling _raceHandling;

        /// <summary>
        /// Constructor for RaceController
        /// </summary>
        public RaceController(StarDeckContext context)
        {
            this._context = context;
            _raceHandling = new RaceHandling(_context);
        }

        
        /// <summary>
        /// Sends all races from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllRaces")]
        public IEnumerable<Race> GetAllRaces()
        {
            return _raceHandling.GetAllRaces();
        }

        /// <summary>
        /// This method returns a race
        /// </summary>
        /// <param name="id">Id of race to be returned</param>
        /// <returns></returns>
        [HttpGet("GetRace/{id}")]
        public string? GetRace(int id)
        {
            return _raceHandling.GetRace(id);
        }

        
        /// <summary>
        /// This method adds a race
        /// </summary>
        /// <param name="raceName"> Name of the race to be added</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddRace/{raceName}")]
        public ActionResult AddRace(string raceName)
        {
            try
            {
                _raceHandling.AddRace(raceName);
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
        [HttpDelete("DelteRace/{id}")]
        public ActionResult DeleteCardType(int id)
        {
            try
            {
                _raceHandling.DeleteRace(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
