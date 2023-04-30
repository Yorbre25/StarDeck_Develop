using Microsoft.EntityFrameworkCore;
using StarAPI.Logic.ModelHandling;
using StarAPI.Data;
using StarAPI.Context;

namespace StarAPI.DataTesting;


public class AddPlaceholderData
{
    private readonly StarDeckContext _context;
    private  CardHandling _cardHandling;
    private DataForTest _dataForTest;

    public AddPlaceholderData(StarDeckContext context)
    {
        this._context = context;
        this._dataForTest = new DataForTest();
        this._cardHandling = new CardHandling(context);
    }


    public void AddData()
    {
        dropPreviousData();
        AddCards();
    }

    private void dropPreviousData()
    {
        _context.Database.ExecuteSqlRaw("TRUNCATE TABLE Card");
    }

    public void AddCards()
    {
        foreach(var card in _dataForTest.cards)
        {
            _cardHandling.AddCard(card);
        }
    }
}







