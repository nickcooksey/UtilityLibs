using GeometryLib;
using System.Collections.Generic;
using ICNCLib;
using IGeometryLib;

namespace CNCLib
{


    /// <summary>
    /// holds location, jet direction feedrate of xsection path entity
    /// </summary>

   
    public class PathEntity :IPathEntity
    {
        public BlockType Type { get; set; }
        public string Comment { get; set; }
        public IMachinePosition Position { get; set; }
        public IMachinePosition PrevPosition { get; set; }
        public IVector3 PositionAsVector3
        {
            get
            {
                return new Vector3(Position.X, Position.Y, Position.Z);
            }
        }
        public bool BAxisRolloverFlag { get; set; }
        public IVector3 DirVector { get; set; }
        public CComp Ccomp { get; set; }
        public IVector3 JetVector { get; set; }
        public IVector3 SurfNormal { get; set; }
        public IFeedrate Feedrate { get; set; }
        public CtrlFlag ControlFlag { get; set; }
        public bool JetOn { get; set; }       
        public int LineNumber { get; set; }
        public double CumulativeTime { get; set; }
        public double TravelTime { get; set; }
        public bool ContainsX { get; set; }
        public bool ContainsY { get; set; }
        public bool ContainsZ { get; set; }
        public bool ContainsF { get; set; }
        public bool ContainsN { get; set; }
        public string InputString { get; set; }
        public double Length { get; set; }
        public PathEntity ()
        {
            
            Position = new CNCLib.MachinePosition();
            PrevPosition = new CNCLib.MachinePosition();
            Type = BlockType.COMMAND;
            Ccomp = CComp.NoChange;
            ControlFlag = CtrlFlag.Unknown;
            DirVector = new Vector3();
            JetVector = new Vector3();
            SurfNormal = new Vector3();
            Feedrate = new Feedrate(FeedrateUnits.InPerMin);
             
        }
       

    }
}
