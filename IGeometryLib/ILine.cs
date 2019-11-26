using System.Collections.Generic;

namespace IGeometryLib
{
    public interface IIntersection
    {
        bool Intersects { get; set; }
        IVector3 Point { get; set; }
    }
}
