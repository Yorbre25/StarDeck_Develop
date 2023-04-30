using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using StarAPI.DTOs;
using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic;

namespace StarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardAssignController : ControllerBase
    {
        private readonly StarDeckContext context;
        private CardPackageGenerator _cardPackageGenerator;
        public CardAssignController(StarDeckContext context)
        {
            this.context = context;
            this._cardPackageGenerator = new CardPackageGenerator(context);
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
                all_cards = all_cards.FindAll(c => c.typeId == 1);
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

        [HttpGet("GetPackagesForNewPlayer")]
        public List<List<OutputCard>> GetPackagesForNewPlayer() 
        {
            try
            {
                return this._cardPackageGenerator.GetPackagesForNewPlayer();
            }
            catch (Exception e)
            {
                return new List<List<OutputCard>>();
            }    
        }
        

        /// <summary>
        /// This method saves a new player_card
        /// </summary>
        /// <param name="player_card">Id of player and Id of card to be added</param>
        /// <returns></returns>
        [HttpPost("AddCardToPlayer")]
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



        // [HttpGet("GetRandomCardWithType/{cardTypeName}")]
        // public OutputCard GetRandomCardWith(string cardTypeName){
        //     // return _cardGenerator.GetRandomCardWith(cardTypeName);
        // }
    }
}
