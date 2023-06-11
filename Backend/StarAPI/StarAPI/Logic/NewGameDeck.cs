using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.DataHandling.Discovery;
using StarAPI.Models;

namespace StarAPI.Logic;

public class NewGameDeck
{

    private readonly StarDeckContext _context;
    private DeckCardHandling _deckCardHandling;
    private IdGenerator _idGenerator = new IdGenerator();


    public NewGameDeck(StarDeckContext context)
    {
        _context = context;
        _deckCardHandling = new DeckCardHandling(_context);
    }

    internal void SetupNewGameDeck(SetupValues setupValues, string gameId)
    {
        try
        {
            Setup(setupValues, gameId);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void Setup(SetupValues setupValues, string gameId)
    {
        AreDecksValid(setupValues);
        string[] players = new string[] { setupValues.player1Id, setupValues.player2Id };
        string[] decks = new string[] { setupValues.player1DeckId, setupValues.player2DeckId };
        for (int i = 0; i < players.Length; i++)
        {
            AddCardsToDeck(gameId, players[i], decks[i]);
        }
    }

    private void AddCardsToDeck(string gameId, string playerId, string deckId)
    {
        string[] cardIds = _deckCardHandling.GetCardIdsFromDeck(deckId);
        AddCardsToDeck(gameId, playerId, cardIds);
    }

    private void AddCardsToDeck(string gameId, string playerId, string[] cardIds)
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

    private void AreDecksValid(SetupValues setupValues)
    {
        string[] players = new string[] { setupValues.player1Id, setupValues.player2Id };
        string[] decks = new string[] { setupValues.player1DeckId, setupValues.player2DeckId };
        for (int i = 0; i < players.Length; i++)
        {
            string playerId = players[i];
            string deckId = decks[i];
            IsDeckValid(playerId, deckId);
        }
    }

    private void IsDeckValid(string playerId, string deckId)
    {
        bool deckExists = AlreadyExists(deckId);
        if (!deckExists)
        {
            throw new ArgumentException("Deck does not exist");
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
}