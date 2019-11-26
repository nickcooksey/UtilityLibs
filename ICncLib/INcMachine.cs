using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICNCLib
{
    public enum MachineCommandType
    {
        JET_ON,
        JET_OFF,
        ABR_ON,
        ABR_OFF,
        DATA_COLL_START,
        DATA_COLL_END
    }
    public interface INcMachine
    {
        IMachinePosition HomePosition { get; set; }
        IMachinePosition ProgStartPosition { get; set; }
       
        ControllerType ControllerType { get; }
        MachineGeometry MachineGeometry { get; }       
        FeedrateUnits FeedrateUnits { get; }
        FeedrateUnits InverseFeedrateUnits { get; }
        IMachineCode MachineCode { get; }
        List<INcAxis> Axes { get;  }
        void AddAxis(INcAxis axis);
        string CommandString(MachineCommandType machineCommandType);
        string MoveGCode(MoveType moveType, BlockType blockType);
    }
}
