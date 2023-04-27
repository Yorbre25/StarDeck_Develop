using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Context;
using StarAPI.Models;
using StarAPI.Utils;


namespace StarAPI.Controllers
{
    /// <summary>
    /// This class is used to handle all requests of Card table.
    /// </summary>
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
 
        /// <summary>
        /// This method is used to get all cards from the Card table. 
        /// </summary>
        /// <returns>All cards</returns>
        // GET: api/<CardController>
        [HttpGet]
        public IEnumerable<Card> Get()
        {
            return context.Card.ToList();
        }


        /// <summary>
        /// This methos is used to get an specific card from the Card table.
        /// </summary>
        /// <param name="id">Id of card to get</param>
        /// <returns>Card with the same id</returns>
        // GET api/<CardController>/5
        [HttpGet("{id}")]
        public Card Get(string id)
        {
            return context.Card.FirstOrDefault(c => c.id == id);
        }

        /// <summary>
        /// This method is used to add a new card to the Card table.
        /// </summary>
        /// <param name="card"> Card to add</param>
        /// <returns></returns>
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
                if(card.image == "")
                {
                    card.image = "Imagen predeterminada";
                }
                context.Card.Add(card);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    
        /// <summary>
        /// This method is used to update a card in the Card table. 
        /// </summary>
        /// <param name="id">Id of card to update</param>
        /// <param name="card">New card data</param>
        /// <returns></returns>
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

        /// <summary>
        /// This method is used to delete a card from the Card table.
        /// </summary>
        /// <param name="id">Id of card to delte</param>
        /// <returns></returns>
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
