using StarAPI.Context;
using StarAPI.DTO.Game;
using StarAPI.DataHandling.Discovery;
using StarAPI.Constants;
using Contracts;

namespace StarAPI.Logic.Mappers;

public class GameMapper
{
    private PlayerHandling _playerHandling;
    private DeckHandling _deckHandling;


    public GameMapper(IRepositoryWrapper repository)
    {
        _playerHandling = new PlayerHandling(repository);
        _deckHandling = new DeckHandling(repository);
    }

    public OutputSetupValues FillOutputSetupValues(StarAPI.Models.Game game, string deckId1, string deckId2)
    {
        OutputSetupValues outputSetupValues = new OutputSetupValues
        {
            id = game.id,
            totalTurns = game.maxTurns,
            timePerTurn = Const.TimePerTurn,
            currentTurn = game.turn,
            initialCardPoints = Const.InitialCardPoints,
            cardsPerPlanet = Const.CardsPerPlanet,
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
            maxTurns = Const.MaxTurns,
            endTurnCounter = Const.EndTurnCounter,
            endGameCounter = Const.EndGameCounter,
            xpGain = Const.XpGain,
            turn = 1,
        };
        return newGame;
    }


}
