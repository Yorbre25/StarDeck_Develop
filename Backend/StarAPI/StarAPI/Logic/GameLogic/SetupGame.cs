// using StarAPI.Models;
// using StarAPI.Context;
// using StarAPI.Utils;

// namespace StarAPI.Logic.GameLogic;

// public class SetupGame
// {
//     private readonly StarDeckContext _context;
//     private readonly IGame _game;
//     private DefaultSetupParam _defaultSetupParam;

//     public SetupGame(StarDeckContext context, IGame game)
//     {
//         this._context = context;
//         this._game = game;
//         this._defaultSetupParam = new DefaultSetupParam();
//     }

//     public SetupParam GetSetupParam()
//     {
//         try
//         {
            
//             return _defaultSetupParam.GetDefaultSetupParam();

//         } 
//         catch (System.Exception)
//         {
//             return _defaultSetupParam.GetDefaultSetupParam();
//         }
//     }

//     public SetupParam GettingSetupParam()
//     {
//         SetupParam? setupParam = new SetupParam();
//         setupParam = _context.SetupParam.OrderByDescending(x => x.date).FirstOrDefault();
//         SetSetupParam(setupParam);
//         return setupParam;
//     }

//     public void SetSetupParam(SetupParam setupParam)
//     {
//         _game.SetupParam(setupParam);
//     }


// }

