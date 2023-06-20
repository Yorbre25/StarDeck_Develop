using Contracts;
using Moq;
using StarAPI.Models;

internal class MockIImageTypeRepository
{
    public static Mock<IImageRepository> GetMock()
    {
        var mock = new Mock<IImageRepository>();
        var images = new List<Image>()
        {
            new Image()
            {
                id = 1,
                imageString = "Image 1",
            },
            new Image()
            {
                id = 2,
                imageString = "Image 2",
            },
            new Image()
            {
                id = 3,
                imageString = "Image 3",
            },
            new Image()
            {
                id = 4,
                imageString = "Image 4",
            },
            new Image()
            {
                id = 5,
                imageString = "Image 5",
            }
        };
        // Set up
      

        mock.Setup(m => m.Get(It.IsAny<int>()))
            .Returns((int id) => images.FirstOrDefault(ct => ct.id == id));
        
        mock.Setup(m => m.GetAll())
            .Returns(images);

        mock.Setup(m => m.Add(It.IsAny<Image>()))
            .Callback(() => {return;});

        mock.Setup(m => m.Delete(It.IsAny<Image>()))
            .Callback(() => {return;});
        return mock;
    }

}