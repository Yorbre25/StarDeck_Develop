using Contracts;
using Moq;

internal class MockRepositoryWrapper
{
    public static Mock<IRepositoryWrapper> GetMock()
    {
        var mock = new Mock<IRepositoryWrapper>();
        var cardTypeRepoMock = MockICardTypeRepository.GetMock();
        var planetRepoMock = MockIPlanetRepository.GetMock();
        var planetTypeRepoMock = MockIPlanetTypeRepository.GetMock();
        var imageRepoMock = MockIImageTypeRepository.GetMock();
        var gameDeckRepoMock = MockIGameDeckRepository.GetMock();
        var handRepoMock = MockIHandRepository.GetMock();
        var deckCardRepoMock = MockIDeckCardRepository.GetMock();
        var cardRepoMock = MockICardRepository.GetMock();
        var raceRepoMock = MockIRaceRepository.GetMock();


        mock.Setup(m => m.CardType).Returns(() => cardTypeRepoMock.Object);
        mock.Setup(m => m.Planet).Returns(() => planetRepoMock.Object);
        mock.Setup(m => m.PlanetType).Returns(() => planetTypeRepoMock.Object);
        mock.Setup(m => m.Image).Returns(() => imageRepoMock.Object);
        mock.Setup(m => m.GameDeck).Returns(() => gameDeckRepoMock.Object);
        mock.Setup(m => m.Hand).Returns(() => handRepoMock.Object);
        mock.Setup(m => m.DeckCard).Returns(() => deckCardRepoMock.Object);
        mock.Setup(m => m.Card).Returns(() => cardRepoMock.Object);
        mock.Setup(m => m.Race).Returns(() => raceRepoMock.Object);
        mock.Setup(m => m.Save()).Callback(() => { return; });
        return mock;
    }
}