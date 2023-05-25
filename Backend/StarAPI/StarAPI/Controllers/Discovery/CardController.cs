using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.DataHandling.Discovery;
using StarAPI.DTO.Discovery;
using StarAPI.Context;
using StarAPI.Logic;

namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly StarDeckContext _context;
        private CardCRUD _cardCrud;

        public CardController(StarDeckContext context)
        {
            this._context = context;
            this._cardCrud = new CardCRUD(_context);
        }


 

        [HttpGet("GetAllCards")]
        public IEnumerable<OutputCard> GetAllCards()
        {
            return _cardCrud.GetAllCards();
        }



        [HttpGet("GetCardById/{id}")]
        public OutputCard GetCard(string id)
        {
            return _cardCrud.GetCard(id);
        }


        [HttpPost("AddCard")]
        public ActionResult Post([FromBody] InputCard card)
        {
            try
            {
                _cardCrud.AddCard(card);
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
                _cardCrud.DeleteCard(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
