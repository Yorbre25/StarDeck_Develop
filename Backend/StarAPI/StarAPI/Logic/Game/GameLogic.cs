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

    public OutputSetupValues SetUpGame(SetupValues setUpValues)
    {
        try
        {
            OutputSetupValues outputSetupValues = _gameHandling.SetUpGame(setUpValues);
            return outputSetupValues;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    internal void SetupHands(string gameId)
    {
        try
        {
            _gameHandling.SetupHands(gameId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    internal List<OutputCard> GetHandCards(string gameId, string playerId)
    {
        try
        {
            return _gameHandling.GetHandCards(gameId, playerId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
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

    public WinnerInfo EndGame(string gameId)
    {
        try
        {
            bool shouldEndGame = CheckIfBothPlayersEndGame(gameId);
            return _gameHandling.EndGame(gameId, shouldEndGame);
        }
        catch (System.Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    private bool CheckIfBothPlayersEndGame(string gameId)
    {
       int counter = _gameHandling.GetEndGameCounter(gameId);
       bool shouldEndGame = false;
       if (counter == 1)
       {
           shouldEndGame = true;
       }
       _gameHandling.DecreaseEndGameCounter(gameId);
        return shouldEndGame;
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