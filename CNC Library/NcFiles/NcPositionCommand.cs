using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICNCLib;
namespace CNCLib
{
    public class NcPositionCommand :NcElement
    {
        public IMachinePosition MachinePosition { get;private set; }
        public IFeedrate Feedrate { get;private set; }

        BlockType blockType;
        IMachinePosition prevPosition;
        MoveType moveType; 
        public NcPositionCommand(int lineNumber, IMachinePosition machinePosition,
           IFeedrate feedrate,BlockType blockType )
        {
            MachinePosition = machinePosition;            
            this.blockType = blockType;
            moveType = MoveType.ABSOLUTE;
            Feedrate = feedrate;
            LineNumber = lineNumber;
        }
        public NcPositionCommand(int lineNumber, IMachinePosition machinePosition,NcPositionCommand previousPositionCommand,
           IFeedrate feedrate, BlockType blockType)
        {
            MachinePosition = machinePosition;
            this.blockType = blockType;
            moveType = MoveType.RELATIVE;
            Feedrate = feedrate;
            LineNumber = lineNumber;
            prevPosition = previousPositionCommand.MachinePosition;
        }
        public override string AsNcString(INcMachine ncMachine)
        {
            try
            {
                string line = ncMachine.MachineCode.LineNumberPrefix + LineNumber.ToString() + " ";

                if(Feedrate.Inverted)
                {
                    line += ncMachine.MachineCode.InverseFeed + " ";
                }
                line += ncMachine.MoveGCode(this.moveType, this.blockType)+" ";
                line += MachinePosition.AsNcString( ncMachine, prevPosition,blockType, moveType) + " " + Feedrate.AsNcString(ncMachine);
                return line;
            }
            catch (Exception)
            {

                throw;
            }
           
        }       
    }
}
