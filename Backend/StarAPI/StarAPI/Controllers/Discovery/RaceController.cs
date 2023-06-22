using Microsoft.AspNetCore.Mvc;
using StarAPI.Models;
using StarAPI.DataHandling.Discovery;
using StarAPI.Context;
using Contracts;

namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private  RaceHandling _raceHandling;
        private ILogger<RaceController> _logger;

        public RaceController(IRepositoryWrapper context)
        {
            _raceHandling = new RaceHandling(context);
        }

        
        [HttpGet("GetAllRaces")]
        public IEnumerable<Race> GetAllRaces()
        {
            return _raceHandling.GetAllRaces();
        }

        [HttpGet("GetRace/{id}")]
        public string? GetRace(int id)
        {
            return _raceHandling.GetRace(id);
        }

        
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
                _logger.LogWarning("Error creating race from data base");
                return BadRequest(e.Message);
            }
            
        }

        // [HttpPost("AddRaces")]
        // public ActionResult AddRaces([FromBody] List<Race> races) 
        // {
        //     try 
        //     {
        //         foreach(var race in races) 
        //         {
        //             _context.Race.Add(race);

        //         }
        //         _context.SaveChanges();
        //         return Ok();
        //     }
        //     catch(Exception e) 
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

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
                _logger.LogWarning("Error deleting race from data base");
                return BadRequest();
            }
        }
    }
}
