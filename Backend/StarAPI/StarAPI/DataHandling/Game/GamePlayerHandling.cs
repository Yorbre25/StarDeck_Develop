using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.Game;
using StarAPI.Logic.Utils;
using StarAPI.DTO.Discovery;

namespace StarAPI.DataHandling.Game;

public class GamePlayerHandling
{
    private readonly StarDeckContext _context;
    private GameDeckCardHandling _gameDeckCardHandling;

    private HandHandling _handHandling;
    private IdGenerator _idGenerator = new IdGenerator();
        private static string s_idPrefix = "GP";



    public GamePlayerHandling(StarDeckContext context)
    {
        this._context = context;
        this._gameDeckCardHandling = new GameDeckCardHandling(_context);
        this._handHandling = new HandHandling(_context);
    }

    public void SetupPlayer(string playerId, string deckId, string gameId)
    {   
        IsPlayerAvailable(playerId);
        _gameDeckCardHandling.SetupDeck(playerId, deckId, gameId);
        Game_Player newGamePlayer = new Game_Player();
        newGamePlayer.playerId = playerId;
        newGamePlayer.deckId = deckId;
        newGamePlayer.gameId = gameId;
        _context.Game_Player.Add(newGamePlayer);
        _context.SaveChanges();
    }

    private void IsPlayerAvailable(string playerId)
    {
        var gamePlayer = _context.Game_Player.FirstOrDefault(g => g.playerId == playerId);
        if (gamePlayer != null)
        {
            throw new ArgumentException("Player is currently in game");
        }
    }
    private bool IdAlreadyExists(string id)
    {
        Game_Player? gamePlayer;
        gamePlayer = _context.Game_Player.FirstOrDefault(c => c.playerId == id);
        if(gamePlayer == null){
            return false;
        }
        return true;
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

    internal void SetupHand(string gameId, string playerId)
    {
       _handHandling.SetupHand(gameId, playerId);
    }

    internal void EndGame(string gameId)
    {
        DeleteGamePlayers(gameId);
        _handHandling.EndGame(gameId);
        _gameDeckCardHandling.EndGame(gameId);
    }

    private void DeleteGamePlayers(string gameId)
    {
        _gameDeckCardHandling.EndGame(gameId);
        List<Game_Player> gamePlayers = _context.Game_Player.Where(gp => gp.gameId == gameId).ToList();
        _context.RemoveRange(gamePlayers);

        _context.SaveChanges();
    }

    internal List<OutputCard> GetHandCards(string gameId, string playerId)
    {
        return _handHandling.GetHandCardsByPlayerId(playerId);
    }

    internal OutputCard DrawCard(string gameId, string playerId)
    {
        return _handHandling.DrawCard(gameId, playerId);
    }
}