using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.DTO.Discovery;
using StarAPI.Constants;
using StarAPI.Models;
using StarAPI.DataHandling.Game;

namespace StarAPI.Logic;

public class DrawCard
{

    private readonly StarDeckContext _context;
    private RandomTools _randomTools = new RandomTools();

    public DrawCard(StarDeckContext context)
    {
        _context = context;
    }

    internal OutputCard Draw(string gameId, string playerId)
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
            _context.SaveChanges();
        }
        return card;
    }

    internal int GetNumCardsInDeck(string playerId)
    {
        var cards = _context.Game_Deck.Where(c => c.playerId == playerId).ToList();
        return cards.Count;
    }

    private int GetHandSize(string playerId)
    {
        return _context.Hand.Count(h => h.playerId == playerId);
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
        _context.Hand.Add(newHandCard);
        RemoveCardFromDeck(playerId, cardId);

        CardCRUD cardCRUD = new CardCRUD(_context);
        return cardCRUD.GetCard(cardId);
    }

    private string PickRandomCard(string playerId)
    {
        string[] cardIds = _context.Game_Deck.Where(d => d.playerId == playerId).Select(d => d.cardId).ToArray();
        string cardId = _randomTools.GetRandomElement<string>(cardIds);
        return cardId;
    }
    private void RemoveCardFromDeck(string playerId, string cardId)
    {
        GameDeckHandling gameDeckHandling = new GameDeckHandling(_context);
        gameDeckHandling.RemoveCardFromDeck(playerId, cardId);
    }





}