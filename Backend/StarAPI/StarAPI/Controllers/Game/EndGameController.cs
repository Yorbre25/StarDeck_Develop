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

        public EndGameController(StarDeckContext context)
        {
            this._gameLogic = new GameLogic(context);
        }

        [HttpDelete("EndGame/{gameId}")]
        public ActionResult EndGame(string? gameId)
        {
            try
            {
                //En caso de empate falta
                return Ok(_gameLogic.EndGame(gameId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
