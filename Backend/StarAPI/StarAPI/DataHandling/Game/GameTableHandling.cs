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

    private static string s_idPrefix = "GT";



    public GameTableHandling(StarDeckContext context)
    {
        this._context = context;
        this._planetsForGame = new PlanetsForGame(_context);
        this._planetCRUD = new PlanetCRUD(_context);
        this._cardCRUD = new CardCRUD(_context);
        this._gameTableMapper = new GameTableMapper(_context);
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


    internal void PlaceCard(InputPlaceCard inputPlaceCard)
    {
        string gameId = inputPlaceCard.gameId;
        string planetId = inputPlaceCard.planetId;
        string playerId = inputPlaceCard.playerId;
        string cardId = inputPlaceCard.cardId;
        EnoughPoints(playerId, cardId);
        RooomForCard(gameId, planetId, playerId);
        AddCardToTable(inputPlaceCard);
    }

    private void AddCardToTable(InputPlaceCard inputPlaceCard)
    {
        string gameId = inputPlaceCard.gameId;
        GameTable newCard = _gameTableMapper.FillNewGameTable(inputPlaceCard);
        this._context.GameTable.Add(newCard);

        UpdatePlayerCardPoints(inputPlaceCard);
        this._context.SaveChanges();
    }         
    

    private void EnoughPoints(string playerId, string cardId)
    {
        OutputCard card = _cardCRUD.GetCard(cardId);
        Game_Player player = _context.Game_Player.FirstOrDefault(gp => gp.playerId == playerId);
        if (player.cardPoints < card.cost)
        {
            throw new Exception("Not enough points");
        }
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

    private void UpdatePlayerCardPoints(InputPlaceCard inputPlaceCard)
    {
        string cardId = inputPlaceCard.cardId;
        string playerId = inputPlaceCard.playerId;

        OutputCard card = _cardCRUD.GetCard(cardId);
        Game_Player player = _context.Game_Player.FirstOrDefault(gp => gp.playerId == playerId);
        
        player.cardPoints -= card.cost;
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
}