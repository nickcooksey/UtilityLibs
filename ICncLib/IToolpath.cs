using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICNCLib
{
    public  interface IToolpath:IList<IPathEntity>
    {
        int[] MiscIntegerArr { get; set; }
        double[] MiscRealArr { get; set; }
        int ProgNumber { get; set; }
        int SeqIncrement { get; set; }
        int StartNumber { get; set; }
        int ToolNumber{ get; set; }
        int ToolDiamNumber { get; set; }
        int ToolLengthNumber { get; set; }
        double NomFeedrate { get; set; }
        IMachinePosition Home { get; set; }     
        double OffsetDist { get; set; }
        double ToolDiameter { get; set; }
        int OpCode { get; set; }
        int CutPathCount { get; set; }
        string FilePath { get; set; }
        string OutputFileName { get; set; }
        string InputFileName { get; set; }
        string Title { get; set; }
    }
}
