using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICNCLib;
using System.Xml;
namespace CNCLib
{
    public class NcMachine:INcMachine
    {
       
        public IMachinePosition HomePosition { get; set; }
        public IMachinePosition ProgStartPosition { get; set; }
        public ControllerType ControllerType { get; private set; }         
        public MachineGeometry MachineGeometry { get; private set; }
        public IMachineCode MachineCode { get; }
        public FeedrateUnits FeedrateUnits { get; private set; }
        public FeedrateUnits InverseFeedrateUnits { get; private set; }
        public List<INcAxis> Axes { get; }
        CommandDictionary commandDictionary;
        public NcMachine(ControllerType controllerType,MachineGeometry machineGeometry)
        {
            
            ControllerType = controllerType;
            MachineGeometry = machineGeometry;
           
            HomePosition = new MachinePosition();
            ProgStartPosition = new MachinePosition();
            MachineCode = new CNCMachineCode();
            commandDictionary = new CommandDictionary();
            FeedrateUnits = FeedrateUnits.InPerMin;
            InverseFeedrateUnits = FeedrateUnits.InverseMins;
        }
        public string MoveGCode(MoveType moveType, BlockType blockType)
        {
            try
            {
                string result = "";
                switch (moveType)
                {
                    case MoveType.ABSOLUTE:
                        result += MachineCode.Absolute;
                        break;
                    case MoveType.RELATIVE:
                        result += MachineCode.Relative;
                        break;
                }
                result += " ";
               
                switch (blockType)
                {
                    case BlockType.CCWARC:
                        result+= MachineCode.CcwArc;
                        break;
                    case BlockType.CWARC:
                        result += MachineCode.CwArc;
                        break;
                    case BlockType.RAPID:
                        result += MachineCode.Rapid;
                        break;
                    case BlockType.LINEAR:
                    default:
                        result += MachineCode.LinearMove;
                        break;

                }
                
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string CommandString(MachineCommandType machineCommandType)
        {
            try
            {
               return commandDictionary.AsNcString(machineCommandType);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void AddAxis(INcAxis axis)
        {
            Axes.Add(axis);
        }

    }
}
