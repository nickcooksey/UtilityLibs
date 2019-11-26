using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGeometryLib;

namespace IDataLib
{
    public interface IPlyVertex : IVector3
    {
        IVector3 Normal { get; }
        bool ContainsColor { get; set; }
        bool ContainsNormal { get; }
        void AddNormal(IVector3 newNormal);
    }
}
