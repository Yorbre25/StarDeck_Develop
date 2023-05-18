using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.GameLogic;
using StarAPI.Logic.Utils;
using StarAPI.DTOs;

namespace StarAPI.Logic.ModelHandling;

public class GameDeckCardHandling
{
    private readonly StarDeckContext _context;
    private DeckCardHandling _deckCardHandling;
    private IdGenerator _idGenerator = new IdGenerator();
    private const string s_idPrefix = "GM";
    private const int s_cardsPerDeck = 18;


    public GameDeckCardHandling(StarDeckContext context)
    {
        this._context = context;
        this._deckCardHandling = new DeckCardHandling(_context);
    }

    public void AddDecks(string playerId, string deckId)
    {
        string[] cardsFromDeck = ImportCards(deckId);

        Game_Deck newGameDeck = new Game_Deck();
        newGameDeck.playerId = playerId;
        newGameDeck.deckId = deckId;
        _context.Game_Deck.Add(newGameDeck);
        AddCardsToDeck(deckId, cardsFromDeck);
    }

    private string[] ImportCards(string deckId)
    {
       return this._deckCardHandling.GetCardIdsFromDeck(deckId);
    }

    public string[] GetCardIds(string deckId)
    {
        try
        {
            return GettingCardIds(deckId);
        }
        catch (System.Exception)
        {
            
            throw new Exception("Error getting cards from deck");
        }
    }

    public string[] GettingCardIds(string deckId)
    {
        return _context.Game_Deck_Card.Select(x => x.cardId).ToArray();
    }

    private void AddCardsToDeck(string deckId, string[] cardIds)
    {
        try
        {
            AddingCardsToDeck(deckId, cardIds);
        }
        catch (System.Exception)
        {
            
            throw new Exception("Error adding cards to deck");
        }
    }

    private void AddingCardsToDeck(string deckId, string[] cardIds)
    {
        foreach (var cardId in cardIds)
        {
            Game_Deck_Card gameDeck = new Game_Deck_Card();
            gameDeck.deckId = deckId;
            gameDeck.cardId = cardId;
            _context.Game_Deck_Card.Add(gameDeck);
        }
    }


    internal void EndGame(string deckId)
    {
        DeleteCards(deckId);
    }

    private void DeleteCards(string deckId)
    {
        List<Game_Deck_Card> cards = _context.Game_Deck_Card.Where(c => c.deckId == deckId).ToList();
        _context.Game_Deck_Card.RemoveRange(cards);
    }

    private void DeleteCard(string cardId)
    {
        Game_Deck_Card card = _context.Game_Deck_Card.Where(c => c.cardId == cardId).FirstOrDefault();
        _context.Game_Deck_Card.Remove(card);
    }

    internal string DrawCard(string playerId)
    {
        //Verificar que el mazo no esté vació
        var deck = _context.Game_Deck.Where(d => d.playerId == playerId).FirstOrDefault();
        //verificar que no esté vació
        string[] cardIds = GetCardIds(deck.deckId);
        string cardId = PickCard(cardIds);
        DeleteCard(cardId);
        return cardId;

    }

    private string PickCard(string[] cardIds)
    {
        Random random = new Random();
        int randomIndex = random.Next(0, cardIds.Length);
        string randomCardId = cardIds[randomIndex];
        return randomCardId;
    }

    internal void Delete(string deckId)
    {
        Game_Deck deck = GetDeck(deckId);
        DeleteCardsFromDeck(deckId);
        DeleteDeck(deck);
    }

    private void DeleteDeck(Game_Deck deck)
    {
        _context.Game_Deck.Remove(deck);
    }

    private void DeleteCardsFromDeck(string deckId)
    {
        List<Game_Deck_Card> cards = GetDeckCards(deckId);
        _context.Game_Deck_Card.RemoveRange(cards);
        _context.SaveChanges();
    }

    private List<Game_Deck_Card> GetDeckCards(string deckId)
    {
        var deckCards = _context.Game_Deck_Card.ToList();
        return deckCards.FindAll(d => d.deckId == deckId);
    }

    private Game_Deck GetDeck(string id)
    {
        return _context.Game_Deck.FirstOrDefault(d => d.deckId == id);
    }
}