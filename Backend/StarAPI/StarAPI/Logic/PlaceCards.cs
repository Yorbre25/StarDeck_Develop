using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.DTO.Discovery;
using StarAPI.Models;
using StarAPI.Constants;

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
        Dictionary<string, string> layout = tableLayout.layout;
        string gameId = tableLayout.gameId;
        string playerId = tableLayout.playerId;
        
        int cardsTotalCost = GetCardTotalCost(layout);
        bool enoughPoints = EnoughPoints(playerId, cardsTotalCost);
        if (enoughPoints)
        {
            AddCardsToTable(tableLayout);
            UpdatePlayerCardPoints(playerId, cardsTotalCost);
            RemoveCardsFromHand(playerId, layout);
        }
        _context.SaveChanges();
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

    private void RemoveCardsFromHand(string playerId, Dictionary<string, string> layout)
    {
        foreach(KeyValuePair<string, string> entry in layout)
        {
            Hand? hand = _context.Hand.FirstOrDefault(h => h.playerId == playerId && h.cardId == entry.Value);
            _context.Hand.Remove(hand);
        }

    }

    //!Falta verificar
    private void RooomForCard(string gameId, string planetId, string playerId)
    {
        List<GameTable> gameCards = _context.GameTable.Where(gt => gt.gameId == gameId).ToList();
        List<GameTable> planetCards = gameCards.Where(gt => gt.planetId == planetId).ToList();
        List<GameTable> planetCardsOfPlayer = planetCards.Where(gt => gt.playerId == playerId).ToList();   
        if (planetCardsOfPlayer.Count() >= Const.CardsPerPlanet)
        {
            throw new Exception("No room for more cards");
        }
    }

    

}