using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGeometryLib
{
    public interface ILine2 : IDwgEntity
    {
        IVector2 Point1 { get; set; }
        IVector2 Point2 { get; set; }
        double SlopeXY { get; }
        double Length { get; }
        IIntersection Intersects(ILine2 ray2);
    }
}
