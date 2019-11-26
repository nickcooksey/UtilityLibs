using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGeometryLib;
namespace ICNCLib
{
    public interface IPathEntity
    {
         BlockType Type { get; set; }
         IMachinePosition Position { get; set; }
         IMachinePosition PrevPosition { get; set; }              
         CComp Ccomp { get; set; }
         IVector3 JetVector { get; set; }
         IVector3 SurfNormal { get; set; }
         IFeedrate Feedrate { get; set; }
         CtrlFlag ControlFlag { get; set; }
         bool JetOn { get; set; }
         string Comment { get; set; }
         int LineNumber { get; set; }               
         double CumulativeTime { get; set; }
         double TravelTime { get; set; }
         bool ContainsX { get; set; }
         bool ContainsY { get; set; }
         bool ContainsZ { get; set; }
         bool ContainsF { get; set; }        
         string InputString { get; set; }
         double Length { get; set; }
    }
}
