using Contracts;
using Moq;

internal class MockRepositoryWrapper
{
    public static Mock<IRepositoryWrapper> GetMock()
    {
        var mock = new Mock<IRepositoryWrapper>();
        var cardTypeRepoMock = MockICardTypeRepository.GetMock();


        mock.Setup(m => m.CardType).Returns(() => cardTypeRepoMock.Object);
        // mock.Setup(m => m.Account).Returns(() => new Mock<IAccountRepository>().Object);
        mock.Setup(m => m.Save()).Callback(() => { return; });
        return mock;
    }
}