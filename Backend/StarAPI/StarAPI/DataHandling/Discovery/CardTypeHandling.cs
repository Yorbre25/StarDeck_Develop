using Microsoft.EntityFrameworkCore;
using StarAPI.Models;
using StarAPI.Context;
using System.Linq.Expressions;
using Contracts;

namespace StarAPI.DataHandling.Discovery;

public class CardTypeHandling
{
    private readonly IRepositoryWrapper _repository;

    public CardTypeHandling(IRepositoryWrapper repository)
    {
        this._repository = repository;
    }

    public List<CardType> GetAllCardTypes()
    {
        return _repository.CardType.GetAll();
    }


    public string Get(int id)
    {
        try
        {
            return GetCardTypeName(id);
        }
        catch (System.Exception)
        {
            throw new ArgumentException("Invalid CardType id");
        }
    }

    public string GetCardTypeName(int id)
    {

        CardType cardType = _repository.CardType.Get(id);
        return cardType.typeName;
    }


    public void AddCardType(string typeName)
    {
        bool alreadyExist = AlreadyExists(typeName);
        if(alreadyExist){
            throw new ArgumentException("CardType already exist");
        }
        InsertCardType(typeName);

    }

    public void InsertCardType(string typeName){
        var cardType = new CardType {typeName = typeName};
        _repository.CardType.Add(cardType);
        _repository.Save();
    }



    private bool AlreadyExists(string typeName){
        var cardTypes = _repository.CardType.GetAll();
        var cardType = cardTypes.FirstOrDefault(c => c.typeName == typeName);
        if(cardType == null){
            return false;
        }
        return true;
    }

    private bool AlreadyExists(int id){
        CardType? cardType = new CardType();
        cardType = _repository.CardType.Get(id);
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
            throw new ArgumentException("Invalid CardType id");
        }
        else
        {
            DeletingCardType(id);
        }
    }

    private void DeletingCardType(int id)
    {
        CardType cardType = _repository.CardType.Get(id);
        _repository.CardType.Delete(cardType);
        _repository.Save();
    }

}