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

        public SetupController(StarDeckContext context)
        {
            this._gameLogic = new GameLogic(context);
            this._tableLogic = new TableLogic(context);
            this._handHandling = new HandHandling(context);
        }


        [HttpPost("SetupParameters/")]
        public ActionResult SetupParam([FromBody] SetupValues setupValues)
        {
            try
            {
                return Ok(_gameLogic.SetUpGame(setupValues));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("GetGamePlanets/{gameId}")]
        public List<OutputPlanet> GetGamePlanets(string gameId)
        {
            return _tableLogic.GetGamePlanets(gameId);
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
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetHandCards/{gameId}/{playerId}")]
        public ActionResult GetHandCards(string gameId, string playerId)
        {
            try
            {
                return Ok(_gameLogic.GetHandCards(gameId,playerId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("EndGame/{gameId}")]
        public ActionResult EndGame(string? gameId)
        {
            try
            {
                _gameLogic.EndGame(gameId);
               return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
