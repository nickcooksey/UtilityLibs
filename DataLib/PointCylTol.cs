using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryLib;
using IDataLib;


namespace DataLib
{
    public class PointCylTol : PointCyl, IPointCylTol
    {
        public double RMax { get; set; }
        public double RMin { get; set; }
        public double RNom { get { return (RMax + RMin) / 2.0; } }
    }
}
