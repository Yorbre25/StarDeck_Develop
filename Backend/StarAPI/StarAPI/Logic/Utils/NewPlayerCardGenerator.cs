using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Context;
using StarAPI.Logic.Utils;

namespace StarAPI.Logic;

    public class NewPlayerCardGenerator
    {
        private CardCRUD _cardCRUD;
        private PlayerHandling _playerHandling;
        private PlayerCardHandling _playerCardHandling;
        private RandomTools _randomTools = new RandomTools();
        private static int s_numOfCardsToAssign = 15;
        private static string s_typeOfCardToAssign = "Básica";

    public NewPlayerCardGenerator(StarDeckContext _context)
    {
        this._cardCRUD = new CardCRUD(_context);
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
        List<OutputCard> basicCards = _cardCRUD.GetCardsByType(s_typeOfCardToAssign);
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
            OutputCard card = _randomTools.GetRandomElement<OutputCard>(basicCards);
            _playerCardHandling.AssignCard(playerId, card.id);
            basicCards.Remove(card);
        }
        return cardsToAssign;
    }

}