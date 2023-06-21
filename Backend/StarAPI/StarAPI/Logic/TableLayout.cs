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

    private Dictionary<string, List<OutputCard>> GetCardsPerPlanet(string playerId)
    {
        List<GameTable> cards = GetPlayerCardsInTable(playerId);
        Dictionary<string, List<OutputCard>> layout = new Dictionary<string, List<OutputCard>>();
        foreach (GameTable card in cards)
        {
            OutputCard outputCard = _cardCRUD.GetCard(card.cardId);
            //If the planet is not in the dictionary, add it
            if (!layout.ContainsKey(card.planetId))
            {
                List<OutputCard> list = new List<OutputCard>();
                list.Add(outputCard);
                layout.Add(card.planetId, list);
            }
            else
            {
                layout[card.planetId].Add(outputCard);
                
            }
            // layout.Add(card.planetId, outputCard);
        }
        return layout;
    }

    private string GetRivalId(string gameId, string playerId)
    {
        return _gameHandling.GetRivalId(gameId, playerId);
    }

    public List<GameTable> GetPlayerCardsInTable(string playerId)
    {
        GameBoardHandling gameTableHandling = new GameBoardHandling(_repository);
        return gameTableHandling.GetPlayerCardsInTable(playerId);

    }      
}