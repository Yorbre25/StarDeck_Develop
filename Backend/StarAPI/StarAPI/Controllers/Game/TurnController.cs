using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DTO.Game;
using StarAPI.DataHandling.Game;

namespace StarAPI.Controllers
{

    [ApiController]
    public class TurnController : ControllerBase
    {
        private readonly StarDeckContext _context;
        private GameHandling _gameHandling;

        public TurnController(StarDeckContext context)
        {
            this._context = context;
            this._gameHandling = new GameHandling(_context);
        }


        [HttpPost("DrawCard/{gameId}/{playerId}")]
        public ActionResult DrawCard(string gameId, string playerId)
        {
            try
            {
                OutputCard outputCard = _gameHandling.DrawCard(gameId, playerId);
                if (outputCard == null)
                {
                    return Ok();
                }
                return Ok(outputCard);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}