using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.Constants;
using StarAPI.DTO.Discovery;
using StarAPI.Models;
using Contracts;
using StarAPI.DataHandling.Game;

namespace StarAPI.Logic;

public class EndTurn
{

    private readonly IRepositoryWrapper _repository;
    // CardCRUD _cardCRUD;
    private int player1 = 0;
    private int player2 = 1;

    public EndTurn(IRepositoryWrapper repository)
    {
        _repository = repository;
        // _cardCRUD = new CardCRUD(_repository);
        
    }

    internal void End(InputTableLayout tableLayout)
    {
        try
        {
            EndAction(tableLayout);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    private void EndAction(InputTableLayout tableLayout)
    {
        PlaceCard placeCard = new PlaceCard(_repository);
        placeCard.Place(tableLayout);
        bool shouldEndTurn = ShouldEndTurn(tableLayout.gameId);
        if (shouldEndTurn)
        {
            EndTurnAction(tableLayout);
        }
    }

    private bool ShouldEndTurn(string gameId)
    {
        int counter = GetEndTurnCounter(gameId);
        bool shouldEndTurn;
        if (counter == 1)
        {
            ResetEndTurnCounter(gameId);
            shouldEndTurn = true;
        }
        else
        {
            DecreaseEndTurnCounter(gameId);
            shouldEndTurn = false;
        }
        return shouldEndTurn;
    }

    internal int GetEndTurnCounter(string gameId)
    {
        // Models.Game? game = _repository.Game.FirstOrDefault(g => g.id == gameId);
        Models.Game? game = GetGame(gameId);
        return game.endTurnCounter;
    }

    internal void ResetEndTurnCounter(string gameId)
    {
        // Models.Game? game = _repository.Game.FirstOrDefault(g => g.id == gameId);
        Models.Game? game = GetGame(gameId);
        game.endTurnCounter = Const.EndTurnCounter;
        // _repository.SaveChanges();
        _repository.Save();
    }

    internal void DecreaseEndTurnCounter(string gameId)
    {
        // Models.Game? game = _repository.Game.FirstOrDefault(g => g.id == gameId);
        Models.Game? game = GetGame(gameId);
        game.endTurnCounter--;
        // _repository.SaveChanges();
        _repository.Save();
    }

    internal void EndTurnAction(InputTableLayout tableLayout)
    {
        string gameId = tableLayout.gameId;
        string[] playerIds = GetPlayersIds(gameId);
        string player1Id = playerIds[player1];
        string player2Id = playerIds[player2];
        
        IncreaseCardPoints(player1Id);
        IncreaseCardPoints(player2Id);
        IncreaseTurn(gameId);
        
        // _repository.SaveChanges();
        _repository.Save();
    }


    private string[] GetPlayersIds(string gameId)
    {
        string[] playersIds = new string[2];
        // StarAPI.Models.Game? game = _repository.Game.FirstOrDefault(g => g.id == gameId);
        Models.Game? game = GetGame(gameId);
        playersIds[player1] = game.player1Id;
        playersIds[player2] = game.player2Id;
        return playersIds;
    }

    internal void IncreaseCardPoints(string playerId)
    {
        // Game_Player? gamePlayer = _repository.Game_Player.FirstOrDefault(gp => gp.playerId == playerId);
        Game_Player? gamePlayer = GetGamePlayer(playerId);
        gamePlayer.maxCardPoints += Const.ExtraCardPointsPerTurn;
        gamePlayer.cardPoints = gamePlayer.maxCardPoints;
    }
    private void IncreaseTurn(string gameId)
    {
        // Models.Game? game = _repository.Game.FirstOrDefault(g => g.id == gameId);
        Models.Game? game = GetGame(gameId);
        game.turn++;
    }

    private Models.Game GetGame(string gameId)
    {
        GameHandling gameHandling = new GameHandling(_repository);
        return gameHandling.GetGame(gameId);
    }

    private Game_Player GetGamePlayer(string playerId)
    {
        GamePlayerHandling gamePlayerHandling = new GamePlayerHandling(_repository);
        return gamePlayerHandling.GetGamePlayerById(playerId);
    }
}