using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGeometryLib;

namespace IDataLib
{
    public interface ICylGridData : IList<ICylData>
    {
        System.Drawing.Color Color { get; set; }
        void AddRange(IEnumerable<ICylData> pointCyls);
        ICylData AsCylData();
        List<IDwgEntity> AsDxfData(System.Drawing.Color color);
        ICartGridData Unroll(double scaling, double unrollRadius);
        IBoundingBox BoundingBox();
    }
}
