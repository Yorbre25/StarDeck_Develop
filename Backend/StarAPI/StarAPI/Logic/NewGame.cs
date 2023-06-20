using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.Logic.Mappers;
using Contracts;

namespace StarAPI.Logic;

public class NewGame
{

    private readonly IRepositoryWrapper _repository;
    private static string s_idPrefix = "G";
    private IdGenerator _idGenerator = new IdGenerator();
    private GameMapper _gameMapper;


    public NewGame(IRepositoryWrapper repository)
    {
        _repository = repository;
        this._gameMapper = new GameMapper(repository);
    }

    internal OutputSetupValues SetupNewGame(SetupValues setupValues)
    {
        try
        {
            return Setup(setupValues);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

    private OutputSetupValues Setup(SetupValues setUpValues)
    {
        string gameId = GenerateId();
        SetupTable(gameId);
        SetupPlayers(setUpValues, gameId);
        SetupGameDeck(setUpValues, gameId);
        
        StarAPI.Models.Game newGame = _gameMapper.FillNewGame(setUpValues, gameId);
        _repository.Game.Add(newGame);
        _repository.Save();

        string deckId1 = setUpValues.player1DeckId;
        string deckId2 = setUpValues.player2DeckId;
        return _gameMapper.FillOutputSetupValues(newGame, deckId1, deckId2);
        
    }

    private void SetupGameDeck(SetupValues setupValues, string gameId)
    {
        NewGameDeck newGameDeck = new NewGameDeck(_repository);
        newGameDeck.SetupNewGameDeck(setupValues, gameId);
    }

    private void SetupPlayers(SetupValues setUpValues, string gameId)
    {
        SetUpPlayerForGame setUpPlayerForGame = new SetUpPlayerForGame(_repository);
        setUpPlayerForGame.SetupPlayer(setUpValues, gameId);
    }

    private void SetupTable(string gameId)
    {
        NewTable newTable = new NewTable(_repository);
        newTable.SetupTable(gameId);
    }

    private string GenerateId()
    {
        string id = "";
        bool alreadyExists = true;
        while (alreadyExists)
        {
            id = _idGenerator.GenerateId(s_idPrefix);
            alreadyExists = IdAlreadyExists(id);
        }
        return id;
    }

    private bool IdAlreadyExists(string id){
        StarAPI.Models.Game? game;
        game = _repository.Game.Get(id);
        if(game == null){
            return false;
        }
        return true;
    }
}
