using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.Models;
using StarAPI.Logic.GameLogic;

namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly StarDeckContext _context;
        private SetupGame _setupGame;

        public SetupController(StarDeckContext context)
        {
            this._context = context;
            this._setupGame = new SetupGame(_context);
        }


        [HttpGet("GetSetupParameters")]
        public SetupParam GetSetupParam()
        {
            return _setupGame.GetSetupParam();
        }


        
    }
}
