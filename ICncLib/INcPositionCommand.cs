using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICNCLib
{
   public  interface INcPositionCommand:INcElement
    {
        IMachinePosition MachinePosition { get; set; }
        IFeedrate Feedrate { get; set; }
        string AsNcString(INcMachine ncMachine, IMachinePosition previousPosition, MoveType moveType);
    }
}
