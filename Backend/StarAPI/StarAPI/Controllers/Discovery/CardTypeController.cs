using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Logic.AdminLogic;

namespace StarAPI.Controllers
{
    /// <summary>
    /// This class is used to handle all requests to the Card_Type table.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CardTypeController : ControllerBase
    {
        private readonly StarDeckContext _context;

        private CardTypeHandling cardTypeHandling;

        /// <summary>
        /// Constructor for CardTypeController.
        /// </summary>
        /// <param name="context"></param>
        public CardTypeController(StarDeckContext context)
        {
            this._context = context;
            cardTypeHandling = new CardTypeHandling(_context);
        }

        /// <summary>
        /// This method is used to get all card types from the Card_Type table.
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        [Route("GetAllCardTypes")]
        public IEnumerable<CardType> GetAllCard()
        {
           return cardTypeHandling.GetAllCardTypes();
        }

        /// <summary>
        /// This method is used to get a card type from the Card_Type table.
        /// </summary>
        /// <param name="id"> Id of card type to be searched </param>
        /// <returns>card type found</returns>
        [HttpGet("GetCardType/{id}")]
        public string GetCardType(int id)
        {
            return cardTypeHandling.GetCardType(id);
        }


        // POST api/<Card_TypeController>
        /// <summary>
        /// This method is used to add a card type to the Card_Type table.
        /// </summary>
        /// <param name="card_type"> Name of new card type</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddCardType/{cardTypeName}")]
        public ActionResult AddCardType(string cardTypeName)
        {
            try
            {
                cardTypeHandling.AddCardType(cardTypeName);
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
                cardTypeHandling.DeleteCardType(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
