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




    public GameTableHandling(StarDeckContext context)
    {
        this._context = context;
        this._planetCRUD = new PlanetCRUD(_context);
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




    // private void AddCardsToTable(InputTableLayout tableLayout)
    // {
    //     List<GameTable> cards = _gameTableMapper.FillNewGameTable(tableLayout);
    //     _context.GameTable.AddRange(cards);
    //     _context.SaveChanges();

    // }         
    


    // internal OutputTableLayout GetLayout(string playerId, string rivalId)
    // {
    //     OutputTableLayout outputTableLayout = new OutputTableLayout();
    //     outputTableLayout.playerCards = GetLayout(playerId);
    //     outputTableLayout.rivalCards = GetLayout(rivalId);
    //     return outputTableLayout;
    // }

    // private Dictionary<string, OutputCard> GetLayout(string playerId)
    // {
    //     List<GameTable> cards = _context.GameTable.Where(gt => gt.playerId == playerId).ToList();
    //     Dictionary<string, OutputCard> layout = new Dictionary<string, OutputCard>();
    //     foreach (GameTable card in cards)
    //     {
    //         OutputCard outputCard = _cardCRUD.GetCard(card.cardId);
    //         layout.Add(card.planetId, outputCard);
    //     }
    //     return layout;
    // }





}
