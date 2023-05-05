using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.ModelHandling;

namespace StarAPI.Controllers
{
    /// <summary>
    /// This class is used to handle all requests to the Card_Type table.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly StarDeckContext _context;

        private CardTypeHandling _cardTypeHandling;
        private PlanetTypeHandling _planetTypeHandling;

        /// <summary>
        /// Constructor for TypeController.
        /// </summary>
        /// <param name="context"></param>
        public TypeController(StarDeckContext context)
        {
            this._context = context;
            _cardTypeHandling = new CardTypeHandling(_context);
            _planetTypeHandling = new PlanetTypeHandling(_context);
        }

        /// <summary>
        /// This method is used to get all card types from the Card_Type table.
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        [Route("GetAllCardTypes")]
        public IEnumerable<CardType> GetAllCardTypes()
        {
           return _cardTypeHandling.GetAllCardTypes();
        }

        /// <summary>
        /// This method is used to get a card type from the Card_Type table.
        /// </summary>
        /// <param name="id"> Id of card type to be searched </param>
        /// <returns>card type found</returns>
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
                return BadRequest(e.Message);
            }
            
        }


        // This method is used to delete a card type from the Card_Type table.
        // DELETE api/<Card_TypeController>/5
        /// <summary>
        /// Deteles a card type from the Card_Type table.
        /// </summary>
        /// <param name="id">Id of card type to be deleted</param>
        /// <returns></returns>
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
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetAllPlanetTypes")]
        public IEnumerable<PlanetType> GetAllPlanetTypes()
        {
           return _planetTypeHandling.GetAllPlanetTypes();
        }

    }
}
