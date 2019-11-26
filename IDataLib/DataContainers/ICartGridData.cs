using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGeometryLib;

namespace IDataLib
{
    public interface ICartGridData : IList<ICartData>
    {
        void AddRange(IEnumerable<ICartData> vector3s);
        IBoundingBox BoundingBox();
    }
}
