using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Context;
using StarAPI.Logic;
using Contracts;

namespace StarAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private PlanetCRUD _planetCRUD;
        private ILogger<PlanetController> _logger;

        public PlanetController(IRepositoryWrapper context)
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
                _logger.LogWarning("Error crating planet from data base");
                return BadRequest(e.Message);
            }
        }

    }
}