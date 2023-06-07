using Microsoft.AspNetCore.Mvc;
using StarAPI.Models;
using StarAPI.DataHandling.Discovery;
using StarAPI.Context;



namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly StarDeckContext _context;
        private  RaceHandling _raceHandling;

        public RaceController(StarDeckContext context)
        {
            this._context = context;
            _raceHandling = new RaceHandling(_context);
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
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost("AddRaces")]
        public ActionResult AddRaces([FromBody] List<Race> races) 
        {
            try 
            {
                foreach(var race in races) 
                {
                    _context.Race.Add(race);

                }
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception e) 
            {
                return BadRequest(e.Message);
            }
        }

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
