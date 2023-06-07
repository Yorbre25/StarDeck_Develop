using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
using StarAPI.DataHandling.Discovery;

namespace StarAPI.Controllers
{
    /// <summary>
    /// This class is used to handle all requests to the CardType table.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly StarDeckContext _context;

        private CardTypeHandling _cardTypeHandling;
        private PlanetTypeHandling _planetTypeHandling;
        private ILogger<TypeController> _logger;

        public TypeController(StarDeckContext context, ILogger<TypeController> logger)
        {
            this._context = context;
            _cardTypeHandling = new CardTypeHandling(_context);
            _planetTypeHandling = new PlanetTypeHandling(_context);
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllCardTypes")]
        public IEnumerable<CardType> GetAllCardTypes()
        {
           return _cardTypeHandling.GetAllCardTypes();
        }

        [HttpGet("GetCardType/{id}")]
        public string GetCardType(int id)
        {
            return _cardTypeHandling.GetCardType(id);
        }


        [HttpPost]
        [Route("AddCardType/{cardTypeName}")]
        public ActionResult AddCardType(string cardTypeName)
        {
            try
            {
                _cardTypeHandling.AddCardType(cardTypeName);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error crating card type from data base");
                return BadRequest(e.Message);
            }
            
        }


        [HttpDelete("DeleteCardType/{id}")]
        public ActionResult DeleteCardType(int id)
        {
            try
            {
                _cardTypeHandling.DeleteCardType(id);
                return Ok();
            }
            catch
            {
                _logger.LogWarning("Error deleting card type from data base");
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetAllPlanetTypes")]
        public IEnumerable<PlanetType> GetAllPlanetTypes()
        {
           return _planetTypeHandling.GetAllPlanetTypes();
        }

        [HttpPost("AddPlanetType/{planetTypeName}")]
        public ActionResult AddPlanetType(string planetTypeName)
        {
            try
            {
                _planetTypeHandling.AddPlanetType(planetTypeName);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogWarning("Error crating planet type from data base");
                return BadRequest(e.Message);
            }
            
        }


        [HttpDelete("DeletePlanetType/{id}")]
        public ActionResult DeletePlanetType(int id)
        {
            try
            {
                _planetTypeHandling.DeletePlanetType(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
