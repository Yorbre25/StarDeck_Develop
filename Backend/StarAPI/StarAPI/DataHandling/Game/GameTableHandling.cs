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


    public void SetupTable(string gameId)
    {
        List<Game_Planet> planets = new List<Game_Planet>();
        string[] planetsId = SetupPlanets();
        for (int i = 0; i < planetsId.Length; i++)
        {
            Game_Planet planet = new Game_Planet();
            planet.gameId = gameId;
            planet.planetId = planetsId[i];
            planet.show = true;
            planets.Add(planet);
        }
        SetHiddenPlanet(planets);
        _context.Game_Planet.AddRange(planets);
    }

    private void SetHiddenPlanet(List<Game_Planet> planets)
    {
        Game_Planet lastPlanet = planets[planets.Count() - 1];
        lastPlanet.show = false;
    }

    public string[] SetupPlanets()
    {
        List<OutputPlanet> listPlanets = _planetsForGame.GetPlanetsForNewGame();
        string[] planetIds = new string[listPlanets.Count];

        for (int i = 0; i < listPlanets.Count; i++)
        {
            planetIds[i] = listPlanets[i].id;
        }
        return planetIds;
    }

    public List<OutputPlanet> GetGamePlanets(string gameId)
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


    internal void PlaceCards(InputTableLayout tableLayout)
    {
        string gameId = tableLayout.gameId;
        string playerId = tableLayout.playerId;
        int cardsTotalCost;
        Dictionary<string, string> layout = tableLayout.layout;
        cardsTotalCost = GetCardTotalCost(layout);
        EnoughPoints(playerId, cardsTotalCost);
        AddCardsToTable(tableLayout);
        UpdatePlayerCardPoints(playerId, cardsTotalCost);
        RemoveCardsFromHand(playerId, layout);
        _context.SaveChanges();
    }

    private void RemoveCardsFromHand(string playerId, Dictionary<string, string> layout)
    {
        foreach(KeyValuePair<string, string> entry in layout)
        {
            _handHandling.RemoveCardFromHand(playerId, entry.Value);
        }

    }

    private void AddCardsToTable(InputTableLayout tableLayout)
    {
        List<GameTable> cards = _gameTableMapper.FillNewGameTable(tableLayout);
        _context.GameTable.AddRange(cards);
        _context.SaveChanges();

    }         
    

    private void EnoughPoints(string playerId, int cardsTotalCost)
    {
        Game_Player player = _context.Game_Player.FirstOrDefault(gp => gp.playerId == playerId);
        if (player.cardPoints < cardsTotalCost)
        {
            throw new Exception("Not enough points");
        }
    }

    private int GetCardTotalCost(Dictionary<string, string> layout)
    {
        Dictionary<string, string>.ValueCollection values = layout.Values;
        int cardsTotalCost = 0;
        foreach(string cardId in values)
        {
            OutputCard card = _cardCRUD.GetCard(cardId);
            cardsTotalCost += card.cost;    
        }
        return cardsTotalCost;
    }

    private void RooomForCard(string gameId, string planetId, string playerId)
    {
        List<GameTable> gameCards = _context.GameTable.Where(gt => gt.gameId == gameId).ToList();
        List<GameTable> planetCards = gameCards.Where(gt => gt.planetId == planetId).ToList();
        List<GameTable> planetCardsOfPlayer = planetCards.Where(gt => gt.playerId == playerId).ToList();    if (planetCardsOfPlayer.Count() >= Const.CardsPerPlanet)
        {
            throw new Exception("No room for more cards");
        }
    }

    private void UpdatePlayerCardPoints(string playerId, int cardsTotalCost)
    {
        Game_Player player = _context.Game_Player.FirstOrDefault(gp => gp.playerId == playerId);
        player.cardPoints -= cardsTotalCost;
    }

    internal void EndGame(string gameId)
    {
        DeleteCards(gameId);
        List<Game_Planet> planets = _context.Game_Planet.Where(gt => gt.gameId == gameId).ToList();
        _context.Game_Planet.RemoveRange(planets);
    }

    private void DeleteCards(string gameId)
    {
        List<GameTable> cards = _context.GameTable.Where(gt => gt.gameId == gameId).ToList();
        if (cards.Count() > 0)
        {
            _context.GameTable.RemoveRange(cards);
        }
    }

    internal void SetTableLayout(InputTableLayout tableLayout)
    {
        PlaceCards(tableLayout);
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