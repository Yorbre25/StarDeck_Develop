using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Mappers;
using StarAPI.Constants;
using StarAPI.DataHandling.Discovery;
using StarAPI.DTO.Game;
using StarAPI.DataHandling.Game;

namespace StarAPI.Logic;

public class SetUpPlayerForGame
{

    private readonly StarDeckContext _context;
    private GamePlayerMapper _gamePlayerMapper;

    public SetUpPlayerForGame(StarDeckContext context)
    {
        _context = context;
        _gamePlayerMapper = new GamePlayerMapper(_context);

    }

    internal void SetupPlayer(SetupValues setupValues, string gameId)
    {
        try
        {
            Setup(setupValues, gameId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    private void Setup(SetupValues setupValues, string gameId)
    {
        string[] players = new string[] { setupValues.player1Id, setupValues.player2Id };
        string[] decks = new string[] { setupValues.player1DeckId, setupValues.player2DeckId };
        for (int i = 0; i < players.Length; i++)
        {
            string playerId = players[i];
            string deckId = decks[i];
            AddPlayer(playerId, deckId, gameId);
        }
        _context.SaveChanges();
    }

    private void AddPlayer(string playerId, string deckId, string gameId)
    {
        IsPlayerAvailable(playerId);
        Game_Player newGamePlayer = _gamePlayerMapper.FillNewGamePlayer(gameId, playerId, deckId);
        _context.Game_Player.Add(newGamePlayer);
    }

    private void IsPlayerAvailable(string playerId)
    {
        var gamePlayer = _context.Game_Player.FirstOrDefault(g => g.playerId == playerId);
        if (gamePlayer != null)
        {
            throw new ArgumentException("Player is currently in game");
        }
    }
}