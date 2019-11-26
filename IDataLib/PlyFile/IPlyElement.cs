using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataLib
{
    public interface IPlyElement
    {
        PlyElementType Type { get; }
        string Name { get; set; }
        bool ContainsVertex { get; set; }
        bool ContainsNormal { get; set; }
        bool ContainsColor { get; set; }
        int Count { get; set; }
        List<IPlyProperty> Properties { get; }
        void AddProperty(IPlyProperty property);
    }
}
