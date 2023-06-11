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
    public class TurnController : ControllerBase
    {
        private StarDeckContext _context;
        private GameLogic _gameLogic;
        private ILogger<TurnController> _logger;

        public TurnController(StarDeckContext context, ILogger<TurnController> logger)
        {
            this._context = context;
            this._gameLogic = new GameLogic(context);
            this._logger = logger;
        }


        [HttpPost("DrawCard/{gameId}/{playerId}")]
        public ActionResult DrawCard(string gameId, string playerId)
        {
            try
            {
                DrawCard drawCard = new DrawCard(_context);
                return Ok(drawCard.Draw(gameId, playerId));
            }
            catch (Exception e)
            {
                _logger.LogError("Error drawing card for player in game {gameId}", gameId);
                return BadRequest(e.Message);
            }

        }

        [HttpPost("EndTurn")]
        public ActionResult EndTurn([FromBody] InputTableLayout tableLayout)
        {
            try{
                _gameLogic.EndTurn(tableLayout);
                _logger.LogInformation("Request to end turn successful");
                return Ok();
            }
            catch(Exception e){

                _logger.LogError("Error ending turn");
                return BadRequest(e.Message);
            }
        }

        [HttpPost("GetLayout/{gameId}/{playerId}")]
        public ActionResult GetLayout(string gameId, string playerId)
        {
            try
            {
                return Ok(_gameLogic.GetLayout(gameId, playerId));
            }
            catch (System.Exception e)
            {
                _logger.LogError("Error getting layout");
                return BadRequest(e.Message);
            }
        }

        [HttpPost("GetTurnInfo/{gameId}/{playerId}")]
        public ActionResult GetTurnInfo(string gameId, string playerId)
        {
            try
            {
                return Ok(_gameLogic.GetTurnInfo(gameId, playerId));
            }
            catch (System.Exception e)
            {
                _logger.LogError("Error getting turn info");
                return BadRequest(e.Message);
            }
        }
    }
}