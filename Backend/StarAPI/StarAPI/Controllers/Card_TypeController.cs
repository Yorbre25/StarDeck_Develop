﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarAPI.Context;
using StarAPI.Models;

namespace StarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Card_TypeController : ControllerBase
    {
        private readonly StarDeckContext context;

        public Card_TypeController(StarDeckContext context)
        {
            this.context = context;
        }

        // GET: api/<Card_TypeController>
        [HttpGet]
        public IEnumerable<Card_Type> Get()
        {
            return context.Card_Type.ToList();
        }

        // GET api/<Card_TypeController>/5
        [HttpGet("{id}")]
        public Card_Type Get(string id)
        {
            return context.Card_Type.FirstOrDefault(c => c.type == id);
        }

        // POST api/<Card_TypeController>
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

        // PUT api/<Card_TypeController>/5
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

        // DELETE api/<Card_TypeController>/5
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
