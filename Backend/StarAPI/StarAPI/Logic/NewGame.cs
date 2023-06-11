using StarAPI.Logic.Utils;
using StarAPI.Context;
using StarAPI.DTO.Game;

namespace StarAPI.Logic;

public class NewGame
{

    private readonly StarDeckContext _context;

    private static string s_idPrefix = "G";
    private IdGenerator _idGenerator = new IdGenerator();


    public NewGame(StarDeckContext context)
    {
        _context = context;
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
        
        OutputSetupValues outputSetupValues = new OutputSetupValues();
        return outputSetupValues;
        
    }

    private void SetupGameDeck(SetupValues setupValues, string gameId)
    {
        NewGameDeck newGameDeck = new NewGameDeck(_context);
        newGameDeck.SetupNewGameDeck(setupValues, gameId);
    }

    private void SetupPlayers(SetupValues setUpValues, string gameId)
    {
        SetUpPlayerForGame setUpPlayerForGame = new SetUpPlayerForGame(_context);
        setUpPlayerForGame.SetupPlayer(setUpValues, gameId);
    }

    private void SetupTable(string gameId)
    {
        NewTable newTable = new NewTable(_context);
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
        game = _context.Game.FirstOrDefault(c => c.id == id);
        if(game == null){
            return false;
        }
        return true;
    }
}
