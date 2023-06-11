using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Game;
using StarAPI.DTO.Game;
using Microsoft.Extensions.Logging;

namespace StarAPI.Logic.Game;

public class GameLogic
{
    private readonly StarDeckContext _context;

    private GameHandling _gameHandling;
    private GameTableHandling _tableHandling;

    public GameLogic(StarDeckContext context)
    {
        _context = context;
        _gameHandling = new GameHandling(context);
        _tableHandling = new GameTableHandling(context);
    }
    internal OutputCard DrawCard(string gameId, string playerId)
    {
        try
        {
            return _gameHandling.DrawCard(gameId, playerId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }


    internal void EndTurn(InputTableLayout tableLayout)
    {
        bool shouldEndTurn = CheckIfBothPlayersPassed(tableLayout.gameId);
        _tableHandling.SetTableLayout(tableLayout);
        if (shouldEndTurn)
            _gameHandling.EndTurn(tableLayout);
    }

    private bool CheckIfBothPlayersPassed(string gameId)
    {
        int counter = _gameHandling.GetEndTurnCounter(gameId);
        bool shouldEndTurn = false;
        if (counter == 1)
        {
            _gameHandling.ResetEndTurnCounter(gameId);
            shouldEndTurn = true;
        }
        _gameHandling.DecreaseEndTurnCounter(gameId);
        return shouldEndTurn;
    }

    internal OutputTableLayout GetLayout(string gameId, string playerId)
    {
        return _gameHandling.GetLayout(gameId, playerId);
    }

    internal TurnInfo GetTurnInfo(string gameId, string playerId)
    {
        return _gameHandling.GetTurnInfo(gameId, playerId);
    }
}