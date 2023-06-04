using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.Logic.Mappers;

namespace StarAPI.DataHandling.Discovery;

public class PlayerHandling
{
    private readonly StarDeckContext _context;
    private PlayerMapper _playerMapper;
    private IdGenerator _idGenerator = new IdGenerator();


    private static int s_minPlayerUsernameLenght = 1;
    private static int s_maxPlayerUsernameLenght = 30;
    private static bool  s_defaultInGameState = false;
    private static bool s_defaultActivationState = true;
    private static int s_defaultXp = 0;
    private static int s_defaultRanking = 0;
    private static int s_defaultCoins = 0;
    private static string s_idPrefix = "U";


    public PlayerHandling(StarDeckContext context)
    {
        this._context = context;
        this._playerMapper = new PlayerMapper(context);
    }


    public List<OutputPlayer> GetAllPlayers()
    {
        List<Player> players = _context.Player.ToList();
        return _playerMapper.FillOutputPlayer(players);
    }


    public string GetUsername(string id)
    {
        var player = _context.Player.FirstOrDefault(p => p.id == id);
        return player.username;
    }


    public void AddPlayer(InputPlayer inputPlayer)
    {
        string id = GenerateId();
        var newPlayer = _playerMapper.FillNewPlayer(inputPlayer, id);
        _context.Player.Add(newPlayer);
        _context.SaveChanges();
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
        var player = _context.Player.FirstOrDefault(r => r.username == username);
        if(player == null){
            return false;
        }
        return true;
    }

    public bool EmailAlreadyExists(string email)
    {
        var player = _context.Player.FirstOrDefault(r => r.email == email);
        if(player == null){
            return false;
        }
        return true;
    }


    public bool IdAlreadyExists(string id){
        Player? player = new Player();
        player = _context.Player.FirstOrDefault(c => c.id == id);
        if(player == null){
            return false;
        }
        return true;
    }

    internal void IncreaseWins(string winnerId, int xpGain)
    {
        Player player = _context.Player.FirstOrDefault(p => p.id == winnerId);
        player.xp += xpGain;
        _context.SaveChanges();
    }
}