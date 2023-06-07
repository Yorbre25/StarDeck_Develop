using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.DTO.Discovery;
using StarAPI.Context;
using StarAPI.DataHandling.Discovery;
using StarAPI.Logic;

namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private PlayerCRUD _playerCRUD;
        public PlayerController(StarDeckContext context) 
        {
            this._playerCRUD = new PlayerCRUD(context);
        }
        
        [HttpGet("GetAllPlayers")]
        public IEnumerable<OutputPlayer> GetAllPlayers()
        {
            return _playerCRUD.GetAllPlayers();
        }

 
        [HttpPost("AddPlayer")]
        public ActionResult AddPlayer([FromBody] InputPlayer player)
        {
            try 
            {
                _playerCRUD.AddPlayer(player);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
