using StarAPI.DTO.Discovery;
using StarAPI.Context;
using StarAPI.DataHandling.Discovery;
using StarAPI.Constants;

namespace StarAPI.Logic;


public class CardCRUD
{

    private CardHandling _cardHandling;

    public CardCRUD(StarDeckContext context)
    {
        this._cardHandling = new CardHandling(context);
    }


    public List<OutputCard> GetAllCards()
    {
        try
        {
            return _cardHandling.GetAllCards();
        } 
        catch (System.Exception)
        {
            throw new Exception("Error getting cards");
        }
    }

    public OutputCard GetCard(string id){
        try
        {
            return _cardHandling.GetCard(id);
        }
        catch (System.Exception)
        {
            throw new ArgumentException("Invalid id");
        }
    }

    public List<OutputCard> GetCardsByType(string cardType)
    {
        try
        {
            return _cardHandling.GetCardsByType(cardType);
        }
        catch (System.Exception)
        {
            throw new Exception("Error getting cards by type");
        }
    }

    public void AddCard(InputCard inputCard)
    {
        bool isValid = CheckInputValues(inputCard);
        bool alreadyExist = NameAlreadyExists(inputCard.name);

        if(!isValid){
            throw new ArgumentException("Invalid Values");
        }
        if(alreadyExist){
            throw new ArgumentException("Card name already exist");
        }
        _cardHandling.AddCard(inputCard);

    }


    public void DeleteCard(string id){
        try
        {
            _cardHandling.DeleteCard(id);
        }
        catch (System.Exception)
        {
            throw new ArgumentException("Invalid id");
        }
    }

    private bool CheckInputValues(InputCard card){
        bool isValid = true;
        if(card.name.Length < Const.MinNameLenght|| card.name.Length > Const.MaxNameLenght){
            throw new Exception("Invalid name lenght");
        }
        else if(card.energy < Const.MinEnergyValue || card.energy > Const.MaxEnergyValue){
            throw new Exception("Invalid energy value");
        }
        else if(card.cost < Const.MinBattleCost || card.cost > Const.MaxBattleCost){
            throw new Exception("Invalid battle cost");
        }
        else if(card.description.Length > Const.MaxDescriptionLenght){
            throw new Exception("Invalid description lenght");
        }
        return isValid;
    }


    private bool NameAlreadyExists(string cardName)
    {
        return _cardHandling.NameAlreadyExists(cardName);
    }
}


