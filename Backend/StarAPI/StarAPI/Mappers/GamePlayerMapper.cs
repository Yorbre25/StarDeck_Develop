using StarAPI.Context;
using StarAPI.Models;
using StarAPI.DTO.Game;
using StarAPI.Constants;

namespace StarAPI.Logic.Mappers
{
    public class GamePlayerMapper
    {

        public GamePlayerMapper()
        {
        }
        
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