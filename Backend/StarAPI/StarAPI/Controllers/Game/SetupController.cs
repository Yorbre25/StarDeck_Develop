using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DTO.Game;
using StarAPI.DataHandling.Game;
using StarAPI.Logic.Game;

namespace StarAPI.Controllers
{

    [ApiController]
    public class SetupController : ControllerBase
    {
        private GameLogic _gameLogic;
        private TableLogic _tableLogic;
        private HandHandling _handHandling;
        private ILogger<SetupController> _logger;

        public SetupController(StarDeckContext context, ILogger<SetupController> logger)
        {
            this._gameLogic = new GameLogic(context);
            this._tableLogic = new TableLogic(context);
            this._handHandling = new HandHandling(context);
            this._logger = logger;
        }


        [HttpPost("SetupParameters/")]
        public ActionResult SetupParam([FromBody] SetupValues setupValues)
        {
            try
            {
                var output = _gameLogic.SetUpGame(setupValues);
                _logger.LogInformation("Game Setup successful at {time}", DateTime.Now.ToString("hh:mm:ss tt"));
                return Ok(output);
            }
            catch (Exception e)
            {
                _logger.LogError("Game Setup failed at {time}", DateTime.Now.ToString("hh:mm:ss tt"));
                return BadRequest(e.Message);
            }
        }

        [HttpPost("GetGamePlanets/{gameId}")]
        public ActionResult GetGamePlanets(string gameId)
        {
            try
            {
                return Ok(_tableLogic.GetGamePlanets(gameId));
            }
            catch (Exception e)
            {
                _logger.LogError("Error getting game planets for game {gameId}", gameId);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("SetupHands/{gameId}")]
        public ActionResult SetupHands(string gameId)
        {
            try
            {
                _gameLogic.SetupHands(gameId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Hand Setup failed for game {gameId}", gameId);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetHandCards/{gameId}/{playerId}")]
        public ActionResult GetHandCards(string gameId, string playerId)
        {
            try
            {
                var output = _gameLogic.GetHandCards(gameId,playerId);
                return Ok(output);
            }
            catch (Exception e)
            {
                _logger.LogError("Error getting hand cards for player in game {gameId}", gameId);
                return BadRequest(e.Message);
            }
        }
    }
}
