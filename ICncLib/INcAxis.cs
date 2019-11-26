using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICNCLib
{
    public interface INcAxis
    {
        string Name { get; set; }
        AxisType Type { get; set; }
        int DecimalPlaces { get; set; }

    }
}
