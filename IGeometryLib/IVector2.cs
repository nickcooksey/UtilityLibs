using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGeometryLib
{
    public interface IVector2 : IDwgEntity, IGeometryRoutines<IVector2>
    {
        double X { get; set; }
        double Y { get; set; }
        double Length { get; }
        IVector2 Plus(IVector2 vector3);
        IVector2 Minus(IVector2 vector3);
        IVector2 MultiplyBy(double scalar);
        double DistanceTo(IVector2 p2);
    }
}
