using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.Models;
using StarAPI.Constants;

namespace StarAPI.Logic;

public class EndGame
{

    private readonly StarDeckContext _context;


    public EndGame(StarDeckContext context)
    {
        _context = context;
    }

    internal WinnerInfo endGame(string gameId)
    {
        try
        {
            return End(gameId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    private WinnerInfo End(string gameId)
    {
        bool shouldEndGame = CheckIfBothPlayersEndGame(gameId);
        string winner = Winner(gameId);
        if(shouldEndGame){
            IncreaseWins(winner, gameId);
            RemoveGame(gameId);
        }

        WinnerInfo winnerInfo = new WinnerInfo();
        winnerInfo.winnerId = winner;
        winnerInfo.xpGain = 0;
        return winnerInfo; 
    }

    private void IncreaseWins(string winner, string gameId)
    {
        if (winner != Const.Tie)
        {
            Models.Game? game = _context.Game.FirstOrDefault(g => g.id == gameId);
            new PlayerCRUD(_context).IncreaseWins(winner, game.xpGain);
        }
    }

    private void RemoveGame(string gameId)
    {
        StarAPI.Models.Game? game = _context.Game.FirstOrDefault(g => g.id == gameId);
        _context.Game.Remove(game);
        RemoveCardsInTable(gameId);
        RemoveGamePlayers(gameId);
        RemoveHand(gameId);
        RemoveDeck(gameId);

        _context.SaveChanges();
    }

    private string Winner(string gameId)
    {
        WinnerDeclaration winnerDeclaration = new WinnerDeclaration(_context);
        return winnerDeclaration.Winner(gameId);
    }

    private bool CheckIfBothPlayersEndGame(string gameId)
    {
       int counter = GetEndGameCounter(gameId);
       bool shouldEndGame = false;
       if (counter == 1)
       {
           shouldEndGame = true;
       }
       DecreaseEndGameCounter(gameId);
       return shouldEndGame;
    }

    private void DecreaseEndGameCounter(string gameId)
    {
        Models.Game? game = _context.Game.FirstOrDefault(g => g.id == gameId);
        game.endGameCounter--;
        _context.SaveChanges();
    }

    private int GetEndGameCounter(string gameId)
    {
        return _context.Game.FirstOrDefault(g => g.id == gameId).endGameCounter;
    }
        private void RemoveDeck(string gameId)
    {
        List<Game_Deck> cards = _context.Game_Deck.Where(c => c.gameId == gameId).ToList();
        _context.Game_Deck.RemoveRange(cards);
    }

    private void RemoveHand(string gameId)
    {
        List<Hand> cards = _context.Hand.Where(h => h.gameId == gameId).ToList();
        _context.Hand.RemoveRange(cards);
    }

    private void RemoveGamePlayers(string gameId)
    {
        List<Game_Player> gamePlayers = _context.Game_Player.Where(gp => gp.gameId == gameId).ToList();
        _context.RemoveRange(gamePlayers);
    }

    private void RemoveCardsInTable(string gameId)
    {
        List<GameTable> cards = _context.GameTable.Where(gt => gt.gameId == gameId).ToList();
        _context.GameTable.RemoveRange(cards);
    }
}