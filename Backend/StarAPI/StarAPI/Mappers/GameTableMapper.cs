using StarAPI.Context;
using StarAPI.DTO.Discovery;
using StarAPI.DataHandling.Discovery;
using StarAPI.Logic.Utils;
using StarAPI.Models;
using StarAPI.DTO.Game;
using Contracts;

namespace StarAPI.Logic.Mappers;

public class GameTableMapper
{
    private CardCRUD _cardCRUD;


    public GameTableMapper(IRepositoryWrapper repository)
    {
        _cardCRUD = new CardCRUD(repository);
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
