using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.Logic.ModelHandling;
using StarAPI.DTOs;

namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly StarDeckContext _context;
        private PlayerHandling _playerHandling;

        public PlayerController(StarDeckContext context) 
        {
            this._context = context;
            this._playerHandling = new PlayerHandling(_context);
        }
        
        [HttpGet("GetAllPlayers")]
        public IEnumerable<OutputPlayer> GetAllPlayers()
        {
            return _playerHandling.GetAllPlayers();
        }


        // [HttpGet("GetPlayerById/{id}")]
        // public Player GetPlayerById(string id)
        // {
        //     return context.Player.FirstOrDefault(p => p.id == id || p.email == id);
        // }

 
        [HttpPost("AddPlayer")]
        public ActionResult AddPlayer([FromBody] InputPlayer player)
        {
            try 
            {
                _playerHandling.AddPlayer(player);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
