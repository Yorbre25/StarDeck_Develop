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
    public class CardController : ControllerBase
    {
        private readonly StarDeckContext _context;
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

        [HttpDelete("DeleteCard/{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                _cardHandling.DeleteCard(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
