using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Context;
using StarAPI.Models;
using StarAPI.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        // GET: api/<PlayerController>
        [HttpGet]
        public IEnumerable<Player> Get()
        {
            return context.Player.ToList();
        }

        [HttpGet("{id}/{password}")]
        
        public ActionResult Get(string id, string password)
        {
            
            Player player = context.Player.FirstOrDefault(p => p.id == id);
           
            if (encrypt.Sha256(password) == player.p_hash) 
            {
                return Ok();
            }
                         
            return BadRequest();
        }

        // GET api/<PlayerController>/5
        [HttpGet("{id}")]
        public Player Get(string id)
        {
            return context.Player.FirstOrDefault(p => p.id == id);
        }

        // POST api/<PlayerController>
        [HttpPost]
        public ActionResult Post([FromBody] Player player)
        {
            try 
            {
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

        // PUT api/<PlayerController>/5
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

        // DELETE api/<PlayerController>/5
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
