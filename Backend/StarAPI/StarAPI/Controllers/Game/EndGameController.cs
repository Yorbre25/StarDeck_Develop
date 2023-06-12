using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DTO.Game;
using StarAPI.DataHandling.Game;
using StarAPI.Logic.Game;
using StarAPI.Logic;

namespace StarAPI.Controllers
{

    [ApiController]
    public class EndGameController : ControllerBase
    {

        private readonly StarDeckContext _context;
        private GameLogic _gameLogic;
        private ILogger<EndGameController> _logger;

        public EndGameController(StarDeckContext context,ILogger<EndGameController> logger)
        {
            _context = context;
            this._gameLogic = new GameLogic(context);
            this._logger = logger;
        }

        [HttpDelete("EndGame/{gameId}")]
        public ActionResult EndGame(string? gameId)
        {
            try
            {
                EndGame endGame = new EndGame(_context);
                var output = endGame.endGame(gameId);
                return Ok(output);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
