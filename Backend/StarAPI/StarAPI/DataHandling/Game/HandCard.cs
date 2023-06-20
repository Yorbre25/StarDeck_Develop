using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.Utils;
using StarAPI.DataHandling.Discovery;
using StarAPI.DTO.Discovery;
using StarAPI.Constants;
using StarAPI.Logic;
using Contracts;

namespace StarAPI.DataHandling.Game;

public class HandCard
{
    private readonly IRepositoryWrapper _repository;
    private DeckCardHandling _deckCardHandling;
    private IdGenerator _idGenerator = new IdGenerator();
    private DeckHandling _deckHandling;
    private CardCRUD _cardCRUD;
    private const string IdPrefix = "H";


    public HandCard(IRepositoryWrapper context)
    {
        this._repository = context;
        this._deckCardHandling = new DeckCardHandling(_repository);
        this._deckHandling = new DeckHandling(_repository);
        this._cardCRUD = new CardCRUD(_repository);
    }

    public List<OutputCard> GetHandCards(string gameId, string playerId)
    {
        try
        {
            return GetHandCardsByPlayerId(playerId);
        }
        catch (Exception e)
        {
            throw new Exception("Error getting hand cards: " + e.Message);
        }
    }

    public List<OutputCard> GetHandCardsByPlayerId(string playerId)
    {
        var cardsIds = GetHandByPlayerId(playerId).Select(d => d.cardId).ToArray();
        return _deckCardHandling.GetCards(cardsIds);
    }

    public void RemoveCardsFromHand(string playerId, string cardId)
    {
        // Hand? hand = _repository.Hand.FirstOrDefault(h => h.playerId == playerId && h.cardId == cardId);
        // _repository.Hand.Remove(hand);
        List<Hand> hands = GetHandByPlayerId(playerId);
        Hand hand = hands.Find(h => h.cardId == cardId);
        _repository.Hand.Delete(hand);
        _repository.Save();
    }

    internal List<Hand> GetHandByGameId(string gameId)
    {
        List<Hand> decks = _repository.Hand.GetAll();
        return decks.FindAll(d => d.gameId == gameId);
    }
    internal List<Hand> GetHandByPlayerId(string playerId)
    {
        List<Hand> cards = _repository.Hand.GetAll();
        return cards.FindAll(d => d.playerId == playerId);
    }
}