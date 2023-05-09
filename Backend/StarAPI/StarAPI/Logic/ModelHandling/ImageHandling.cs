using StarAPI.Context;
using StarAPI.Models;


namespace StarAPI.Logic.ModelHandling;


public class ImageHandling
{
    private readonly StarDeckContext _context;


    public ImageHandling(StarDeckContext context)
    {
        this._context = context;
    }

    public int GetImageId(string image)
    {
        bool alreadyExists = _context.Image.Any(r => r.imageString == image);
        if(alreadyExists){
            return _context.Image.Where(r => r.imageString == image).FirstOrDefault().id;
        }
        AddImage(image);
        return GetImageId(image);
    }

    public string GetImage(int id)
    {
        return _context.Image.Where(r => r.id == id).FirstOrDefault().imageString;
    }

    public void AddImage(string image)
    {
        try
        {
            AddingImage(image);
        }
        catch
        {
            throw new Exception("Error adding image");
        }
    }

    private void AddingImage(string image)
    {
        Image newImage = new Image();
        newImage.imageString = image;
        _context.Image.Add(newImage);
        _context.SaveChanges();
    }
}