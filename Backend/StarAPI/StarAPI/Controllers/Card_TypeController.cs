using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Context;
using StarAPI.Models;

namespace StarAPI.Controllers
{
    /// <summary>
    /// This class is used to handle all requests to the Card_Type table.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class Card_TypeController : ControllerBase
    {
        private readonly StarDeckContext context;

        public Card_TypeController(StarDeckContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// This method is used to get all card types from the Card_Type table.
        /// </summary>
        /// <returns> </returns>
        [HttpGet]
        public IEnumerable<Card_Type> Get()
        {
           return context.Card_Type.ToList();
        }

        /// <summary>
        /// This method is used to get a card type from the Card_Type table.
        /// </summary>
        /// <param name="id"> Id of card type to be searched </param>
        /// <returns>card type found</returns>
        [HttpGet("{id}")]
        public Card_Type Get(string id)
        {
            return context.Card_Type.FirstOrDefault(c => c.type == id);
        }


        // POST api/<Card_TypeController>
        /// <summary>
        /// This method is used to add a card type to the Card_Type table.
        /// </summary>
        /// <param name="card_type"> Name of new card type</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] Card_Type card_type)
        {
            try
            {
                context.Card_Type.Add(card_type);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // This method is used to update a card type in the Card_Type table.
        // PUT api/<Card_TypeController>/5
        /// <summary>
        /// This method is used to update a card type in the Card_Type table.
        /// </summary>
        /// <param name="id">Id of card type to be updated  </param>
        /// <param name="card_type"> New card type name</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Card_Type card_type)
        {
            if (card_type.type == id)
            {
                context.Entry(card_type).State = EntityState.Modified;
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        // This method is used to delete a card type from the Card_Type table.
        // DELETE api/<Card_TypeController>/5
        /// <summary>
        /// Deteles a card type from the Card_Type table.
        /// </summary>
        /// <param name="id">Id of card type to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var card_type = context.Card_Type.FirstOrDefault(c => c.type == id);
            if (card_type != null)
            {
                context.Card_Type.Remove(card_type);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
