using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.DTO.Discovery;
using StarAPI.Models;
using StarAPI.Constants;
using StarAPI.DataHandling.Game;

namespace StarAPI.Logic;

public class PlaceCard
{

    private readonly StarDeckContext _context;
    CardCRUD _cardCRUD;
    private GameTableMapper _gameTableMapper;


    public PlaceCard(StarDeckContext context)
    {
        _context = context;
        _gameTableMapper = new GameTableMapper(_context);
        _cardCRUD = new CardCRUD(_context);
    }

    internal void Place(InputTableLayout tableLayout)
    {
        try
        {
            SetTableLayout(tableLayout);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    private void SetTableLayout(InputTableLayout tableLayout)
    {
        Dictionary<string, string> cardsPerPlanet = tableLayout.layout;
        string gameId = tableLayout.gameId;
        string playerId = tableLayout.playerId;
        
        int cardsTotalCost = GetCardTotalCost(cardsPerPlanet);
        bool enoughPoints = EnoughPoints(playerId, cardsTotalCost);
        if (enoughPoints)
        {
            AddCardsToTable(tableLayout);
            UpdatePlayerCardPoints(playerId, cardsTotalCost);
            RemoveCardsFromHand(playerId, cardsPerPlanet);
        }
        _context.SaveChanges();
    }

    private int GetCardTotalCost(Dictionary<string, string> cardsPerPlanet)
    {
        int cardsTotalCost = 0;

        foreach(string cardId in cardsPerPlanet.Values)
        {
            OutputCard card = _cardCRUD.GetCard(cardId);
            cardsTotalCost += card.cost;    
        }
        return cardsTotalCost;
    }
    private bool EnoughPoints(string playerId, int cardsTotalCost)
    {
        Game_Player? player = _context.Game_Player.FirstOrDefault(gp => gp.playerId == playerId);
        bool enoughPoints = false;
        if(player.cardPoints >= cardsTotalCost)
        {
            enoughPoints = true;
        }
        else
        {
            throw new Exception("Not enough points");
        }
        return enoughPoints;
    }

    private void AddCardsToTable(InputTableLayout tableLayout)
    {
        List<GameTable> cards = _gameTableMapper.FillNewGameTable(tableLayout);
        _context.GameTable.AddRange(cards);
    }

    private void UpdatePlayerCardPoints(string playerId, int cardsTotalCost)
    {
        Game_Player player = _context.Game_Player.FirstOrDefault(gp => gp.playerId == playerId);
        player.cardPoints -= cardsTotalCost;
    }

    private void RemoveCardsFromHand(string playerId, Dictionary<string, string> cardsPerPlanet)
    {
        HandCard handCard = new HandCard(_context);
        foreach(KeyValuePair<string, string> entry in cardsPerPlanet)
        {
            string cardId = entry.Value;
            handCard.RemoveCardsFromHand(playerId, entry.Value);
        }

    }


    

}