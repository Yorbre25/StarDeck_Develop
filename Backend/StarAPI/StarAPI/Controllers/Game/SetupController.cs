using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.Models;
using StarAPI.Logic.GameLogic;
using StarAPI.DTOs;

namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly StarDeckContext _context;
        private SetupGame _setupGame;
        private PlanetsForGame _planetsForGame;

        public SetupController(StarDeckContext context)
        {
            this._context = context;
            this._setupGame = new SetupGame(_context);
            this._planetsForGame = new PlanetsForGame(_context);
        }


        [HttpGet("GetSetupParameters")]
        public SetupParam GetSetupParam()
        {
            return _setupGame.GetSetupParam();
        }

        [HttpGet("TestPlanetSetUp")]
        public List<OutputPlanet> TestPlanetSetUp()
        {
            return _planetsForGame.GetPlanetsForNewGame();
        }
        
        
        
    }
}
