using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.Game;
using StarAPI.Logic.Utils;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Logic.Mappers;
using StarAPI.Logic;
using StarAPI.DTO.Game;
using StarAPI.Constants;
namespace StarAPI.DataHandling.Game;

public class GameTableHandling
{
    private readonly StarDeckContext _context;
    private PlanetCRUD _planetCRUD;
    private CardCRUD _cardCRUD;
    private PlanetsForGame _planetsForGame;
    private GameTableMapper _gameTableMapper;
    private HandHandling _handHandling;

    private static string s_idPrefix = "GT";



    public GameTableHandling(StarDeckContext context)
    {
        this._context = context;
        this._planetsForGame = new PlanetsForGame(_context);
        this._planetCRUD = new PlanetCRUD(_context);
        this._cardCRUD = new CardCRUD(_context);
        this._gameTableMapper = new GameTableMapper(_context);
        this._handHandling = new HandHandling(_context);
    }

    public List<OutputPlanet> GetGamePlanets(string gameId)
    {
        try
        {
            return GetPlanets(gameId);
        }
        catch
        {
            throw new Exception("Error getting planets");
        }
    }


    public List<OutputPlanet> GetPlanets(string gameId)
    {
        List<Game_Planet> gamePlanets = _context.Game_Planet.Where(gp => gp.gameId == gameId).ToList();
        List<OutputPlanet> listPlanets = new List<OutputPlanet>();

        foreach (Game_Planet gamePlanet in gamePlanets)
        {
            OutputPlanet planet = _planetCRUD.GetPlanet(gamePlanet.planetId);
            planet.show = gamePlanet.show;
            listPlanets.Add(planet);
        }
        return listPlanets;
    }




    private void AddCardsToTable(InputTableLayout tableLayout)
    {
        List<GameTable> cards = _gameTableMapper.FillNewGameTable(tableLayout);
        _context.GameTable.AddRange(cards);
        _context.SaveChanges();

    }         
    


    internal OutputTableLayout GetLayout(string playerId, string rivalId)
    {
        OutputTableLayout outputTableLayout = new OutputTableLayout();
        outputTableLayout.playerCards = GetLayout(playerId);
        outputTableLayout.rivalCards = GetLayout(rivalId);
        return outputTableLayout;
    }

    private Dictionary<string, OutputCard> GetLayout(string playerId)
    {
        List<GameTable> cards = _context.GameTable.Where(gt => gt.playerId == playerId).ToList();
        Dictionary<string, OutputCard> layout = new Dictionary<string, OutputCard>();
        foreach (GameTable card in cards)
        {
            OutputCard outputCard = _cardCRUD.GetCard(card.cardId);
            layout.Add(card.planetId, outputCard);
        }
        return layout;
    }

    internal Dictionary<string, int> GetBattlePointsPerPlanet(string playerId)
    {
        List<GameTable> cards = _context.GameTable.Where(gt => gt.playerId == playerId).ToList();
        Dictionary<string, int> battlePointsPerPlanet = new Dictionary<string, int>();
        foreach (GameTable card in cards)
        {
            string planetId = card.planetId;
            if (battlePointsPerPlanet.ContainsKey(planetId))
            {
                battlePointsPerPlanet[planetId] += card.battlePoints;
            }
            else
            {
                battlePointsPerPlanet.Add(planetId, card.battlePoints);
            }
        }
        return battlePointsPerPlanet;
    }

    internal string DeclareWinner(string player1Id, string player2Id)
    {

        Dictionary<string, int> battlePointsPlayer1 = GetBattlePointsPerPlanet(player1Id);
        Dictionary<string, int> battlePointsPlayer2 = GetBattlePointsPerPlanet(player2Id);

        int numPlanetsConqueredPlayer1 = 0;
        int numPlanetsConqueredPlayer2 = 0;
        //Compare each planet battle points
        foreach (KeyValuePair<string, int> planet in battlePointsPlayer1)
        {
            string planetId = planet.Key;
            int battlePointsPlayer1Planet = planet.Value;
            if(!battlePointsPlayer2.ContainsKey(planetId))
            {
                numPlanetsConqueredPlayer1++;
                continue;
            }
            int battlePointsPlayer2Planet = battlePointsPlayer2[planetId];
            if (battlePointsPlayer1Planet > battlePointsPlayer2Planet)
            {
                numPlanetsConqueredPlayer1++;
            }
            else if (battlePointsPlayer1Planet < battlePointsPlayer2Planet)
            {
                numPlanetsConqueredPlayer2++;
            }
        }

        foreach (KeyValuePair<string, int> planet in battlePointsPlayer2)
        {
            string planetId = planet.Key;
            if (!battlePointsPlayer1.ContainsKey(planetId))
            {
                numPlanetsConqueredPlayer2++;
            }
        }

        if(numPlanetsConqueredPlayer1 > numPlanetsConqueredPlayer2)
        {
            return player1Id;
        }
        else
        {
            return player2Id;
        }

    }
}
