using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.Utils;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Mappers;
using StarAPI.Constants;

namespace StarAPI.DataHandling.Game;

public class GamePlayerHandling
{
    private readonly StarDeckContext _context;
    private GameDeckCardHandling _gameDeckCardHandling;
    private GamePlayerMapper _gamePlayerMapper;

    private HandHandling _handHandling;
    private IdGenerator _idGenerator = new IdGenerator();
        private static string s_idPrefix = "GP";



    public GamePlayerHandling(StarDeckContext context)
    {
        this._context = context;
        this._gameDeckCardHandling = new GameDeckCardHandling(_context);
        this._handHandling = new HandHandling(_context);
        this._gamePlayerMapper = new GamePlayerMapper(_context);
    }

    public Game_Player GetGamePlayer(string playerId)
    {
        try
        {
            return GettingGamePlayer(playerId);
        }
        catch (System.Exception e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    private Game_Player GettingGamePlayer(string playerId)
    {
        Game_Player? gamePlayer = _context.Game_Player.FirstOrDefault(g => g.playerId == playerId);
        return gamePlayer;
    }

    internal int GetMaxCardPoints(string gameId)
    {
        Game_Player gamePlayer = _context.Game_Player.FirstOrDefault(gp => gp.gameId == gameId);
        return gamePlayer.maxCardPoints;
    }
}