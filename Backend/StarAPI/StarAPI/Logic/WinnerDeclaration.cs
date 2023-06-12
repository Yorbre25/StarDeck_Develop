using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.DataHandling.Game;
using StarAPI.DTO.Discovery;
using StarAPI.Models;
using StarAPI.Constants;

namespace StarAPI.Logic;

public class WinnerDeclaration
{

    private readonly StarDeckContext _context;
    private int player1 = 0;
    private int player2 = 1;
    private int numPlayers = 2;


    public WinnerDeclaration(StarDeckContext context)
    {
        _context = context;
    }

    public string GetWinner(string gameId)
    {
        try
        {
            return DeclareWinner(gameId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    internal string DeclareWinner(string gameId)
    {
        string [] playersIds = GetGamePlayers(gameId);
        string[] planetsIds = GetGamePlanets(gameId);


        Dictionary<string, int> pointsPerPlanetPlayer1 = GetBattlePointsPerPlanet(planetsIds, playersIds[player1]);
        Dictionary<string, int> pointsPerPlanetPlayer2 = GetBattlePointsPerPlanet(planetsIds, playersIds[player2]);

        string winner = ComparePoints(playersIds, pointsPerPlanetPlayer1, pointsPerPlanetPlayer2);
        return winner;   
    }

    private Dictionary<string, int> GetBattlePointsPerPlanet(string[] planetsIds, string playerId)
    {
        Dictionary<string, int> pointsPerPlanet = new Dictionary<string, int>();
        foreach (string planetId in planetsIds)
        {
            pointsPerPlanet.Add(planetId, 0);
        }

        List<GameTable> cards = _context.GameTable.Where(gt => gt.playerId == playerId).ToList();
        foreach (GameTable card in cards)
        {
            pointsPerPlanet[card.planetId] += card.battlePoints;
        }
        return pointsPerPlanet;
    }



    private string ComparePoints(string[] playersIds, Dictionary<string, int> pointsPerPlanetPlayer1, Dictionary<string, int> pointsPerPlanetPlayer2)
    {
        Dictionary<string, int> planetsConqueredPerPlayer = PlanetsConquered(playersIds, pointsPerPlanetPlayer1, pointsPerPlanetPlayer2);
        string winner = PlayerWithMostPlanetsConquered(playersIds, planetsConqueredPerPlayer);
        bool tie = winner == Const.Tie;
        if (tie)
        {
            winner = TieBreaker(playersIds, pointsPerPlanetPlayer1, pointsPerPlanetPlayer2);
        }
        return winner;
    }

    private Dictionary<string, int> PlanetsConquered(string[] playersIds, Dictionary<string, int> pointsPerPlanetPlayer1, Dictionary<string, int> pointsPerPlanetPlayer2)
    {
        string IdPlayer1 = playersIds[0];
        string IdPlayer2 = playersIds[1];
        Dictionary<string, int> planetsConqueredPerPlayer = new Dictionary<string, int>
        {
            {IdPlayer1, 0},
            {IdPlayer2, 0}
        };

        foreach (KeyValuePair<string, int> planetPoints in pointsPerPlanetPlayer1)
        {
            string planetId = planetPoints.Key;
            int pointsPlayer1 = planetPoints.Value;
            int pointsPlayer2 = pointsPerPlanetPlayer2[planetId];
            if(pointsPlayer1 > pointsPlayer2)
            {
                planetsConqueredPerPlayer[IdPlayer1] += 1;
            }
            else if(pointsPlayer1 < pointsPlayer2)
            {
                planetsConqueredPerPlayer[IdPlayer2] += 1;
            }
        }
        return planetsConqueredPerPlayer;
    }

    private string TieBreaker(string[] playersIds, Dictionary<string, int> pointsPerPlanetPlayer1, Dictionary<string, int> pointsPerPlanetPlayer2)
    {
        string IdPlayer1 = playersIds[player1];
        string IdPlayer2 = playersIds[player2];

        int totalPointsPlayer1 = GetTotalPoints(pointsPerPlanetPlayer1);
        int totalPointsPlayer2 = GetTotalPoints(pointsPerPlanetPlayer2);
        if(totalPointsPlayer1 > totalPointsPlayer2)
        {
            return IdPlayer1;
        }
        else if(totalPointsPlayer1 < totalPointsPlayer2)
        {
            return IdPlayer2;
        }
        else
        {
            return Const.Tie;
        }
    }

    private int GetTotalPoints(Dictionary<string, int> pointsPerPlanetPlayer1)
    {
        int totalPoints = 0;
        foreach (KeyValuePair<string, int> planet in pointsPerPlanetPlayer1)
        {
            totalPoints += planet.Value;
        }
        return totalPoints;
    }

    private string PlayerWithMostPlanetsConquered(string[] playersIds, Dictionary<string, int> planetsConqueredPerPlayer)
    {
        string IdPlayer1 = playersIds[player1];
        string IdPlayer2 = playersIds[player2];
        
        if(planetsConqueredPerPlayer[IdPlayer1] > planetsConqueredPerPlayer[IdPlayer2])
        {
            return IdPlayer1;
        }
        else if(planetsConqueredPerPlayer[IdPlayer1] < planetsConqueredPerPlayer[IdPlayer2])
        {
            return IdPlayer2;
        }
        else
        {
            return Const.Tie;
        }
    }





    private string[] GetGamePlayers(string gameId)
    {
        string[] playersIds = new string[numPlayers];

        Models.Game? game = _context.Game.FirstOrDefault(g => g.id == gameId);
        playersIds[player1] = game.player1Id;
        playersIds[player2] = game.player2Id;
        return playersIds;
    }

    private string[] GetGamePlanets(string gameId)
    {
        GameTableHandling gameTableHandling = new GameTableHandling(_context);
        List<OutputPlanet> planets = gameTableHandling.GetGamePlanets(gameId);
        string[] planetsIds = new string[planets.Count];
        for (int i = 0; i < planets.Count; i++)
        {
            planetsIds[i] = planets[i].id;
        }
        return planetsIds;
    }

}