using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICNCLib;

namespace CNCLib
{
   

    public class NcActionCommand:NcElement
    {
        MachineCommandType machineCommand;
        public NcActionCommand(int lineNumber, MachineCommandType machineCommand )
        {
            LineNumber = lineNumber;
            this.machineCommand = machineCommand;
        }
        public override string AsNcString(INcMachine ncMachine)
        {
           return ncMachine.CommandString(machineCommand);
        }
    }
}
