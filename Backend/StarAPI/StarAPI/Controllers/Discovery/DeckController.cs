using Microsoft.AspNetCore.Mvc;
using StarAPI.DataHandling.Discovery;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private DeckHandling _deckHandling;
        private DeckCardHandling _deckCardHandling;
        private ILogger<DeckController> _logger;

        public DeckController(IRepositoryWrapper context, ILogger<DeckController> logger)
        {
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
                _logger.LogWarning("Error crating deck from data base");
                return BadRequest(e.Message);
            }
        }

      
    }
}
