using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDataLib
{
    public interface IPlyEdge
    {
        int Vertex1 { get; set; }
        int Vertex2 { get; set; }
        System.Drawing.Color Color { get; set; }
        bool ContainsColor { get; set; }
    }
}
