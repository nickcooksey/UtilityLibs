using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICNCLib
{
    public interface INcElement
    {
        int LineNumber { get; set; }
        string AsNcString(INcMachine ncMachine);
    }
}
