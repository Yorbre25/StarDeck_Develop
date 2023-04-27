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
    public class CardController : ControllerBase
    {
        private readonly StarDeckContext context;
        private Encrypt encrypt = new Encrypt();

        public CardController(StarDeckContext context)
        {
            this.context = context;
        }
        // GET: api/<CardController>
        [HttpGet]
        public IEnumerable<Card> Get()
        {
            return context.Card.ToList();
        }

        // GET api/<CardController>/5
        [HttpGet("{id}")]
        public Card Get(string id)
        {
            return context.Card.FirstOrDefault(c => c.id == id);
        }

        // POST api/<CardController>
        [HttpPost]
        public ActionResult Post([FromBody] Card card)
        {
            try
            {
                string id = encrypt.gen_id("C");
                while (context.Player.FirstOrDefault(p => p.id == id) != null)
                {
                    id = encrypt.gen_id("C");
                }
                card.id = id;
                card.image = "imagen codificada";
                context.Card.Add(card);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/<CardController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Card card)
        {
            if (card.id == id)
            {
                context.Entry(card).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<CardController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var card = context.Card.FirstOrDefault(c => c.id == id);
            if (card != null)
            {
                context.Card.Remove(card);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }

}
