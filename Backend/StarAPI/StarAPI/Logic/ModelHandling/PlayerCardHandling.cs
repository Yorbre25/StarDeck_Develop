using StarAPI.Models;
using StarAPI.DTOs;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTOs;

namespace StarAPI.Logic.ModelHandling;

public class PlayerCardHandling
{
    private readonly StarDeckContext _context;
    private CardHandling _cardHandling;
    // private NewPlayerCardGenerator _newPlayerCardGenerator;
    private CardPackageGenerator _cardPackageGenerator;

    public PlayerCardHandling(StarDeckContext context)
    {
        this._context = context;
        this._cardHandling = new CardHandling(_context);
        // this._newPlayerCardGenerator = new NewPlayerCardGenerator(_context);
        // this._cardPackageGenerator = new CardPackageGenerator(_context);
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
            cards.Add(_cardHandling.GetCard(cardId));
        }
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

    // public void GenerateCardsForNewPlayer(string playerId)
    // {
    //     try
    //     {
    //         _newPlayerCardGenerator.GenerateCardsForNewPlayer(playerId);
    //     } 
    //     catch (System.Exception)
    //     {
    //         throw new Exception("Error generating cards for new player");
    //     }
    // }
    
    public List<List<OutputCard>> GetPackagesForNewPlayer()
    {
        try
        {
            // return _cardPackageGenerator.GetPackagesForNewPlayer();
            return _cardPackageGenerator.GetPackagesForNewPlayer();
        } 
        catch (System.Exception)
        {
            throw new Exception("Error getting packages for new player");
        }
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