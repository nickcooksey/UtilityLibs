using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICNCLib
{
    public interface INcFile:IList<INcElement>
    {
        List<string> AsNcTextFile(INcMachine ncMachine);
        void AddRange(IEnumerable<INcElement> ncLines);
    }
}
