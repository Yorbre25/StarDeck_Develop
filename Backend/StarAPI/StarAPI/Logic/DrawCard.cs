using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.DTO.Discovery;
using StarAPI.Constants;
using StarAPI.Models;
using StarAPI.DataHandling.Game;
using Contracts;
using System.Linq.Expressions;

namespace StarAPI.Logic;

public class DrawCard
{

    private readonly IRepositoryWrapper _repository;
    private RandomTools _randomTools = new RandomTools();

    public DrawCard(IRepositoryWrapper context)
    {
        _repository = context;
    }

    public OutputCard Draw(string gameId, string playerId)
    {
        try
        {
            return GetCard(gameId, playerId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    private OutputCard GetCard(string gameId, string playerId)
    {
        int numCardsInDeck = GetNumCardsInDeck(playerId);
        int handSize = GetHandSize(playerId);
        bool shouldDraw = ShouldDraw(numCardsInDeck, handSize);

        OutputCard card = new OutputCard();
        if (shouldDraw)
        {
            card = AddCardToHand(gameId, playerId);
            _repository.Save();
        }
        return card;
    }

    internal int GetNumCardsInDeck(string playerId)
    {
        GameDeckHandling gameDeckHandling = new GameDeckHandling(_repository);
        var cards = GetGameDeckByPlayerId(playerId);
        return cards.Count;
    }
    private int GetHandSize(string playerId)
    {
        HandCard handHandling = new HandCard(_repository);
        var cards = handHandling. GetHandByPlayerId(playerId);
        return cards.Count;
    }

    private bool ShouldDraw(int numCardsInDeck, int handSize)
    {
        bool shouldDraw = true;
        if(numCardsInDeck == 0 || handSize == Const.MaxHandSize){
            shouldDraw = false;
        }
        return shouldDraw;
    }

    private OutputCard AddCardToHand(string gameId, string playerId)
    {
        string cardId = PickRandomCard(playerId);
        Hand newHandCard = new Hand()
        {
            gameId = gameId,
            playerId = playerId,
            cardId = cardId
        };
        _repository.Hand.Add(newHandCard);
        RemoveCardFromDeck(playerId, cardId);

        CardCRUD cardCRUD = new CardCRUD(_repository);
        return cardCRUD.GetCard(cardId);
    }

    private string PickRandomCard(string playerId)
    {
        // string[] cardIds = _repository.Game_Deck.Where(d => d.playerId == playerId).Select(d => d.cardId).ToArray();
        string[] cardIds = GetGameDeckByPlayerId(playerId).Select(d => d.cardId).ToArray();
        string cardId = _randomTools.GetRandomElement<string>(cardIds);
        return cardId;
    }
    private void RemoveCardFromDeck(string playerId, string cardId)
    {
        GameDeckHandling gameDeckHandling = new GameDeckHandling(_repository);
        gameDeckHandling.RemoveCardFromDeck(playerId, cardId);
    }

    private List<Game_Deck> GetGameDeckByPlayerId(string playerId)
    {
        GameDeckHandling gameDeckHandling = new GameDeckHandling(_repository);
        List<Game_Deck> cards = gameDeckHandling.GetGameDeckByPlayerId(playerId);
        return cards;

    }





}