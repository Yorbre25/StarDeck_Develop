using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using StarAPI.Models;
using StarAPI.DataHandling.Game;
using StarAPI.Constants;
using Contracts;

namespace StarAPI.Logic;

public class SetupHands
{

    private readonly IRepositoryWrapper _repository;

    private DrawCard _drawCard;



    public SetupHands(IRepositoryWrapper repository)
    {
        _repository = repository;
        _drawCard = new DrawCard(repository);
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
        // StarAPI.Models.Game game = _repository.Game.FirstOrDefault(g => g.id == gameId);
        Models.Game game = _repository.Game.Get(gameId);
        CreateHand(gameId, game.player1Id);
        CreateHand(gameId, game.player2Id);
        // _repository.SaveChanges();
        _repository.Save();
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
        // var hand = _repository.Hand.FirstOrDefault(d => d.playerId == playerId);
        var hand = GetHandByPlayerId(playerId);
        bool alreadyHas = false;
        if(hand.Count != 0){
            alreadyHas = true;
        }
        return alreadyHas;
    }

    private void GiveInitialCards(string gameId, string playerId)
    {
        int handSize = SetHandSize(playerId);
        for (int i = 0; i < handSize; i++)
        {
            _drawCard.Draw(gameId, playerId);
        }
    }

    private int SetHandSize(string playerId)
    {
        // int numCardsInDeck = _repository.Game_Deck.Where(c => c.playerId == playerId).ToList().Count;
        int numCardsInDeck = GetGameDeckByPlayerId(playerId).Count;
        if(numCardsInDeck < Const.IntialCardsPerHand){
            return numCardsInDeck;
        }
        return Const.IntialCardsPerHand;
    }

    private List<Game_Deck> GetGameDeckByPlayerId(string playerId)
    {
        GameDeckHandling gameDeckHandling = new GameDeckHandling(_repository);
        var cards = gameDeckHandling.GetGameDeckByPlayerId(playerId);
        return cards;
    }

    private List<Hand> GetHandByPlayerId(string playerId)
    {
        HandCard handHandling = new HandCard(_repository);
        List<Hand> cards = handHandling.GetHandByPlayerId(playerId);
        return cards;
    }

}