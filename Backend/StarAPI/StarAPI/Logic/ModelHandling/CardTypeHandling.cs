using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
namespace StarAPI.Logic.ModelHandling;

/// <summary>
/// Class who creates, deletes and adds card types to the database
/// </summary>
public class CardTypeHandling
{
    private readonly StarDeckContext _context;

    /// <summary>
    /// CardTypeHandling constructor. Initializes the DBContext
    /// </summary>
    /// <param name="context"> Database context</param>
    public CardTypeHandling(StarDeckContext context)
    {
        this._context = context;
    }

    /// <summary>
    /// Returns all card types from the database
    /// </summary>
    /// <returns>A list of Card_Type</returns>
    public List<CardType> GetAllCardTypes()
    {
        return _context.Card_Type.ToList();
    }


    /// <summary>
    /// Returns a card type with the given id
    /// </summary>
    /// <param name="id">Id of card type to be searched</param>
    /// <returns></returns>
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
        CardType? cardType = _context.Card_Type.FirstOrDefault(r => r.id == id);
        if (cardType == null)
        {
            throw new ArgumentException("CardType does not exist");
        }
        return cardType.typeName;
    }


    /// <summary>xc
    /// Top function for adding a card type
    /// </summary>
    /// <param name="raceName"> New card type name</param>
    public void AddCardType(string raceName)
    {
        bool isNameValid = CheckInputName(raceName);
        bool alreadyExist = AlreadyExists(raceName);

        if(!isNameValid){
            throw new ArgumentException("Invalid name");
        }
        if(alreadyExist){
            throw new ArgumentException("Card_Type already exist");
        }
        InsertCardType(raceName);

    }

    /// <summary>
    /// Inserts the new card type to the database
    /// </summary>
    /// <param name="raceName"> New card type name</param>
    public void InsertCardType(string raceName){
        var cardType = new CardType {typeName = raceName};
        _context.Card_Type.Add(cardType);
        _context.SaveChanges();
    }

    /// <summary>
    /// Checks if the name is valid
    /// </summary>
    /// <param name="raceName"> New card type name</param>
    /// <returns></returns>
    private bool CheckInputName(string raceName){
        return true;
    }

    /// <summary>
    /// Checks if the card type already exists
    /// </summary>
    /// <param name="typeName"> Name of the card type</param>
    /// <returns></returns>
    private bool AlreadyExists(string typeName){
        var cardType = _context.Card_Type.FirstOrDefault(r => r.typeName == typeName);
        if(cardType == null){
            return false;
        }
        return true;
    }

    /// <summary>
    /// Checks if the card type already exists
    /// </summary>
    /// <param name="id"> Id of card type to look</param>
    /// <returns></returns>
    private bool AlreadyExists(int id){
        CardType? cardType = new CardType();
        cardType = _context.Card_Type.FirstOrDefault(r => r.id == id);
        if(cardType == null){
            return false;
        }
        return true;
    }

    /// <summary>
    /// Top function for deleting 
    /// </summary>
    /// <param name="id">Id of cardType to be removed</param>
    public void DeleteCardType(int id)
    {
        bool alreadyExists = AlreadyExists(id);
        if(!alreadyExists)
        {
            throw new ArgumentNullException("Card_Type does not exist");
        }
        else
        {
            RemoveCardType(id);
        }
    }

    /// <summary>
    /// Removes card_type with the given id
    /// </summary>
    /// <param name="id">Id of card type to be removed</param>
    private void RemoveCardType(int id)
    {
        var cardType = _context.Card_Type.FirstOrDefault(r => r.id == id);
        _context.Card_Type.Remove(cardType);
        _context.SaveChanges();
    }

}