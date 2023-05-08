using StarAPI.Models;
using StarAPI.Context;
using StarAPI.Utils;

namespace StarAPI.Logic.GameLogic;

public class SetupGame
{
    private readonly StarDeckContext _context;
    private DefaultSetupParam _defaultSetupParam;

    public SetupGame(StarDeckContext context)
    {
        this._context = context;
        this._defaultSetupParam = new DefaultSetupParam();
    }

    public SetupParam GetSetupParam()
    {
        try
        {
            //Change default values for setupParam is not implemented yet

            // return GettingSetupParam();
            return _defaultSetupParam.GetDefaultSetupParam();

        } 
        catch (System.Exception)
        {
            return _defaultSetupParam.GetDefaultSetupParam();
        }
    }

    public SetupParam GettingSetupParam()
    {
        SetupParam? setupParam = new SetupParam();
        setupParam = _context.SetupParam.OrderByDescending(x => x.date).FirstOrDefault();
        return setupParam;
    }


}

