using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Logic.Game;
using StarAPI.Logic.Utils;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using Contracts;

namespace StarAPI.DataHandling.Game;

public class GameDeckHandling
{
    private readonly IRepositoryWrapper _repository;
    private DeckCardHandling _deckCardHandling;
    private IdGenerator _idGenerator = new IdGenerator();
    private RandomTools _randomTools = new RandomTools();


    public GameDeckHandling(IRepositoryWrapper context)
    {
        this._repository = context;
        this._deckCardHandling = new DeckCardHandling(_repository);
    }


    private string[] ImportCards(string deckId)
    {
       return this._deckCardHandling.GetCardIdsFromDeck(deckId);
    }
    
    public void RemoveCardFromDeck(string playerId, string cardId)
    {
    //    Game_Deck? card = _repository.Game_Deck.Where(c => c.playerId == playerId && c.cardId == cardId).FirstOrDefault();
        // _repository.Game_Deck.Remove(card);
        List<Game_Deck> cards = GetGameDeckByPlayerId(playerId);
        Game_Deck card = cards.Find(c => c.cardId == cardId);
        _repository.GameDeck.Delete(card);
        _repository.Save();
    }

    internal List<Game_Deck> GetGameDeckByGameId(string gameId)
    {
        List<Game_Deck> decks = _repository.GameDeck.GetAll();
        return decks.FindAll(d => d.gameId == gameId);
    }
    internal List<Game_Deck> GetGameDeckByPlayerId(string playerId)
    {
        List<Game_Deck> decks = _repository.GameDeck.GetAll();
        return decks.FindAll(d => d.playerId == playerId);
    }
}