using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Context;
using StarAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Player_CardController : ControllerBase
    {
        private readonly StarDeckContext context;
        public Player_CardController(StarDeckContext context)
        {
            this.context = context;
        }

        // GET: api/<Player_CardController>
        [HttpGet]
        public IEnumerable<Player_Card> Get()
        {
            return context.Player_Card.ToList();
        }

        // GET api/<Player_CardController>/5
        [HttpGet("{player_id}/{card_id}")]
        public Player_Card Get(string player_id, string card_id)
        {
            return context.Player_Card.FirstOrDefault(p => p.player_id == player_id && p.card_id == card_id);
        }

        // POST api/<Player_CardController>
        [HttpPost]
        public ActionResult Post([FromBody] Player_Card player_card)
        {
            try
            {
                context.Player_Card.Add(player_card);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<Player_CardController>/5
        [HttpPut("{player_id}/{card_id}")]
        public ActionResult Put(string player_id, string card_id, [FromBody] Player_Card player_card)
        {
            if (player_card.player_id == player_id && player_card.card_id == card_id)
            {
                context.Entry(player_card).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<Player_CardController>/5
        [HttpDelete("{player_id}/{card_id}")]
        public ActionResult Delete(string player_id, string card_id)
        {
            var player_card = context.Player_Card.FirstOrDefault(p => p.player_id == player_id && p.card_id == card_id);
            if (player_card != null)
            {
                context.Player_Card.Remove(player_card);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
