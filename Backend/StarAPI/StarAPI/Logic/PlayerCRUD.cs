using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.Logic.Mappers;
using StarAPI.Constants;

using StarAPI.Context;
using StarAPI.DataHandling.Discovery;

namespace StarAPI.Logic;

public class PlayerCRUD
{

    private PlayerHandling _playerHandling;

    private static bool  s_defaultInGameState = false;
    private static bool s_defaultActivationState = true;
    private static int s_defaultXp = 0;
    private static int s_defaultRanking = 0;
    private static int s_defaultCoins = 0;
    private static string s_idPrefix = "U";


    public PlayerCRUD(StarDeckContext context)
    {
        this._playerHandling = new PlayerHandling(context);
    }


    public List<OutputPlayer> GetAllPlayers()
    {
        try
        {
            return _playerHandling.GetAllPlayers();
        } 
        catch (System.Exception)
        {
            throw new Exception("Error getting players");
        }
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
        _playerHandling.AddPlayer(inputPlayer);

    }

    private bool CheckInputValues(InputPlayer player)
    {
        bool isValid = true;
        if(player.username.Length < Const.MinNameLenght || player.username.Length > Const.MaxNameLenght)
        {
            throw new ArgumentException("Invalid username lenght");
        }
        return isValid;
    }


    private bool UsernameAlreadyExists(string username)
    {
        return _playerHandling.UsernameAlreadyExists(username);
    }

    private bool EmailAlreadyExists(string email)
    {
        return _playerHandling.EmailAlreadyExists(email);
    }


    internal void IncreaseWins(string winnerId, int xpGain)
    {
        try
        {
            _playerHandling.IncreaseWins(winnerId, xpGain);
        }
        catch (System.Exception)
        {
            throw new Exception("Error increasing player wins");
        }
    }
}