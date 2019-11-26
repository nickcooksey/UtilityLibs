using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGeometryLib;

namespace IDataLib
{
    public interface IDataUtilities<T> : IList<T>
    {
        System.Drawing.Color Color { get; set; }
        string FileName { get; set; }
        void AddRange(IEnumerable<T> vector3);
        IList<T> TrimToWindow(System.Drawing.RectangleF rectangleF, ViewPlane viewPlane);
        IDisplayData AsDisplayData(ViewPlane viewplane);
        List<IDwgEntity> AsDxfData(System.Drawing.Color color);
        List<string> AsCSV();
        IBoundingBox BoundingBox();
    }
}
