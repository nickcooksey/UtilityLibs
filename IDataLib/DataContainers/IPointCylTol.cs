using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGeometryLib;

namespace IDataLib
{
    public interface IPointCylTol : IPointCyl
    {
        double RMax { get; set; }
        double RMin { get; set; }
        double RNom { get; }
    }
}
