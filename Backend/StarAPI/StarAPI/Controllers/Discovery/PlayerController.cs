using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Utils;


namespace StarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private Encrypt encrypt = new Encrypt();
        private readonly StarDeckContext context;
        public PlayerController(StarDeckContext context) 
        {
            this.context = context;
        }
        
        /// <summary>
        /// This method returns all the players in the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Player> Get()
        {
            return context.Player.ToList();
        }

        /// <summary>
        /// This method returns the player with the given email and password
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <param name="password">Password of user</param>
        /// <returns></returns>
        [HttpGet("{email}/{password}")]
        public ActionResult Get(string email, string password)
        {
            
            Player player = context.Player.FirstOrDefault(p => p.email == email || (p.id == email));
           
            if (player != null && encrypt.Sha256(password) == player.p_hash ) 
            {
                return Ok();
            }
                         
            return BadRequest();
        }

        /// <summary>
        /// This method returns the player with the given id
        /// </summary>
        /// <param name="id">Id of player to be searched</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Player Get(string id)
        {
            return context.Player.FirstOrDefault(p => p.id == id || p.email == id);
        }

        /// <summary>
        /// This method adds a new player to the database
        /// </summary>
        /// <param name="player">Player data</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] Player player)
        {
            try 
            {
                if(context.Player.FirstOrDefault(p => p.email == player.email) != null) 
                {
                    return BadRequest("The e-mail is already in use");
                }
                var id = encrypt.gen_id("U");
                while (context.Player.FirstOrDefault(p=> p.id == id) !=null )
                {
                    id = encrypt.gen_id("U");
                }
                if(player.avatar == "")
                {
                    player.avatar = "Imagen default";
                }
                player.coins = 20;
                player.id = id;
                player.p_hash = encrypt.Sha256(player.p_hash);
                context.Player.Add(player);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// This method updates the player with the given id
        /// </summary>
        /// <param name="id">Id of player to be updated</param>
        /// <param name="player">Player to be updated</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Player player )
        {
            if (player.id == id) 
            {
                context.Entry(player).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// This method deletes the player with the given id
        /// </summary>
        /// <param name="id">Id of player to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            Player player = context.Player.FirstOrDefault(p => p.id == id);
            if (player != null) 
            {
                context.Player.Remove(player);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
