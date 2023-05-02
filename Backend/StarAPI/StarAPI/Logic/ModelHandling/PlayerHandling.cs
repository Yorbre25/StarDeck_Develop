using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.DTOs;
using StarAPI.Logic.Utils;
using StarAPI.Context;

namespace StarAPI.Logic.ModelHandling;

public class PlayerHandling
{
    private readonly StarDeckContext _context;
    private static int s_minPlayerUsernameLenght = 1;
    private static int s_maxPlayerUsernameLenght = 30;
    private static bool  s_defaultInGameState = false;
    private static bool s_defaultActivationState = true;
    private static int s_defaultLevel = 0;
    private static int s_defaultRanking = 0;
    private static int s_defaultCoins = 0;
    private static string s_idPrefix = "U";



    private Encrypt _encrypt;

    private IdGenerator _idGenerator;
    private CountryHandling _countryHandling;

    public PlayerHandling(StarDeckContext context)
    {
        this._context = context;
        this._countryHandling = new CountryHandling(context);
        this._idGenerator = new IdGenerator();
        this._encrypt = new Encrypt();
    }


    public List<OutputPlayer> GetAllPlayers()
    {
        try
        {
            return GettingAllPlayers();
        } 
        catch (System.Exception)
        {
            throw new Exception("Error getting players");
        }
    }

    public string[] GetAllPlayersIds()
    {
        try
        {
            return GettingAllPlayersIds();
        }
        catch (System.Exception)
        {
            throw new Exception("Error getting players ids");
        }
    }

    private string[] GettingAllPlayersIds()
    {
        //Get List of players ids from database
        List<Player> players = _context.Player.ToList();
        var result = from player in players select player.id;
        return result.ToArray();

    }

    public void SetPlayerRanking(string id, int ranking)
    {
        try
        {
           SettingPlayerRanking(id, ranking);
        }
        catch (System.Exception)
        {
            throw new Exception("Error setting player ranking");
        }
    }

    private void SettingPlayerRanking(string id, int ranking)
    {
        Player player = _context.Player.Find(id);
        player.ranking = ranking;
        _context.SaveChanges();
    }


    private List<OutputPlayer> GettingAllPlayers()
    {
        List<Player> players = _context.Player.ToList();
        return PlayersToOutputPlayers(players);
    }

    private List<OutputPlayer> PlayersToOutputPlayers(List<Player> players)
    {
        List<OutputPlayer> outputPlayers = new List<OutputPlayer>();
        foreach(var player in players)
        {
            outputPlayers.Add(PassPlayerValuesToOutputPlayer(player));
        }
        return outputPlayers;
    }

    private OutputPlayer PassPlayerValuesToOutputPlayer(Player player)
    {
        OutputPlayer outputCard = new OutputPlayer
        {
            id = player.id,
            email = player.email,
            firstName = player.firstName,
            lastName = player.lastName,
            username = player.username,
            pHash = player.pHash,
            level = player.level,
            ranking = player.ranking,
            country = _countryHandling.GetCountry(player.countryId),
            coins = player.coins,
            avatar = "Hola"
        };
        return outputCard;
    }

    public void AddPlayer(InputPlayer inputPlayer)
    {
        bool isValid = CheckInputValues(inputPlayer);
        bool usernameAlreadyExist = UsernameAlreadyExists(inputPlayer.username);
        bool emailAlreadyExist = EmailAlreadyExists(inputPlayer.email);

        if(!isValid){
            throw new ArgumentException("Invalid Values");
        }
        if(usernameAlreadyExist){
            throw new ArgumentException("Player username already exist");
        }
        if(emailAlreadyExist){
            throw new ArgumentException("Player email already exist");
        }
        InsertPlayer(inputPlayer);

    }

    public void AddVeteranPlayer(Player player)
    {
        _context.Player.Add(player);
        _context.SaveChanges();
    }



    public void InsertPlayer(InputPlayer inputPlayer){
        var newPlayer = setNewPlayerValues(inputPlayer);
        _context.Player.Add(newPlayer);
        _context.SaveChanges();
    }

    private Player setNewPlayerValues(InputPlayer newPlayer){
        Player player = new Player();
        string id = GenerateId();
        player.id = id;
        player.email = newPlayer.email;
        player.firstName = newPlayer.firstName;
        player.lastName = newPlayer.lastName;
        player.username = newPlayer.username;
        player.pHash = _encrypt.Sha256(newPlayer.password);
        player.level = s_defaultLevel;
        player.ranking = s_defaultRanking;
        player.inGame = s_defaultInGameState;
        player.activatedAccount = s_defaultActivationState;
        player.countryId = newPlayer.countryId;
        player.coins = 0;
        // player.avatarId = 1;
        return player;
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

    private bool CheckInputValues(InputPlayer player)
    {
        bool isValid = true;
        if(player.username.Length < s_minPlayerUsernameLenght || player.username.Length > s_maxPlayerUsernameLenght)
        {
            throw new ArgumentException("Invalid username lenght");
        }
        return isValid;
    }


    private bool UsernameAlreadyExists(string username)
    {
        var player = _context.Player.FirstOrDefault(r => r.username == username);
        if(player == null){
            return false;
        }
        return true;
    }

    private bool EmailAlreadyExists(string email)
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

    
}