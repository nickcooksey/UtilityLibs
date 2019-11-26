using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataLib
{
    public interface IPlyProperty
    {
        string Name { get; set; }
        string TypeName { get; set; }
        PlyPropertyType Type { get; set; }
        bool IsList { get; set; }
        string ListCountTypeName { get; set; }

    }
}
