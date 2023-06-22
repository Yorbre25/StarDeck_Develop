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
        winnerInfo.xpGain = setXPGain(winner);
        return winnerInfo; 
    }

    private int setXPGain(string winner)
    {
        if (winner == Const.Tie)
        {
            return 0;
        }
        return Const.XpGain;
    }

    private void IncreaseWins(string winner, string gameId)
    {
        if (winner != Const.Tie)
        {
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
        Models.Game game = GetGame(gameId);
        _repository.Game.Delete(game);
        RemoveCardsInTable(gameId);
        RemovePlanets(gameId);
        RemoveGamePlayers(gameId);
        RemoveHand(gameId);
        RemoveDeck(gameId);

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
        Models.Game? game = _repository.Game.Get(gameId);
        game.endGameCounter--;
        _repository.Save();
    }

    private int GetEndGameCounter(string gameId)
    {
        return _repository.Game.Get(gameId).endGameCounter;
    }
        private void RemoveDeck(string gameId)
    {
        GameDeckHandling gameDeckHandling = new GameDeckHandling(_repository);
        List<Game_Deck> cardsInDeck = gameDeckHandling.GetGameDeckByGameId(gameId);
        _repository.GameDeck.Delete(cardsInDeck);
    }

    private void RemoveHand(string gameId)
    {
        HandCard handHandling = new HandCard(_repository);
        List<Hand> cardsInDeck = handHandling.GetHandByGameId(gameId);
        _repository.Hand.Delete(cardsInDeck);
    }

    private void RemoveGamePlayers(string gameId)
    {
        GamePlayerHandling gamePlayerHandling = new GamePlayerHandling(_repository);
        List<Game_Player> gamePlayer = gamePlayerHandling.GetGamePlayersByGameId(gameId);
        _repository.GamePlayer.Delete(gamePlayer);

    }

    private void RemoveCardsInTable(string gameId)
    {
        GameBoardHandling gameTableHandling = new GameBoardHandling(_repository);
        List<GameTable> cardsInDeck = gameTableHandling.GetPlayerCardsInTable(gameId);
        _repository.GameTable.Delete(cardsInDeck);
    }

    private void RemovePlanets(string gameId)
    {
        GameBoardHandling gameTableHandling = new GameBoardHandling(_repository);
        List<Game_Planet> planets = gameTableHandling.GetGamePlanetsByGameId(gameId);
        _repository.GamePlanet.Delete(planets);
    }
}