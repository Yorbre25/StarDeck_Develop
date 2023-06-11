using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.Models;
using StarAPI.DataHandling.Game;
using StarAPI.Constants;

namespace StarAPI.Logic;

public class SetupHands
{

    private readonly StarDeckContext _context;

    private DrawCard _drawCard;

    private GameDeckCardHandling _gameDeckCardHandling;


    public SetupHands(StarDeckContext context)
    {
        _context = context;
        _gameDeckCardHandling = new GameDeckCardHandling(_context);
        _drawCard = new DrawCard(_context);
    }

    internal void SetupHand(string gameId)
    {
        try
        {
            Setup(gameId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    private void Setup(string gameId)
    {
        StarAPI.Models.Game game = _context.Game.FirstOrDefault(g => g.id == gameId);
        CreateHand(gameId, game.player1Id);
        CreateHand(gameId, game.player2Id);
        _context.SaveChanges();
    }

    private void CreateHand(string gameId, string playerId)
    {
        bool alreadyExist = PlayerAlreadyHasHand(playerId);
        if(alreadyExist){
            throw new ArgumentException("Player already has a hand");
        }
        GiveInitialCards(gameId, playerId);
    }

    private bool PlayerAlreadyHasHand(string playerId)
    {
        var hand = _context.Hand.FirstOrDefault(d => d.playerId == playerId);
        bool alreadyHas = false;
        if(hand != null){
            alreadyHas = true;
        }
        return alreadyHas;
    }

    private void GiveInitialCards(string gameId, string playerId)
    {
        string cardId;
        int handSize = SetHandSize(playerId);
        for (int i = 0; i < handSize; i++)
        {
            _drawCard.Draw(gameId, playerId);
        }
    }

    private int SetHandSize(string playerId)
    {
        int numCardsInDeck = _context.Game_Deck.Where(c => c.playerId == playerId).ToList().Count;
        if(numCardsInDeck < Const.IntialCardsPerHand){
            return numCardsInDeck;
        }
        return Const.IntialCardsPerHand;
    }

}