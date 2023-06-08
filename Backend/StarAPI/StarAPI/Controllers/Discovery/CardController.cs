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
        private ILogger<CardController> _logger;

        public CardController(StarDeckContext context, ILogger<CardController> logger)
        {
            this._context = context;
            this._cardCrud = new CardCRUD(_context);
            this._logger = logger;
        }


 

        [HttpGet("GetAllCards")]
        public IEnumerable<OutputCard> GetAllCards()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM Game_Player");
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
                _logger.LogWarning("Error crating card from data base");
                return BadRequest(e.Message);
            }
        }

        [HttpPost("AddCards")]
        public ActionResult Post([FromBody] List<Card> cards)
        {
            try
            {
                foreach(var card in cards) 
                {
                    _context.Card.Add(card);
                }
                _context.SaveChanges();
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
                _logger.LogWarning("Error deleting card from data base");
                return BadRequest(e.Message);
            }
        }
    }
}
