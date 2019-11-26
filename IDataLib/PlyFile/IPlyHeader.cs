using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataLib
{
    public interface IPlyHeader
    {
        IList<IPlyElement> Elements { get; set; }
    }
}
