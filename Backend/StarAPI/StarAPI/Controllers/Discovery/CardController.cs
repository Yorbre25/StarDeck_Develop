using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Logic.ModelHandling;
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


 

        [HttpGet("GetAllCards")]
        public IEnumerable<OutputCard> GetAllCards()
        {
            return _cardHandling.GetAllCards();
        }



        [HttpGet("GetCardById/{id}")]
        public OutputCard GetCard(string id)
        {
            return _cardHandling.GetCard(id);
        }


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
