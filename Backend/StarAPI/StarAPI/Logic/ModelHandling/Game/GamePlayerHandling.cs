using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.GameLogic;
using StarAPI.Logic.Utils;
using StarAPI.DTOs;

namespace StarAPI.Logic.ModelHandling;

public class GamePlayerHandling
{
    private readonly StarDeckContext _context;
    private GameDeckHandling _gameDeckHandling;



    public GamePlayerHandling(StarDeckContext context)
    {
        this._context = context;
        this._gameDeckHandling = new GameDeckHandling(_context);
    }

    public void AddPlayers(string gameId, SetUpValues setUpValues)
    {   
        _gameDeckHandling.AddDecks(setUpValues);
        // Game_Player newGamePlayer = new Game_Player();
        // newGamePlayer.playerId = setUpValues.player1Id;
        // newGamePlayer.gameId = gameId;
        // newGamePlayer.gameDeckId = setUpValues.player1DeckId;
        // _context.Game_Player.Add(newGamePlayer);

        // newGamePlayer.playerId = setUpValues.player2Id;
        // newGamePlayer.gameId = gameId;
        // newGamePlayer.gameDeckId = setUpValues.player2DeckId;
        // _context.Game_Player.Add(newGamePlayer);
        // _context.SaveChanges();
    }

}