using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
using StarAPI.DataHandling.Discovery;
using Contracts;

namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardTypeController : ControllerBase
    {

        private CardTypeHandling _cardTypeHandling;
        private PlanetTypeHandling _planetTypeHandling;

        public CardTypeController(IRepositoryWrapper repository)
        {
            _cardTypeHandling = new CardTypeHandling(repository);
            _planetTypeHandling = new PlanetTypeHandling(repository);
        }

        [HttpGet]
        [Route("GetAllCardTypes")]
        public IActionResult GetAllCardTypes()
        {
            try{
                return Ok(_cardTypeHandling.GetAllCardTypes());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetCardType/{id}")]
        public IActionResult GetCardType(int id)
        {
            try
            {
                return Ok(_cardTypeHandling.GetCardTypeName(id));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
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
                return NotFound();
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
