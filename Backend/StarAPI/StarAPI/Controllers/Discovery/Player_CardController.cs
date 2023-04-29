using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using System;

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

        /// <summary>
        /// This method returns the number of cards that a player has
        /// </summary>
        /// <param name="player_id"> Id of player</param>
        /// <returns></returns>
        [HttpGet("card_count/{player_id}")]
        public int GetCardCount(string player_id)
        {   
            // check if player exists
            var player = context.Player.FirstOrDefault(p => p.id == player_id);
            if (player == null)
            {
                return -1;
            }
            var player_cards = Get(player_id);
            return player_cards.Count();
        }
       

        /// <summary>
        /// This method assigns 15 basic cards to a player
        /// </summary>
        /// <param name="player_id">Id of player</param>
        /// <returns></returns>
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

        /// <summary>
        /// This method returns the cards of a player
        /// </summary>
        /// <param name="player_id"></param>
        /// <returns></returns>
        [HttpGet("{player_id}")]
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

        /// <summary>
        /// This method returns n Normal or Rare random cards that a player doesn't have
        /// </summary>
        /// <param name="player_id">Id fo player</param>
        /// <param name="n">Number of cards</param>
        /// <returns></returns>
        [HttpGet("{player_id}/{n}")]
        public IEnumerable<Card> Get(string player_id, int n) 
        {
            try 
            {
                var player_cards = Get(player_id);
                var cards = context.Card.ToList();
                
                cards = cards.FindAll(c => (c.type == "Normal" || c.type == "Rara"));
                // Delete cards that player already have
                foreach(var card in cards)
                {
                    if (player_cards.Contains(card))
                    {
                        cards.Remove(card);
                    }
                }
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
        

        /// <summary>
        /// This method saves a new player_card
        /// </summary>
        /// <param name="player_card">Id of player and Id of card to be added</param>
        /// <returns></returns>
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

        /// <summary>
        /// This method updates a player_card
        /// </summary>
        /// <param name="player_id">Id of player who owns the card</param>
        /// <param name="card_id">Id of card to be updated</param>
        /// <param name="player_card">New player_card</param>
        /// <returns></returns>
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

        /// <summary>
        /// This method deletes a player_card
        /// </summary>
        /// <param name="player_id">Id of player who owns the card</param>
        /// <param name="card_id">Id of card to be deleted</param>
        /// <returns></returns>
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
