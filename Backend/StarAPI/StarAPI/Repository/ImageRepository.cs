using Contracts;
using StarAPI.Context;
using StarAPI.Models;

namespace Repository
{
    public class ImageRepository : RepositoryBase<Image>, IImageRepository
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public ImageRepository(StarDeckContext repositoryContext)
            :base(repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        // public Image GetImageByImageString(string imageString)
        // {
        //     return FindByCondition(image => image.imageString.Equals(imageString)).FirstOrDefault();
        // }

    }
}