using StarAPI.Context;
using StarAPI.Models;
using StarAPI.DTO.Game;
using StarAPI.Constants;

namespace StarAPI.Logic.Mappers
{
    public class GamePlayerMapper
    {
        private StarDeckContext _context;

        public GamePlayerMapper(StarDeckContext context)
        {
            this._context = context;
        }
        
        // public Game_Player FillNewGamePlayer(InputGamePlayer inputGamePlayer)
        public Game_Player FillNewGamePlayer(string gameId, string playerId, string deckId)
        {
            Game_Player player = new Game_Player()
            {
                gameId = gameId,
                playerId = playerId,
                deckId = deckId,
                cardPoints = Const.InitialCardPoints,
                maxCardPoints = Const.InitialCardPoints
            };
            return player;
        }
    }

}