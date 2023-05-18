using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.GameLogic;
using StarAPI.Logic.Utils;
using StarAPI.DTOs;

namespace StarAPI.Logic.ModelHandling;

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

    public void AddPlayer(string playerId, string deckId)
    {   
        IsPlayerAvailable(playerId);
        _gameDeckCardHandling.AddDecks(playerId, deckId);
        Game_Player newGamePlayer = new Game_Player();
        newGamePlayer.playerId = playerId;
        newGamePlayer.deckId = deckId;
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

    private string GenerateId()
    {
        string id = "";
        bool alreadyExists = true;
        while (alreadyExists)
        {
            id = _idGenerator.GenerateId(s_idPrefix);
            alreadyExists = IdAlreadyExists(id);
        }
        return id;
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

    internal void EndGame(string[] gamePlayerIds)
    {
        EndPlayers(gamePlayerIds);
        // _gameDeckCardHandling.EndGame();
    }

    private void EndPlayers(string[] gamePlayerIds)
    {
        Game_Player gamePlayer = GetGamePlayer(gamePlayerIds[0]);

        string deckId = gamePlayer.deckId;
        _gameDeckCardHandling.EndGame(deckId);
        // DeleteGamePlayer(gamePlayer);

        gamePlayer = GetGamePlayer(gamePlayerIds[1]);
        deckId = gamePlayer.deckId;
        _gameDeckCardHandling.EndGame(deckId);
        // DeleteGamePlayer(gamePlayer);
        _context.SaveChanges();
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

    internal void SetupHands(string gameId, string playerId)
    {
       _handHandling.SetupHand(playerId);
    }

    internal void Delete(string playerId)
    // internal List<Hand_Card> Delete(string playerId)
    {
        Game_Player gamePlayer = GetGamePlayer(playerId);
        DeleteGamePlayer(gamePlayer);
        _handHandling.Delete(playerId);
        _gameDeckCardHandling.Delete(gamePlayer.deckId);
    }

    private void DeleteGamePlayer(Game_Player gamePlayer)
    {
        _context.Game_Player.Remove(gamePlayer);
    }

    internal List<OutputCard> GetHandCards(string gameId, string playerId)
    {
        return _handHandling.GetHandCardsByPlayerId(playerId);
    }
}