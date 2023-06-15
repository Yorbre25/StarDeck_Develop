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
using Contracts;

namespace StarAPI.Logic;

public class SetUpPlayerForGame
{

    private readonly IRepositoryWrapper _repository;
    private GamePlayerMapper _gamePlayerMapper;

    public SetUpPlayerForGame(IRepositoryWrapper repository)
    {
        _repository = repository;
        _gamePlayerMapper = new GamePlayerMapper();

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
        // _repository.SaveChanges();
        //_repository.Save();
    }

    private void AddPlayer(string playerId, string deckId, string gameId)
    {
        IsPlayerAvailable(playerId);
        Game_Player newGamePlayer = _gamePlayerMapper.FillNewGamePlayer(gameId, playerId, deckId);
        // _repository.Game_Player.Add(newGamePlayer);
        _repository.GamePlayer.Add(newGamePlayer);
        //_repository.Save();
    }

    private void IsPlayerAvailable(string playerId)
    {
        // var gamePlayer = _repository.Game_Player.FirstOrDefault(g => g.playerId == playerId);
        var gamePlayers = _repository.GamePlayer.GetAll();
        var gamePlayer = gamePlayers.FirstOrDefault(g => g.playerId == playerId);
        if (gamePlayer != null)
        {
            throw new ArgumentException("Player is currently in game");
        }
    }
}