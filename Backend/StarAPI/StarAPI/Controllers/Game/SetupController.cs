using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DTO.Game;
using StarAPI.DataHandling.Game;
using StarAPI.Logic.Game;
using StarAPI.Logic;
using Contracts;

namespace StarAPI.Controllers
{

    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public SetupController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }


        [HttpPost("SetupParameters/")]
        public ActionResult SetupParam([FromBody] SetupValues setupValues)
        {
            try
            {
                NewGame newGame = new NewGame(_repository);
                var output = newGame.SetupNewGame(setupValues);
                return Ok(output);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("GetGamePlanets/{gameId}")]
        public ActionResult GetGamePlanets(string gameId)
        {
            try
            {
                GameTableHandling gameTableHandling = new GameTableHandling(_repository);
                return Ok(gameTableHandling.GetGamePlanets(gameId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("SetupHands/{gameId}")]
        public ActionResult SetupHands(string gameId)
        {
            try
            {
                SetupHands setupHands = new SetupHands(_repository);
                setupHands.SetupHand(gameId);
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
                HandCard handHandling = new HandCard(_repository);
                var output = handHandling.GetHandCards(gameId,playerId);
                return Ok(output);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
