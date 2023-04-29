using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly StarDeckContext context;

        public DeckController(StarDeckContext context)
        {
            this.context = context;
        }
        // GET: api/<DeckController>
        [HttpGet]
        public IEnumerable<Deck> Get()
        {
            return context.Deck.ToList();
        }

        // GET api/<DeckController>/5
        [HttpGet("{id}")]
        public Deck Get(string id)
        {
            return context.Deck.FirstOrDefault(d => d.deck_id == id);
        }

        // POST api/<DeckController>
        [HttpPost]
        public ActionResult Post([FromBody] Deck deck)
        {
            try
            {
                context.Deck.Add(deck);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<DeckController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Deck deck)
        {
            if (deck.deck_id == id)
            {
                context.Entry(deck).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<DeckController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var deck = context.Deck.FirstOrDefault(d => d.deck_id == id);
            if (deck != null)
            {
                context.Deck.Remove(deck);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
