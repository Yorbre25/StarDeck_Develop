using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DTO.Game;
using StarAPI.DataHandling.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Models;

namespace StarAPI.Logic.Mappers;

public class GameMapper
{
    private StarDeckContext _context;
    private RaceHandling _raceHandling;
    private CardTypeHandling _cardTypeHandling;
    private PlayerHandling _playerHandling;
    private ImageHandling _imageHandling;
    private DeckHandling _deckHandling;
    private static int s_maxTurns = 10;
    private static int s_timePerTurn = 20;


    public GameMapper(StarDeckContext context)
    {
        _context = context;
        _raceHandling = new RaceHandling(_context);
        _cardTypeHandling = new CardTypeHandling(_context);
        _imageHandling = new ImageHandling(_context);
        _playerHandling = new PlayerHandling(_context);
        _deckHandling = new DeckHandling(_context);
    }

    public OutputSetupValues FillOutputSetupValues(StarAPI.Models.Game game, string deckId1, string deckId2)
    {
        OutputSetupValues outputSetupValues = new OutputSetupValues
        {
            id = game.id,
            totalTurns = game.maxTurns,
            timePerTurn = game.timePerTurn,
            currentTurn = game.turn,
            player1Id = game.player1Id,
            player2Id = game.player2Id,
            usernamePlayer1 = _playerHandling.GetUsername(game.player1Id),
            usernamePlayer2 = _playerHandling.GetUsername(game.player2Id),
            deckNamePlayer1 = _deckHandling.GetDeckName(deckId1),
            deckNamePlayer2 = _deckHandling.GetDeckName(deckId2)
        };
        return outputSetupValues;
    }

    public StarAPI.Models.Game FillNewGame(SetupValues setUpValues, string gameId)
    {
        StarAPI.Models.Game newGame = new StarAPI.Models.Game
        {
            id = gameId,
            timeStarted = DateTime.Now,
            player1Id = setUpValues.player1Id,
            player2Id = setUpValues.player2Id,
            maxTurns = s_maxTurns,
            timePerTurn = s_timePerTurn,
            turn = 0
        };
        return newGame;
    }


}
