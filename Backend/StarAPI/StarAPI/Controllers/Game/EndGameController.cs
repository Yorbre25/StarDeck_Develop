using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DTO.Game;
using StarAPI.DataHandling.Game;
using StarAPI.Logic.Game;

namespace StarAPI.Controllers
{

    [ApiController]
    public class EndGameController : ControllerBase
    {
        private GameLogic _gameLogic;
        private TableLogic _tableLogic;
        private HandHandling _handHandling;
        private ILogger<EndGameController> _logger;

        public EndGameController(StarDeckContext context,ILogger<EndGameController> logger)
        {
            this._gameLogic = new GameLogic(context);
            this._logger = logger;
        }

        [HttpDelete("EndGame/{gameId}")]
        public ActionResult EndGame(string? gameId)
        {
            try
            {
                var output = _gameLogic.EndGame(gameId);
                this._logger.LogInformation("Game ended successfully at {time}", DateTime.Now.ToString("hh:mm:ss tt"));
                return Ok(output);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
