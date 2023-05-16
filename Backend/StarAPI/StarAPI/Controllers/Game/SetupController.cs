using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.Models;
using StarAPI.Logic.GameLogic;
using StarAPI.DTOs;
using StarAPI.Logic.ModelHandling;

namespace StarAPI.Controllers
{

    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly StarDeckContext _context;
        private GameHandling _gameHandling;

        public SetupController(StarDeckContext context)
        {
            this._context = context;
            this._gameHandling = new GameHandling(_context);
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

        [HttpPost("SetupHand/{gameId}/{playerId}")]
        public ActionResult SetupHand(string gameId, string playerId)
        {
            try
            {
                return Ok( _gameHandling.SetupHand(gameId, playerId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // [HttpDelete("EndGame/{gameId}")]
        // public ActionResult EndGame(string gameId)
        // {
        //     try
        //     {
        //         _gameHandling.EndGame(gameId);
        //         return Ok();
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
