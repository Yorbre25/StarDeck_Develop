using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Context;
using StarAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Deck_CardController : ControllerBase
    {
        private readonly StarDeckContext context;
        public Deck_CardController(StarDeckContext context)
        {
            this.context = context;
        }

        // GET: api/<Deck_CardController>
        [HttpGet]
        public IEnumerable<Deck_Card> Get()
        {
            return context.Deck_Card.ToList();
        }

        // GET api/<Deck_CardController>/5
        [HttpGet("{deck_id}/{card_id}")]
        public Deck_Card Get(string deck_id, string card_id)
        {
            return context.Deck_Card.FirstOrDefault(d => d.deck_id == deck_id  && d.card_id ==  card_id); ;
        }

        // POST api/<Deck_CardController>
        [HttpPost]
        public ActionResult Post([FromBody] Deck_Card deck_card)
        {
            try
            {
                context.Deck_Card.Add(deck_card);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<Deck_CardController>/5
        [HttpPut("{deck_id}/{card_id}")]
        public ActionResult Put(string deck_id, string card_id, [FromBody] Deck_Card deck_card)
        {
            if (deck_card.deck_id == deck_id && deck_card.card_id == card_id)
            {
                context.Entry(deck_card).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<Deck_CardController>/5
        [HttpDelete("{deck_id}/{card_id}")]
        public ActionResult Delete(string deck_id, string card_id)
        {
            var deck_card = context.Deck_Card.FirstOrDefault(d => d.deck_id == deck_id && d.card_id==card_id);
            if (deck_card != null)
            {
                context.Deck_Card.Remove(deck_card);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
