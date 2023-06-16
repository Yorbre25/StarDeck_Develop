using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.Models;
using StarAPI.Constants;
using Contracts;
using StarAPI.DataHandling.Game;

namespace StarAPI.Logic;

public class EndGame
{

    private readonly IRepositoryWrapper _repository;


    public EndGame(IRepositoryWrapper context)
    {
        _repository = context;
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
            // Models.Game? game = _repository.Game.FirstOrDefault(g => g.id == gameId);
            Models.Game game = GetGame(gameId);
            new PlayerCRUD(_repository).IncreaseWins(winner, game.xpGain);
        }
    }

    private Models.Game GetGame(string gameId)
    {
        GameHandling gameHandling = new GameHandling(_repository);
        return gameHandling.GetGame(gameId);
    }

    private void RemoveGame(string gameId)
    {
        // StarAPI.Models.Game? game = _repository.Game.FirstOrDefault(g => g.id == gameId);
        // _repository.Game.Remove(game);
        Models.Game game = GetGame(gameId);
        _repository.Game.Delete(game);
        RemoveCardsInTable(gameId);
        RemovePlanets(gameId);
        RemoveGamePlayers(gameId);
        RemoveHand(gameId);
        RemoveDeck(gameId);

        // _repository.SaveChanges();
        _repository.Save();
    }

  

    private string Winner(string gameId)
    {
        WinnerDeclaration winnerDeclaration = new WinnerDeclaration(_repository);
        return winnerDeclaration.GetWinner(gameId);
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
        // Models.Game? game = _repository.Game.FirstOrDefault(g => g.id == gameId);
        Models.Game? game = _repository.Game.Get(gameId);
        game.endGameCounter--;
        // _repository.SaveChanges();
        _repository.Save();
    }

    private int GetEndGameCounter(string gameId)
    {
        // return _repository.Game.FirstOrDefault(g => g.id == gameId).endGameCounter;
        return _repository.Game.Get(gameId).endGameCounter;
    }
        private void RemoveDeck(string gameId)
    {
        // List<Game_Deck> cards = _repository.Game_Deck.Where(c => c.gameId == gameId).ToList();
        // _repository.Game_Deck.RemoveRange(cards);
        GameDeckHandling gameDeckHandling = new GameDeckHandling(_repository);
        List<Game_Deck> cardsInDeck = gameDeckHandling.GetGameDeckByGameId(gameId);
        _repository.GameDeck.Delete(cardsInDeck);
    }

    private void RemoveHand(string gameId)
    {
        // List<Hand> cards = _repository.Hand.Where(h => h.gameId == gameId).ToList();
        // _repository.Hand.RemoveRange(cards);
        HandCard handHandling = new HandCard(_repository);
        List<Hand> cardsInDeck = handHandling.GetHandByGameId(gameId);
        _repository.Hand.Delete(cardsInDeck);
    }

    private void RemoveGamePlayers(string gameId)
    {
        // List<Game_Player> gamePlayers = _repository.Game_Player.Where(gp => gp.gameId == gameId).ToList();
        // _repository.RemoveRange(gamePlayers);
        GamePlayerHandling gamePlayerHandling = new GamePlayerHandling(_repository);
        List<Game_Player> gamePlayer = gamePlayerHandling.GetGamePlayersByGameId(gameId);
        _repository.GamePlayer.Delete(gamePlayer);

    }

    private void RemoveCardsInTable(string gameId)
    {
        // List<GameTable> cards = _repository.GameTable.Where(gt => gt.gameId == gameId).ToList();
        // _repository.GameTable.RemoveRange(cards);
        GameTableHandling gameTableHandling = new GameTableHandling(_repository);
        List<GameTable> cardsInDeck = gameTableHandling.GetPlayerCardsInTable(gameId);
        _repository.GameTable.Delete(cardsInDeck);
    }

    private void RemovePlanets(string gameId)
    {
        GameTableHandling gameTableHandling = new GameTableHandling(_repository);
        List<Game_Planet> planets = gameTableHandling.GetGamePlanetsByGameId(gameId);
        _repository.GamePlanet.Delete(planets);
    }
}