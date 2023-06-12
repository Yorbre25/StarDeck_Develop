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


    private string[] ImportCards(string deckId)
    {
       return this._deckCardHandling.GetCardIdsFromDeck(deckId);
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

}