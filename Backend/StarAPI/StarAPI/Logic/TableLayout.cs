using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.DTO.Discovery;
using StarAPI.Constants;
using StarAPI.Models;
using StarAPI.DataHandling.Game;

namespace StarAPI.Logic;

public class TableLayout
{

    private readonly StarDeckContext _context;
    private CardCRUD _cardCRUD;
    private GameHandling _gameHandling;

    public TableLayout(StarDeckContext context)
    {
        _context = context;
        _cardCRUD = new CardCRUD(_context);
        _gameHandling = new GameHandling(_context);
    }

    internal OutputTableLayout GetLayout(string gameId, string playerId)
    {
        try
        {
            return Get(gameId, playerId);
        }
        catch (Exception e)
        {
            throw new Exception("Error getting layour" + e.Message);
        }

    }

    private OutputTableLayout Get(string gameId, string playerId)
    {
        string rivalId = GetRivalId(gameId, playerId);
        OutputTableLayout outputTableLayout = new OutputTableLayout();
        outputTableLayout.playerCards = GetCardsPerPlanet(playerId);
        outputTableLayout.rivalCards = GetCardsPerPlanet(rivalId);
        return outputTableLayout;
       
    }

    private Dictionary<string, OutputCard> GetCardsPerPlanet(string playerId)
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

    private string GetRivalId(object gameId, string playerId)
    {
        return _gameHandling.GetRivalId(gameId, playerId);
    }
}