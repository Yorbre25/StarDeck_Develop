using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using StarAPI.Context;
using StarAPI.Models;
using System;

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


        [HttpGet]
        public IEnumerable<Player_Card> Get() 
        {
            return context.Player_Card.ToList();
        }

        // GET: api/<Player_CardController>
        [HttpPost("{player_id}")]
        public IEnumerable<Card> Post( string player_id)
        {
            try 
            {
                var player = context.Player.FirstOrDefault(p => p.id == player_id || p.email == player_id);
                player_id = player.id;
                var all_cards = context.Card.ToList();
                all_cards = all_cards.FindAll(c => c.type == "Basic");
                Random random = new Random();
                HashSet<int> uniques = new HashSet<int>();
                while(uniques.Count < 15) 
                {
                    uniques.Add(random.Next(0,all_cards.Count));
                }
                List<int> uniquesIndexes = uniques.ToList();
                List<Card> cards = new List<Card>();
                foreach(var u in uniquesIndexes)
                {
                    var card = new Player_Card();
                    card.card_id = all_cards[u].id;
                    card.player_id = player_id;
                    Post(card);
                    cards.Add(all_cards[u]);

                }
                return cards;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        [HttpGet("player_id")]
        public IEnumerable<Card> Get(string player_id) 
        {
            try 
            {
                var player = context.Player.FirstOrDefault(p => p.id == player_id || p.email == player_id);
                var cards_id = context.Player_Card.ToList();
                cards_id = cards_id.FindAll(c => c.player_id == player_id || player.email == player_id);
                var cards = new List<Card>();
                foreach (var card in cards_id)
                {
                    cards.Add(context.Card.FirstOrDefault(c => c.id == card.card_id));
                }
                return cards;
            }
            catch 
            {
                return null;
            }
            
        }
        [HttpGet("{player_id}/{n}/{type}")]
        public IEnumerable<Card> Get(string player_id, int n, string type) 
        {
            try 
            {
                var cards = context.Card.ToList();
                cards = cards.FindAll(c => c.type != type);
                Random random = new Random();
                HashSet<int> uniques = new HashSet<int>();
                while (uniques.Count < n)
                {
                    uniques.Add(random.Next(0, cards.Count));
                }
                List<int> uniquesIndexes = uniques.ToList();
                List<Card> sel_cards = new List<Card>();
                foreach (var u in uniquesIndexes)
                {
                    sel_cards.Add(cards[u]);

                }

                return sel_cards;
            }
            catch 
            {
                return null;
            }
           
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
