using System.ComponentModel.DataAnnotations;
using StarAPI.Models;

namespace StarAPI.Utils
{
    public class DefaultSetupParam
    {
        private static SetupParam s_defaultSetupParam = new SetupParam();
        
        public DefaultSetupParam()
        {
            int totalTurns = 10;
            int timePerTurn = 20;
            s_defaultSetupParam.totalTurns = totalTurns;
            s_defaultSetupParam.timePerTurn = timePerTurn;
        }

        public SetupParam GetDefaultSetupParam()
        {
            return s_defaultSetupParam;
        }
    }
}
