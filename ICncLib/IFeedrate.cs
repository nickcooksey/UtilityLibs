using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICNCLib
{
    public interface IFeedrate
    {
        bool Inverted { get;  }
        double Value { get; set; }
        FeedrateUnits Units { get;   }
        void SetInverseFeedrate(double moveLength, double time);
        double MoveTimeSeconds(double moveLength);
        string AsNcString(INcMachine ncMachine);
         
    }
}
