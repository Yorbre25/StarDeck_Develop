using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
namespace StarAPI.DataHandling.Discovery;

public class CardTypeHandling
{
    private readonly StarDeckContext _context;

    public CardTypeHandling(StarDeckContext context)
    {
        this._context = context;
    }

    public List<CardType> GetAllCardTypes()
    {
        return _context.CardType.ToList();
    }


    public string GetCardType(int id)
    {
        try
        {
            return GetCardTypeName(id);
        }
        catch (System.Exception)
        {
            throw new ArgumentException("Invalid id");
        }
    }

    public string GetCardTypeName(int id)
    {
        CardType? cardType = _context.CardType.FirstOrDefault(r => r.id == id);
        if (cardType == null)
        {
            throw new ArgumentException("CardType does not exist");
        }
        return cardType.typeName;
    }


    public void AddCardType(string raceName)
    {
        bool isNameValid = CheckInputName(raceName);
        bool alreadyExist = AlreadyExists(raceName);

        if(!isNameValid){
            throw new ArgumentException("Invalid name");
        }
        if(alreadyExist){
            throw new ArgumentException("CardType already exist");
        }
        InsertCardType(raceName);

    }

    public void InsertCardType(string raceName){
        var cardType = new CardType {typeName = raceName};
        _context.CardType.Add(cardType);
        _context.SaveChanges();
    }

    private bool CheckInputName(string raceName){
        return true;
    }


    private bool AlreadyExists(string typeName){
        var cardType = _context.CardType.FirstOrDefault(r => r.typeName == typeName);
        if(cardType == null){
            return false;
        }
        return true;
    }

    private bool AlreadyExists(int id){
        CardType? cardType = new CardType();
        cardType = _context.CardType.FirstOrDefault(r => r.id == id);
        if(cardType == null){
            return false;
        }
        return true;
    }

    public void DeleteCardType(int id)
    {
        bool alreadyExists = AlreadyExists(id);
        if(!alreadyExists)
        {
            throw new ArgumentNullException("CardType does not exist");
        }
        else
        {
            DeletingCardType(id);
        }
    }

    private void DeletingCardType(int id)
    {
        var cardType = _context.CardType.FirstOrDefault(r => r.id == id);
        _context.CardType.Remove(cardType);
        _context.SaveChanges();
    }

}