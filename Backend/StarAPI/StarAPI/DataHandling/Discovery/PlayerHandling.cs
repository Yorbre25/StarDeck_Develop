using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.Logic.Mappers;
using Contracts;

namespace StarAPI.DataHandling.Discovery;

public class PlayerHandling
{
    private readonly IRepositoryWrapper _repository;
    private PlayerMapper _playerMapper;
    private IdGenerator _idGenerator = new IdGenerator();


    private static string s_idPrefix = "U";


    public PlayerHandling(IRepositoryWrapper repository)
    {
        this._repository = repository;
        this._playerMapper = new PlayerMapper(repository);
    }


    public List<OutputPlayer> GetAllPlayers()
    {
        // List<Player> players = _repository.Player.ToList();
        List<Player> players = _repository.Player.GetAll();
        return _playerMapper.FillOutputPlayer(players);
    }


    public string GetUsername(string id)
    {
        // var player = _repository.Player.FirstOrDefault(p => p.id == id);
        var player = _repository.Player.Get(id);
        return player.username;
    }


    public void AddPlayer(InputPlayer inputPlayer)
    {
        string id = GenerateId();
        var newPlayer = _playerMapper.FillNewPlayer(inputPlayer, id);
        // _repository.Player.Add(newPlayer);
        // _repository.SaveChanges();
        _repository.Player.Add(newPlayer);
        _repository.Save();
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


    public bool UsernameAlreadyExists(string username)
    {
        // var player = _repository.Player.FirstOrDefault(r => r.username == username);
        var players = _repository.Player.GetAll();
        var player = players.FirstOrDefault(r => r.username == username);
        if(player == null){
            return false;
        }
        return true;
    }

    public bool EmailAlreadyExists(string email)
    {
        // var player = _repository.Player.FirstOrDefault(r => r.email == email);
        var players = _repository.Player.GetAll();
        var player = players.FirstOrDefault(r => r.email == email);
        if(player == null){
            return false;
        }
        return true;
    }


    public bool IdAlreadyExists(string id){
        Player? player = new Player();
        // player = _repository.Player.FirstOrDefault(c => c.id == id);
        player = _repository.Player.Get(id);
        if(player == null){
            return false;
        }
        return true;
    }

    internal void IncreaseWins(string winnerId, int xpGain)
    {
        // Player player = _repository.Player.FirstOrDefault(p => p.id == winnerId);
        Player player = _repository.Player.Get(winnerId);
        player.xp += xpGain;
        // _repository.SaveChanges();
        _repository.Save();
    }
}