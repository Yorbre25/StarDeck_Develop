using System;
using System.Security.Cryptography;
using System.Text;
using StarAPI.DTOs;
using StarAPI.Logic.ModelHandling;
using StarAPI.Context;

namespace StarAPI.Logic;

    public class NewPlayerCardGenerator
    {
        private CardHandling _cardHandling;
        private PlayerHandling _playerHandling;
        private PlayerCardHandling _playerCardHandling;
        private static int s_numOfCardsToAssign = 15;
        private static string s_typeOfCardToAssign = "Básica";

    public NewPlayerCardGenerator(StarDeckContext _context)
    {
        this._cardHandling = new CardHandling(_context);
        this._playerHandling = new PlayerHandling(_context);
        this._playerCardHandling = new PlayerCardHandling(_context);
    }
    public void GenerateCardsForNewPlayer(string playerId)
    {
        bool playerExists = _playerHandling.IdAlreadyExists(playerId);
        bool alreadyHasCards;
        if (!playerExists)
        {
            throw new Exception("Invalid player id");
        }
        alreadyHasCards = _playerCardHandling.PlayerAlreadyHasCards(playerId);
        if (alreadyHasCards)
        {
            throw new Exception("Player already has cards");
        }

        GenerateCards(playerId);
    }

    private List<OutputCard> GetBasicCards()
    {
        List<OutputCard> basicCards = _cardHandling.GetCardsWith(s_typeOfCardToAssign);
        return basicCards;
    }

    private void GenerateCards(string playerId)
    {
        List<OutputCard> basicCards = GetBasicCards();
        if (basicCards.Count() < s_numOfCardsToAssign)
        {
            throw new Exception("Not enough cards to assign");
        }
        AssignCards(playerId, basicCards);
    }

    private List<OutputCard> AssignCards(string playerId, List<OutputCard> basicCards)
    {
        List<OutputCard> cardsToAssign = new List<OutputCard>();
        for(var i = 0; i < s_numOfCardsToAssign; i++)
        {
            OutputCard card = GetRandomCard(basicCards);
            _playerCardHandling.AssignCard(playerId, card.id);
            basicCards.Remove(card);
        }
        return cardsToAssign;
    }

    public OutputCard GetRandomCard(List<OutputCard> cards)
    {
        Random random = new Random();
        int index = random.Next(cards.Count);
        return cards[index];
    }

}