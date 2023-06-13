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
    public class SetupController : ControllerBase
    {
        private readonly StarDeckContext _context;
        private ILogger<SetupController> _logger;

        public SetupController(StarDeckContext context, ILogger<SetupController> logger)
        {
            this._context = context;
            this._logger = logger;
        }


        [HttpPost("SetupParameters/")]
        public ActionResult SetupParam([FromBody] SetupValues setupValues)
        {
            try
            {
                NewGame newGame = new NewGame(_context);
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
                GameTableHandling gameTableHandling = new GameTableHandling(_context);
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
                SetupHands setupHands = new SetupHands(_context);
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
                HandCard handHandling = new HandCard(_context);
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
