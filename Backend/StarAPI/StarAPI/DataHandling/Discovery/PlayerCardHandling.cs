using StarAPI.Models;
using StarAPI.DTO.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.Logic;
using Contracts;

namespace StarAPI.DataHandling.Discovery;

public class PlayerCardHandling
{
    private readonly IRepositoryWrapper _repository;
    private CardCRUD _cardCRUD;

    public PlayerCardHandling(IRepositoryWrapper repository)
    {
        this._repository = repository;
        this._cardCRUD = new CardCRUD(_repository);
    }

    public List<OutputCard> GetPlayerCards(string playerId)
    {
        // var playerCards = _repository.Player_Card.ToList();
        var playerCards = _repository.PlayerCard.GetAll();
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
        // _repository.Player_Card.ToList();
        _repository.PlayerCard.GetAll();
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
        // var playerCards = _repository.Player_Card.ToList();
        var playerCards = _repository.PlayerCard.GetAll();
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
        // _repository.Player_Card.Add(playerCard);
        // _repository.SaveChanges();
        _repository.PlayerCard.Add(playerCard);
        _repository.Save();
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