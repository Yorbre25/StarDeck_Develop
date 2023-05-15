using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.GameLogic;
using StarAPI.Logic.Utils;
using StarAPI.DTOs;

namespace StarAPI.Logic.ModelHandling;

public class GameDeckHandling
{
    private readonly StarDeckContext _context;
    private DeckCardHandling _deckCardHandling;
    private const int s_cardsPerDeck = 18;


    public GameDeckHandling(StarDeckContext context)
    {
        this._context = context;
    }

    public void AddDecks(SetUpValues setUpValues)
    {
        string[] cardsFromDeck = GetCardIds(setUpValues.player1DeckId);
        AddCardsToDeck(setUpValues.player1DeckId, cardsFromDeck);

        string[] cardsFromDeck2 = GetCardIds(setUpValues.player2DeckId);
        AddCardsToDeck(setUpValues.player2DeckId, cardsFromDeck2);
    }

    public string[] GetCardIds(string deckId)
    {
        return _deckCardHandling.GetCardIdsFromDeck(deckId);
    }

    private void AddCardsToDeck(string id, string[] cardIds)
    {
        if(cardIds.Count() != s_cardsPerDeck){
           throw new ArgumentException("Invalid number of cards");
        }
        try
        {
            AddingCardsToDeck(id, cardIds);
        }
        catch (System.Exception)
        {
            
            throw new Exception("Error adding cards to deck");
        }
    }

    private void AddingCardsToDeck(string id, string[] cardIds)
    {
        foreach (var cardId in cardIds)
        {
            Game_Deck gameDeck = new Game_Deck();
            gameDeck.deckId = id;
            gameDeck.cardId = cardId;
            _context.Game_Deck.Add(gameDeck);
        }
    }

}