using Contracts;
using StarAPI.Context;
using StarAPI.Models;


namespace StarAPI.DataHandling.Discovery;


public class ImageHandling
{
    private readonly IRepositoryWrapper _repository;


    public ImageHandling(IRepositoryWrapper context)
    {
        this._repository = context;
    }

    public int GetImageId(string image)
    {
        var images = _repository.Image.GetAll();
        var imageEntity = images.Find(r => r.imageString == image);
        if(imageEntity != null){
            return imageEntity.id;
        }
        AddImage(image);
        return GetImageId(image);
    }

    public string GetImage(int id)
    {
        // return _repository.Image.Where(r => r.id == id).FirstOrDefault().imageString;
        var image = _repository.Image.Get(id);
        return image.imageString;
    }
    private void AddImage(string image)
    {
        Image newImage = new Image();
        newImage.imageString = image;
        // _repository.Image.Add(newImage);
        // _repository.SaveChanges();
        _repository.Image.Add(newImage);
        _repository.Save();
    }
}