using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.DTOs;
using StarAPI.Logic.Utils;
using StarAPI.Context;

namespace StarAPI.Logic.ModelHandling;

public class PlayerHandling
{
    private readonly StarDeckContext _context;
    private static int s_minPlayerNameLenght = 5;
    private static int s_maxCardNameLenght = 30;
    private static bool s_defaultActivationState = true;
    private static int s_minEnergyValue = -100;
    private static int s_maxEnergyValue = 100;
    private static int s_minBattleCost = 0;
    private static int s_maxBattleCost = 100;
    private static int s_maxDescriptionLenght = 1000;
    private static string s_idPrefix = "U";

    private IdGenerator _idGenerator = new IdGenerator();
    private CountryHandling _countryHandling;

    public PlayerHandling(StarDeckContext context)
    {
        this._context = context;
        this._countryHandling = new CountryHandling(context);
    }


    public List<OutputPlayer> GetAllPlayer()
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
            try
            {
                outputPlayers.Add(PassPlayerValuesToOutputPlayer(player));
                
            }
            catch (System.Exception)
            {
                continue;
            }
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
            activatedAccount = player.activatedAccount,
            country = _countryHandling.GetCountry(player.countryId),
            coins = player.coins,
            avatar = "Hola"
        };
        return outputCard;
    }
}