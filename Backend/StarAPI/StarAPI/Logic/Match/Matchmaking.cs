using Microsoft.AspNetCore.Mvc;
using StarAPI.Context;
using StarAPI.Models;
using StarAPI.Logic.Utils;
using StarAPI.DTO.Game;
using StarAPI.DataHandling.Game;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using Contracts;

namespace StarAPI.Logic.Match
{
    public class Matchmaking
    {
        private readonly IRepositoryWrapper _repository;
        private CancelRequest cancel;
        // private GameHandling gameHandling;
        // private PlayerHandling playerHandling;
        private NewGame _newGame;
        public Matchmaking (IRepositoryWrapper context) 
        {
            this._repository = context;
            this.cancel = CancelRequest.Instance;
            this._newGame = new NewGame(_repository);
        }
      
        private List<Player> getMatchedPlayers(string id) 
        {
            // int rank = _repository.Player.FirstOrDefault(p => p.id == id).ranking;
            // var playersWaiting = _repository.Match_Player.OrderBy(p => p.waiting_since).ToList();
            // var playersMatched = _repository.Player.ToList();
            int rank = _repository.Player.Get(id).ranking;
            var playersWaiting = _repository.MatchPlayer.GetAll().OrderBy(p => p.waiting_since).ToList();
            var playersMatched = _repository.Player.GetAll().ToList();
            List<Player> intersec = playersMatched.Where(pm => playersWaiting.Any(pw => pw.id == pm.id)).ToList();
            intersec = intersec.FindAll(i => i.ranking == rank);

            return intersec;

        }

        public bool match(string id, string deckId) 
        {
            
            Match_Player  match_player = new Match_Player();
            match_player.id = id;
            match_player.deckId = deckId;
            match_player.waiting_since = DateTime.Now;
            submit(match_player);
            var players = getMatchedPlayers(id);
            int min_player = 2;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (players.Count < min_player)
            {
                Thread.Sleep(1000);
                players = getMatchedPlayers(id);

                if (cancel.terminate || stopwatch.Elapsed > TimeSpan.FromSeconds(20)) 
                {
                    remove(id);
                    return false;
                }
                if (players.Count == 0) 
                {
                    remove(id);
                    return true;
                }
            }

            var player = players.FirstOrDefault(p=> p.id != id);
            SetupValues sv = new SetupValues();
            sv.player1Id = id;
            sv.player2Id = player.id;
            sv.player1DeckId = deckId;
            // sv.player2DeckId = _repository.Match_Player.FirstOrDefault(p=>p.id == player.id).deckId;
            sv.player2DeckId = _repository.MatchPlayer.Get(player.id).deckId;

            AddGame(sv);
            // _repository.SaveChanges();
            _repository.Save();

            // update(player, players.FirstOrDefault(p1 => p1.id == id),true);
            remove(id, player.id);

            return true;
        }

        protected void remove(string player1, string player2) 
        {
            // var p1 = _repository.Match_Player.FirstOrDefault(p => p.id == player1);
            // var p2 = _repository.Match_Player.FirstOrDefault(p => p.id == player2);
            var p1 = _repository.MatchPlayer.Get(player1);
            var p2 = _repository.MatchPlayer.Get(player2);
            if (p1 != null && p2 != null)
            {
                // _repository.Match_Player.Remove(p1);
                // _repository.SaveChanges();
                // _repository.Match_Player.Remove(p2);
                // _repository.SaveChanges();

                _repository.MatchPlayer.Delete(p1);
                _repository.Save();
                _repository.MatchPlayer.Delete(p2);
                _repository.Save();
            }
        }

        protected bool submit([FromBody] Match_Player match_player) 
        {
            try
            {
                // _repository.Match_Player.Add(match_player);
                // _repository.SaveChanges();
                _repository.MatchPlayer.Add(match_player);
                _repository.Save();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public void AddGame(SetupValues sv)
        {
            
            _newGame.SetupNewGame(sv);


           
        }

        // protected void update(string idPlayer1, string idPlayer2, bool state)
        // {
            
        //     player1.inGame = state;
        //     player2.inGame = state;
        //     _repository.Entry(player1).State = EntityState.Modified;
        //     _repository.Entry(player2).State = EntityState.Modified;
        //     _repository.SaveChanges();

        // }

        protected void remove(string id)
        {
            // var player = _repository.Match_Player.FirstOrDefault(p => p.id == id);
            var player = _repository.MatchPlayer.Get(id);
            if (player != null)
            {
                // _repository.Match_Player.Remove(player);
                // _repository.SaveChanges();
                _repository.MatchPlayer.Delete(player);
                _repository.Save();
            }
        }

    }
}
