using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.Constants;
using StarAPI.DTO.Discovery;
using StarAPI.Models;

namespace StarAPI.Logic;

public class EndTurn
{

    private readonly StarDeckContext _context;
    CardCRUD _cardCRUD;

    public EndTurn(StarDeckContext context)
    {
        _context = context;
        _cardCRUD = new CardCRUD(_context);
        
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
        PlaceCard placeCard = new PlaceCard(_context);
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
        Models.Game? game = _context.Game.FirstOrDefault(g => g.id == gameId);
        return game.endTurnCounter;
    }

    internal void ResetEndTurnCounter(string gameId)
    {
        Models.Game? game = _context.Game.FirstOrDefault(g => g.id == gameId);
        game.endTurnCounter = Const.EndTurnCounter;
        _context.SaveChanges();
    }

    internal void DecreaseEndTurnCounter(string gameId)
    {
        Models.Game? game = _context.Game.FirstOrDefault(g => g.id == gameId);
        game.endTurnCounter--;
        _context.SaveChanges();
    }

    internal void EndTurnAction(InputTableLayout tableLayout)
    {
        string gameId = tableLayout.gameId;
        string[] playerIds = GetPlayersIds(gameId);
        string player1Id = playerIds[0];
        string player2Id = playerIds[1];
        
        IncreaseCardPoints(player1Id);
        IncreaseCardPoints(player2Id);
        IncreaseTurn(gameId);
        
        _context.SaveChanges();
    }


    private string[] GetPlayersIds(string gameId)
    {
        string[] playersIds = new string[2];
        StarAPI.Models.Game? game = _context.Game.FirstOrDefault(g => g.id == gameId);
        playersIds[0] = game.player1Id;
        playersIds[1] = game.player2Id;
        return playersIds;
    }

    internal void IncreaseCardPoints(string playerId)
    {
        Game_Player? gamePlayer = _context.Game_Player.FirstOrDefault(gp => gp.playerId == playerId);
        gamePlayer.maxCardPoints += Const.ExtraCardPointsPerTurn;
        gamePlayer.cardPoints = gamePlayer.maxCardPoints;
    }
    private void IncreaseTurn(string gameId)
    {
        Models.Game? game = _context.Game.FirstOrDefault(g => g.id == gameId);
        game.turn++;
    }
}