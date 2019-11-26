using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICNCLib;
namespace CNCLib
{

    public class CommandDictionary
    {
        Dictionary<MachineCommandType, string> commandDict;
        public CommandDictionary()
        {
            commandDict = new Dictionary<MachineCommandType, string>();
            BuildDictionary();
        }
        void BuildDictionary()
        {
            commandDict.Add(MachineCommandType.JET_ON, "JETON_Mcode");
            commandDict.Add(MachineCommandType.JET_OFF, "JETOFF_Mcode");
            commandDict.Add(MachineCommandType.DATA_COLL_START, "DATA_COLL_START_Mcode");
            commandDict.Add(MachineCommandType.DATA_COLL_END, "DATA_COLL_END_Mcode");

        }
        public string AsNcString(MachineCommandType machineCommandType)
        {
            return commandDict[machineCommandType];
        }
        public string AsNcString(string command)
        {
            try
            {
                string commandStr = command+"_UnknownCommand";
                if(Enum.TryParse<MachineCommandType>(command, out MachineCommandType result))
                {
                    commandStr = commandDict[result];
                }
                 
                return commandStr;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
