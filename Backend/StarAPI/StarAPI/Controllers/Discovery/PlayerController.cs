using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Logic.Utils;
using StarAPI.Context;


namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly StarDeckContext context;

        public PlayerController(StarDeckContext context) 
        {
            this.context = context;
        }
        
        [HttpGet("GetAllPlayers")]
        public IEnumerable<Player> GetAllPlayers()
        {
            return context.Player.ToList();
        }


        // [HttpGet("{email}/{password}")]
        // public ActionResult Get(string email, string password)
        // {
            
        //     Player player = context.Player.FirstOrDefault(p => p.email == email || (p.id == email));
           
        //     if (player != null && encrypt.Sha256(password) == player.p_hash ) 
        //     {
        //         return Ok();
        //     }
                         
        //     return BadRequest();
        // }

        // [HttpGet("GetPlayerById/{id}")]
        // public Player GetPlayerById(string id)
        // {
        //     return context.Player.FirstOrDefault(p => p.id == id || p.email == id);
        // }

 
        // [HttpPost]
        // public ActionResult AddPlayer([FromBody] Player player)
        // {
        //     try 
        //     {
        //         if(context.Player.FirstOrDefault(p => p.email == player.email) != null) 
        //         {
        //             return BadRequest("The e-mail is already in use");
        //         }
        //         var id = _idGenerator.GenerateId("U");
        //         while (context.Player.FirstOrDefault(p=> p.id == id) !=null )
        //         {
        //             id = _idGenerator.GenerateId("U");
        //         }
        //         if(player.avatar == "")
        //         {
        //             player.avatar = "Imagen default";
        //         }
        //         player.coins = 20;
        //         player.id = id;
        //         player.p_hash = encrypt.Sha256(player.p_hash);
        //         context.Player.Add(player);
        //         context.SaveChanges();
        //         return Ok();
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
    }
}
