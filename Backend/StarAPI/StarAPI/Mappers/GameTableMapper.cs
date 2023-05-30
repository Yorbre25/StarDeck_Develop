using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Models;
using StarAPI.DTO.Game;

namespace StarAPI.Logic.Mappers;

public class GameTableMapper
{
    private StarDeckContext _context;
    private RaceHandling _raceHandling;
    private CardTypeHandling _cardTypeHandling;
    private PlayerHandling _playerHandling;
    private CardCRUD _cardCRUD;
    private ImageHandling _imageHandling;
    private DeckHandling _deckHandling;
    private static int s_maxTurns = 10;
    private static int s_timePerTurn = 20;


    public GameTableMapper(StarDeckContext context)
    {
        _context = context;
        _raceHandling = new RaceHandling(_context);
        _cardTypeHandling = new CardTypeHandling(_context);
        _imageHandling = new ImageHandling(_context);
        _playerHandling = new PlayerHandling(_context);
        _deckHandling = new DeckHandling(_context);
        _cardCRUD = new CardCRUD(_context);
    }

  

    public List<GameTable> FillNewGameTable(InputTableLayout tableLayout)
    {
        string gameId = tableLayout.gameId;
        string playerId = tableLayout.playerId;
        Dictionary<string, string> layout = tableLayout.layout;

        List<GameTable> gameTables = new List<GameTable>();
        foreach(KeyValuePair<string, string> entry in layout)
        {
            GameTable gameTable = new GameTable
            {
                gameId = gameId,
                playerId = playerId,
                planetId = entry.Key,
                cardId = entry.Value,
                battlePoints = _cardCRUD.GetCard(entry.Value).energy
            };
            gameTables.Add(gameTable);
        }
        return gameTables;
    }

}
