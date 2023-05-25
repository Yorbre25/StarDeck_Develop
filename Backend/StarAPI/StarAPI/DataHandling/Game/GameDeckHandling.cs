using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.Game;
using StarAPI.Logic.Utils;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;

namespace StarAPI.DataHandling.Game;

public class GameDeckCardHandling
{
    private readonly StarDeckContext _context;
    private DeckCardHandling _deckCardHandling;
    private IdGenerator _idGenerator = new IdGenerator();
    private RandomTools _randomTools = new RandomTools();


    public GameDeckCardHandling(StarDeckContext context)
    {
        this._context = context;
        this._deckCardHandling = new DeckCardHandling(_context);
    }

    public void SetupDeck(string playerId, string deckId, string gameId)
    {
        IsDeckValid(playerId, deckId);
        string[] cardsFromDeck = ImportCards(deckId);
        AddCardsToDeck(gameId, playerId, cardsFromDeck);
    }

    private void IsDeckValid(string playerId, string deckId)
    {
        bool deckExists = AlreadyExists(deckId);
        if (!deckExists)
        {
            throw new ArgumentException("Deck does not exist");
        }
        bool isPlayersDeck = IsPlayersDeck(playerId, deckId);
        if (!isPlayersDeck)
        {
            throw new ArgumentException("Deck does not belong to player");
        }
    }

    private bool AlreadyExists(string deckId)
    {
        var deck = _context.Deck.FirstOrDefault(d => d.id == deckId);
        if (deck != null)
        {
            return true;
        }
        return false;
    }

    private bool IsPlayersDeck(string playerId, string deckId)
    {
        var decks = _context.Deck.Where(d => d.id == deckId).ToList();
        var playerDecks = decks.Where(d => d.playerId == playerId).ToList();
        if (playerDecks.Count == 0)
        {
            return false;
        }
        var selectedDeck = playerDecks.FirstOrDefault(d => d.id == deckId);
        if (selectedDeck == null)
        {
            return false;
        }
        return true;
    }


    private string[] ImportCards(string deckId)
    {
       return this._deckCardHandling.GetCardIdsFromDeck(deckId);
    }

    private void AddCardsToDeck(string gameId, string playerId, string[] cardIds)
    {
        // try
        // {
            AddingCardsToDeck(gameId, playerId, cardIds);
        // }
        // catch (System.Exception)
        // {
            
        //     throw new Exception("Error adding cards to deck");
        // }
    }

    private void AddingCardsToDeck(string gameId, string playerId, string[] cardIds)
    {
        foreach (var cardId in cardIds)
        {
            Game_Deck gameDeck = new Game_Deck();
            gameDeck.gameId = gameId;
            gameDeck.playerId = playerId;
            gameDeck.cardId = cardId;
            _context.Game_Deck.Add(gameDeck);
        }
    }


    internal void EndGame(string gameId)
    {
        DeleteAllCards(gameId);
    }

    private void DeleteAllCards(string gameId)
    {
        List<Game_Deck> cards = _context.Game_Deck.Where(c => c.gameId == gameId).ToList();
        _context.Game_Deck.RemoveRange(cards);
    }

    private void DeleteCard(string playerId, string cardId)
    {
        Game_Deck card = _context.Game_Deck.Where(c => c.playerId == playerId && c.cardId == cardId).FirstOrDefault();
        _context.Game_Deck.Remove(card);
    }

    internal string DrawCard(string playerId)
    {
        string[] cardIds = GetCardIds(playerId);
        if (cardIds.Count() == 0)
        {
            throw new ArgumentException("Deck is empty");
        }
        string cardId = _randomTools.GetRandomElement<string>(cardIds);
        DeleteCard(playerId, cardId);
        return cardId;

    }

    private string[] GetCardIds(string playerId)
    {
        return _context.Game_Deck.Where(d => d.playerId == playerId).Select(d => d.cardId).ToArray();

    }

    private Game_Deck GetDeckByPlayer(string playerId)
    {
        try
        {
            return _context.Game_Deck.Where(d => d.playerId == playerId).FirstOrDefault();
        }
        catch (System.Exception)
        {
            
            throw new Exception("Error getting deck by player");
        }
    }

    internal int NumCardsInDeck(string playerId)
    {
        var cards = _context.Game_Deck.Where(c => c.playerId == playerId).ToList();
        return cards.Count;
    }
}