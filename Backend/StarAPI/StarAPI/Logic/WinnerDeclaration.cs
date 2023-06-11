using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.DataHandling.Game;
using StarAPI.DTO.Discovery;
using StarAPI.Models;

namespace StarAPI.Logic;

public class WinnerDeclaration
{

    private readonly StarDeckContext _context;
    private string _tie = "Tie";


    public WinnerDeclaration(StarDeckContext context)
    {
        _context = context;
    }

    public string Winner(string gameId)
    {
        try
        {
            return GetWinner(gameId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    internal string GetWinner(string gameId)
    {
        string [] playersIds = GetGamePlayers(gameId);
        string[] planetsIds = GetGamePlanets(gameId);
        Dictionary<string, int> pointsPerPlanetPlayer1 = GetBattlePointsPerPlanet(planetsIds, playersIds[0]);
        Dictionary<string, int> pointsPerPlanetPlayer2 = GetBattlePointsPerPlanet(planetsIds, playersIds[1]);

        // string winner = DeclareWinner(playersIds, pointsPerPlanetPlayer1, pointsPerPlanetPlayer2);
        string winner = DeclareWinner(playersIds, pointsPerPlanetPlayer1, pointsPerPlanetPlayer2);
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



    private string DeclareWinner(string[] playersIds, Dictionary<string, int> pointsPerPlanetPlayer1, Dictionary<string, int> pointsPerPlanetPlayer2)
    {
        Dictionary<string, int> planetsConquered = PlanetsConquered(playersIds, pointsPerPlanetPlayer1, pointsPerPlanetPlayer2);
        string winner = PlayerWithMostPlanetsConquered(playersIds, planetsConquered);
        bool tie = winner == this._tie;
        if (tie)
        {
            winner = TieBreaker(playersIds, pointsPerPlanetPlayer1, pointsPerPlanetPlayer2);
        }
        return winner;
    }

    private Dictionary<string, int> PlanetsConquered(string[] playersIds, Dictionary<string, int> pointsPerPlanetPlayer1, Dictionary<string, int> pointsPerPlanetPlayer2)
    {
        string player1Id = playersIds[0];
        string player2Id = playersIds[1];
        Dictionary<string, int> planetsConquered = new Dictionary<string, int>
        {
            {player1Id, 0},
            {player2Id, 0}
        };

        foreach (KeyValuePair<string, int> planet in pointsPerPlanetPlayer1)
        {
            string planetId = planet.Key;
            int pointsPlayer1 = planet.Value;
            int pointsPlayer2 = pointsPerPlanetPlayer2[planetId];
            if(pointsPlayer1 > pointsPlayer2)
            {
                planetsConquered[player1Id] += 1;
            }
            else if(pointsPlayer1 < pointsPlayer2)
            {
                planetsConquered[player2Id] += 1;
            }
        }
        return planetsConquered;
    }

    private string TieBreaker(string[] playersIds, Dictionary<string, int> pointsPerPlanetPlayer1, Dictionary<string, int> pointsPerPlanetPlayer2)
    {
        string player1Id = playersIds[0];
        string player2Id = playersIds[1];

        int totalPointsPlayer1 = GetTotalPoints(pointsPerPlanetPlayer1);
        int totalPointsPlayer2 = GetTotalPoints(pointsPerPlanetPlayer2);
        if(totalPointsPlayer1 > totalPointsPlayer2)
        {
            return player1Id;
        }
        else if(totalPointsPlayer1 < totalPointsPlayer2)
        {
            return player2Id;
        }
        else
        {
            return _tie;
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

    private string PlayerWithMostPlanetsConquered(string[] playersIds, Dictionary<string, int> planetsConquered)
    {
        string player1Id = playersIds[0];
        string player2Id = playersIds[1];
        
        if(planetsConquered[player1Id] > planetsConquered[player2Id])
        {
            return player1Id;
        }
        else if(planetsConquered[player1Id] < planetsConquered[player2Id])
        {
            return player2Id;
        }
        else
        {
            return _tie;
        }
    }





    private string[] GetGamePlayers(string gameId)
    {
        int numPlayers = 2;
        string[] playersIds = new string[numPlayers];

        Models.Game game = _context.Game.FirstOrDefault(g => g.id == gameId);
        playersIds[0] = game.player1Id;
        playersIds[1] = game.player2Id;
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