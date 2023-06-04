using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StarAPI.DataHandling.Game
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly StarDeckContext context;
        public GameController(StarDeckContext context)
        {
            this.context = context;
        }

        // GET: api/<GameController>
        [HttpGet("GetGames")]
        public IEnumerable<Models.Game> GetGames()
        {
            var dd = context.Set<Game_Planet>();
            context.RemoveRange(dd);
            context.SaveChanges();
            return context.Game.ToList();
        }

        // GET api/<GameController>/5
        [HttpGet("GetPlayers")]
        public IEnumerable<Models.Game_Player> GetPlayers()
        {
            return context.Game_Player.ToList();
        }

        [HttpGet("GetDecks")]
        public IEnumerable<Models.Game_Deck> GetDecks()
        {
            return context.Game_Deck.ToList();
        }

        [HttpGet("GetTables")]
        public IEnumerable<Models.Game_Planet> GetPlanets()
        {
            return context.Game_Planet.ToList();
        }


        // DELETE api/<GameController>/5
        [HttpDelete("{gameId}")]
        public void DeleteAll(string gameId)
        {
            var game = context.Game.FirstOrDefault(g=> g.id == gameId);
            var players = context.Game_Player.ToList();
            players = players.FindAll(p => p.gameId == gameId);
            var decks = context.Game_Deck.ToList();
            decks = decks.FindAll(d => d.gameId == gameId);
            var tables = context.Game_Planet.ToList();
            tables = tables.FindAll(t => t.gameId == gameId);
            context.Remove(game);
            context.RemoveRange(players);
            context.RemoveRange(decks);
            context.RemoveRange(tables);
            context.SaveChanges();
        }
    }
}
