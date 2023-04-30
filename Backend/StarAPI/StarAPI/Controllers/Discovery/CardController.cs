using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Logic.AdminLogic;
using StarAPI.DTOs;
using StarAPI.Context;

namespace StarAPI.Controllers
{
    /// <summary>
    /// This class is used to handle all requests of Card table.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly StarDeckContext _context;
        // private Encrypt encrypt = new Encrypt();
        private CardHandling _cardHandling;

        public CardController(StarDeckContext context)
        {
            this._context = context;
            this._cardHandling = new CardHandling(_context);
        }
 
        /// <summary>
        /// This method is used to get all cards from the Card table. 
        /// </summary>
        /// <returns>All cards</returns>
        // GET: api/<CardController>
        [HttpGet("GetAllCards")]
        public IEnumerable<OutputCard> GetAllCards()
        {
            return _cardHandling.GetAllCards();
        }


        /// <summary>
        /// This methos is used to get an specific card from the Card table.
        /// </summary>
        /// <param name="id">Id of card to be searched</param>
        /// <returns>Card with the same id</returns>
        // GET api/<CardController>/5
        [HttpGet("GetCardById/{id}")]
        public OutputCard GetCard(string id)
        {
            return _cardHandling.GetCard(id);
        }

        /// <summary>
        /// This method is used to add a new card to the Card table.
        /// </summary>
        /// <param name="card"> Card to add</param>
        /// <returns></returns>
        // POST api/<CardController>
        [HttpPost("AddCard")]
        public ActionResult Post([FromBody] InputCard card)
        {
            try
            {
                _cardHandling.AddCard(card);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
