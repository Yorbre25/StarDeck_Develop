using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Logic.ModelHandling;
using StarAPI.DTOs;
using StarAPI.Context;

namespace StarAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private readonly StarDeckContext _context;
        private  PlanetHandling _planetHandling;

        public PlanetController(StarDeckContext context)
        {
            this._context = context;
            this._planetHandling = new PlanetHandling(_context);
        }




        [HttpPost("AddPlanet")]
        public ActionResult AddPlanet([FromBody] InputPlanet planet)
        {
            try
            {
                _planetHandling.AddPlanet(planet);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}