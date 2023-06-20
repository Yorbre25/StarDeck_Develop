using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.DTO.Discovery;
using StarAPI.Constants;
using StarAPI.Models;
using StarAPI.DataHandling.Game;
using Contracts;

namespace StarAPI.Logic;

public class TableLayout
{

    private readonly IRepositoryWrapper _repository;
    private CardCRUD _cardCRUD;
    private GameHandling _gameHandling;

    public TableLayout(IRepositoryWrapper repository)
    {
        _repository = repository;
        _cardCRUD = new CardCRUD(_repository);
        _gameHandling = new GameHandling(_repository);
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
        // List<GameTable> cards = _repository.GameTable.Where(gt => gt.playerId == playerId).ToList();
        List<GameTable> cards = GetPlayerCardsInTable(playerId);
        Dictionary<string, OutputCard> layout = new Dictionary<string, OutputCard>();
        foreach (GameTable card in cards)
        {
            OutputCard outputCard = _cardCRUD.GetCard(card.cardId);
            layout.Add(card.planetId, outputCard);
        }
        return layout;
    }

    private string GetRivalId(string gameId, string playerId)
    {
        return _gameHandling.GetRivalId(gameId, playerId);
    }

    public List<GameTable> GetPlayerCardsInTable(string playerId)
    {
        GameTableHandling gameTableHandling = new GameTableHandling(_repository);
        return gameTableHandling.GetPlayerCardsInTable(playerId);

    }      
}