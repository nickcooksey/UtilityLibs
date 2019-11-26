using System.Collections.Generic;

namespace IGeometryLib
{
    public interface ITriangle
    {
        IVector3[] Vertices { get; }
        IVector3 Normal { get; }
        uint Index { get; }
        IBoundingBox BoundingBox { get; }
        List<IVector3> AsPointGrid(double pointSpacing);
        List<ITriangle> Split(IRay<IVector3> ray);
        IIntersection IntersectedBy(IRay<IVector3> ray);
        bool Contains(IVector3 pt);
        IIntersection IntersectsTriPlane(IRay<IVector3> ray);
    }
}
