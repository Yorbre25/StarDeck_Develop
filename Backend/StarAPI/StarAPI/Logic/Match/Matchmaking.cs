using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.Models;
using StarAPI.Logic.Utils;

namespace StarAPI.Logic.Match
{
    public class Matchmaking
    {
        private readonly StarDeckContext _context;
        private CancelRequest cancel;
        public Matchmaking (StarDeckContext context) 
        {
            this._context = context;
            this.cancel = CancelRequest.Instance;
        }
        
      
        private List<Player> getMatchedPlayers(string id) 
        {
            var rank = _context.Player.FirstOrDefault(p => p.id == id).ranking;
            var playersWaiting = _context.Match_Player.OrderBy(p=>p.waiting_since);
            var playersMatched = _context.Player.Join(playersWaiting, p1 => p1.id, p2 => p2.id, (p1,p2) => p1)
                .ToList().Where(p=>p.ranking == rank).ToList();
            
            return playersMatched;

        }

        public bool match(string id) 
        {
            Match_Player  match_player = new Match_Player();
            match_player.id = id;
            match_player.waiting_since = DateTime.Now;
            submit(match_player);

            var players = getMatchedPlayers(id);
            while(players.Count < 2)
            {
                players = getMatchedPlayers(id);
                if (cancel.terminate) 
                {
                    return false;
                }
            }
            var player = players.FirstOrDefault(p=> p.id != id);

            //add both players to a game
            //...
            //..
            //.
            remove(id, player.id);
            return true;
        }

        protected void remove(string player1, string player2) 
        {
            var p1 = _context.Match_Player.FirstOrDefault(p => p.id == player1);
            var p2 = _context.Match_Player.FirstOrDefault(p => p.id == player2);
            if (p1 != null && p2 != null)
            {
                _context.Match_Player.Remove(p1);
                _context.Match_Player.Remove(p2);
                _context.SaveChanges();
            }
        }

        protected bool submit([FromBody] Match_Player match_player) 
        {
            try
            {
                _context.Match_Player.Add(match_player);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
    }
}
