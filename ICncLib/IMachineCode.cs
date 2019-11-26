using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICNCLib
{
    public interface IMachineCode
    {
         string InverseFeed { get; set; }
         string Rapid { get; set; }
         string LinearMove { get; set; }
         string FiveAxis { get; set; }
         string CwArc { get; set; }
         string CcwArc { get; set; }
         string Delay { get; set; }
         string Absolute { get; set; }
         string Relative { get; set; }
         string LineNumberPrefix { get; set; }
         string DelayAmountPrefix { get; set; }
         string FeedratePrefix { get; set; }
         string EndofProg { get; set; }
         string LinearAxisFormat { get; set; }
         string RotaryAxisFormat { get; set; }
         string FeedrateFormat { get; set; }
         string[] AxisNames { get; set; }
         int AxisCount { get; set; }
         string Sp { get; set; }
         string ComStart { get; set; }
         string ComEnd { get; set; }
         string HeaderStart { get; set; }
         string HeaderEnd { get; set; }
         int CommentMaxLength { get; set; }
         int StartingLineNumber { get; set; }
       
         char[] ForbiddenChars { get; set; }
         double DelayScaleFactor { get; set; }
         string DelayStringFormat { get; set; }

         string McodeFilename { get; set; }
         FeedrateUnits FeedrateUnits { get; set; }
        FeedrateUnits InverseFeedUnits { get; set; }
         int RotaryAxisPrecision { get; set; }
         int LinearAxisPrecision { get; set; }
         int FeedratePrecision { get; set; }
         
         int LineNumberInc { get; set; }
    }
}
