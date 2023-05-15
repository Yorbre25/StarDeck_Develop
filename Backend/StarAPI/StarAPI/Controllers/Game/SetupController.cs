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


        [HttpPost("SetupParameters")]
        public ActionResult SetupParam()
        {
            try
            {
                return Ok(_gameHandling.SetUpGame());
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

        // [HttpPost("GetGameTable/{gameTableId}")]
        // public GameTable NewGame(string gameTableId)
        // {
        //     return _gameHandling.GetGameTable(gameTableId);

        // }


        
        
        
    }
}
