using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.Utils;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Mappers;
using StarAPI.Constants;
using Contracts;

namespace StarAPI.DataHandling.Game;

public class GamePlayerHandling
{
    private readonly IRepositoryWrapper _repository;
    private GameDeckHandling _gameDeckCardHandling;

    private IdGenerator _idGenerator = new IdGenerator();
        private static string s_idPrefix = "GP";



    public GamePlayerHandling(IRepositoryWrapper repository)
    {
        this._repository = repository;
        this._gameDeckCardHandling = new GameDeckHandling(_repository);
    }

    public Game_Player GetGamePlayerById(string playerId)
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

    public Game_Player GetGamePlayerByGameId(string gameId)
    {
        List<Game_Player> gamePlayers = _repository.GamePlayer.GetAll();
        return gamePlayers.FirstOrDefault(gp => gp.gameId == gameId);
    }
    public List<Game_Player> GetGamePlayersByGameId(string gameId)
    {
        List<Game_Player> gamePlayers = _repository.GamePlayer.GetAll();
        List<Game_Player> playersInGame = gamePlayers.Where(gp => gp.gameId == gameId).ToList();
        return playersInGame;

    }

    private Game_Player GettingGamePlayer(string playerId)
    {
        List<Game_Player> gamePlayers = _repository.GamePlayer.GetAll();
        return gamePlayers.FirstOrDefault(gp => gp.playerId == playerId);
    }

    internal int GetMaxCardPoints(string gameId)
    {
        // Game_Player gamePlayer = _repository.Game_Player.FirstOrDefault(gp => gp.gameId == gameId);
        GameHandling gameHandling = new GameHandling(_repository);
        Models.Game game = gameHandling.GetGame(gameId);
        Game_Player gamePlayer = GetGamePlayerById(game.player1Id);
        return gamePlayer.maxCardPoints;
    }
}