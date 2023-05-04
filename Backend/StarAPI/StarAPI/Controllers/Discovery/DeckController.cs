using Microsoft.AspNetCore.Mvc;
using StarAPI.Logic.ModelHandling;
using StarAPI.DTOs;
using StarAPI.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly StarDeckContext _context;
        private DeckHandling _deckHandling;
        private DeckCardHandling _deckCardHandling;

        public DeckController(StarDeckContext context)
        {
            this._context = context;
            this._deckHandling = new DeckHandling(context);
            this._deckCardHandling = new DeckCardHandling(context);
        }
        
        [HttpGet("GetDecksFromPlayer/{playerId}")]
        public List<OutputDeck> GetDecksFromPlayer(string playerId)
        {
            return _deckHandling.GetDecksFromPlayer(playerId);
        }

        
        [HttpGet("GetCardsFromDeck/{deckId}")]
        public List<OutputCard> GetCardsFromDeck(string deckId)
        {
            return _deckCardHandling.GetCardsFromDeck(deckId);
        }


        [HttpPost("AddDeck")]
        public ActionResult AddDeck([FromBody] InputDeck deck)
        {
            try
            {
                _deckHandling.AddDeck(deck);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

      
    }
}
