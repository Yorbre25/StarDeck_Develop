using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DTO.Game;
using StarAPI.DataHandling.Game;

namespace StarAPI.Controllers
{

    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly StarDeckContext _context;
        private GameHandling _gameHandling;
        private HandHandling _handHandling;

        public SetupController(StarDeckContext context)
        {
            this._context = context;
            this._gameHandling = new GameHandling(_context);
            this._handHandling = new HandHandling(_context);
        }


        [HttpPost("SetupParameters/")]
        public ActionResult SetupParam([FromBody] SetUpValues setUpValues)
        {
            try
            {
                return Ok(_gameHandling.SetUpGame(setUpValues));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("GetGamePlanets/{gameId}")]
        public List<OutputPlanet> GetGamePlanets(string gameId)
        {
            return _gameHandling.GetPlanets(gameId);
        }

        [HttpPost("SetupHands/{gameId}")]
        public ActionResult SetupHands(string gameId)
        {
            try
            {
                _gameHandling.SetupHands(gameId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetHandCards/{gameId}/{playerId}")]
        public ActionResult SetupHand(string gameId, string playerId)
        {
            try
            {
                return Ok(_gameHandling.GetHandCards(gameId,playerId));
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
                _gameHandling.EndGame(gameId);
               return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // [HttpGet("GetGame")]
        // public ActionResult GetHand()
        // {
        //     try
        //     {
        //         return Ok(_gameHandling.End());
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
       


        // Test
        // [HttpPost("GetGameTable/{gameTableId}")]
        // public GameTable NewGame(string gameTableId)
        // {
        //     return _gameHandling.GetGameTable(gameTableId);
        // }


        
        
        
    }
}
