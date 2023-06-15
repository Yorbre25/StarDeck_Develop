namespace Contracts
{
    public interface IRepositoryWrapper
    {
        ICardTypeRepository CardType { get; }
        IGameRepository Game { get; }
        IGameDeckRepository GameDeck { get; }
        IGamePlayerRepository GamePlayer { get; }
        IGamePlanetRepository GamePlanet { get; }
        IDeckRepository Deck { get; }
        IDeckCardRepository DeckCard { get; }
        ICardRepository Card { get; }
        IPlanetRepository Planet { get; }
        IImageRepository Image { get; }
        IRaceRepository Race { get; }
        IPlanetTypeRepository PlanetType { get; }
        IPlayerRepository Player { get; }
        IPlayerCardRepository PlayerCard { get; }
        IGameTableRepository GameTable { get; }
        ICountryRepository Country { get; }
        IHandRepository Hand { get; }
        IMatchPlayerRepository MatchPlayer { get; }
        void Save();
    }
}