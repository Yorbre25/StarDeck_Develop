using StarAPI.Models;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.Logic;

namespace StarAPI.DataHandling.Discovery;

public class PlayerCardHandling
{
    private readonly StarDeckContext _context;
    private CardCRUD _cardCRUD;

    public PlayerCardHandling(StarDeckContext context)
    {
        this._context = context;
        this._cardCRUD = new CardCRUD(_context);
    }

    public List<OutputCard> GetPlayerCards(string playerId)
    {
        var playerCards = _context.Player_Card.ToList();
        string [] cardsId = playerCards.FindAll(pc => pc.playerId == playerId).Select(pc => pc.cardId).ToArray();
        return GetCards(cardsId);
    }

    private List<OutputCard> GetCards(string [] cardsId)
    {
        List<OutputCard> cards = new List<OutputCard>();
        foreach(var cardId in cardsId)
        {
            cards.Add(_cardCRUD.GetCard(cardId));
        }
        _context.Player_Card.ToList();
        return cards;
    }

    public int GetCardCount(string playerId)
    {
        try
        {
            return GettingCardCount(playerId);
        } 
        catch (System.Exception)
        {
            throw new Exception("Error getting card count");
        }
    }

    private int GettingCardCount(string playerId)
    {
        var playerCards = _context.Player_Card.ToList();
        playerCards = playerCards.FindAll(pc => pc.playerId == playerId);
        return playerCards.Count;
    }

    public void AssignCard(string playerId, string cardId)
    {
        try
        {
            AssigningCard(playerId, cardId);
        } 
        catch (System.Exception)
        {
        }
    }

    private void AssigningCard(string playerId, string cardId)
    {
        Player_Card playerCard = new Player_Card();
        playerCard.playerId = playerId;
        playerCard.cardId = cardId;
        _context.Player_Card.Add(playerCard);
        _context.SaveChanges();
    }
 
    internal bool PlayerAlreadyHasCards(string playerId)
    {
        int cardCount = GetCardCount(playerId);
        if (cardCount > 0)
        {
            return true;
        }
        return false;
    }

}