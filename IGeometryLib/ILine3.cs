using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGeometryLib
{
    public interface ILine3 : IDwgEntity
    {
        IVector3 Point1 { get; set; }
        IVector3 Point2 { get; set; }
        double Length { get; }
        double SlopeXY { get; }
        ILine3 Translate(IVector3 vector3);
        IIntersection Intersects(ILine3 ray2);
        ILine3 RotateX(IVector3 rotationPt, double angleRad);
        ILine3 RotateY(IVector3 rotationPt, double angleRad);
        ILine3 RotateZ(IVector3 rotationPt, double angleRad);
        void ParseDxf(List<string> fileSection);
    }
}
