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

    // internal OutputTableLayout GetLayout(string gameId, string playerId)
    // {
    //     return _gameHandling.GetLayout(gameId, playerId);
    // }

    internal TurnInfo GetTurnInfo(string gameId, string playerId)
    {
        return _gameHandling.GetTurnInfo(gameId, playerId);
    }
}