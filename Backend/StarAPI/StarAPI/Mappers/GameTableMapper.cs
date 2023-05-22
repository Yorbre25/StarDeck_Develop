using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Models;

namespace StarAPI.Logic.Mappers;

public class GameTableMapper
{
    private StarDeckContext _context;
    private RaceHandling _raceHandling;
    private CardTypeHandling _cardTypeHandling;
    private PlayerHandling _playerHandling;
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
    }

  

    public GameTable FillNewGameTable(string[] planetsIds)
    {
        GameTable newGameTable = new GameTable
        {
            planet1Id = planetsIds[0],
            planet2Id = planetsIds[1],
            planet3Id = planetsIds[2]
        };
        return newGameTable;
    }


}
