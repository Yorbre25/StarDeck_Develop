using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Context;
using StarAPI.Logic;

namespace StarAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private readonly StarDeckContext _context;
        private PlanetCRUD _planetCRUD ;

        public PlanetController(StarDeckContext context)
        {
            this._planetCRUD = new PlanetCRUD(context);
        }

        [HttpGet("GetAllPlanets")]
        public IEnumerable<OutputPlanet> GetAllPlanets()
        {
            return _planetCRUD.GetAllPlanets();
        }

        [HttpGet("GetPlanet/{id}")]
        public OutputPlanet GetPlanet(string id)
        {
            return _planetCRUD.GetPlanet(id);
        }


        [HttpPost("AddPlanet")]
        public ActionResult AddPlanet([FromBody] InputPlanet planet)
        {
            try
            {
                _planetCRUD.AddPlanet(planet);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}