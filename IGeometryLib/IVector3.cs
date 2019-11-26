using System.Collections.Generic;

namespace IGeometryLib
{
    
    public interface IVector3 : IDwgEntity, IGeometryRoutines<IVector3>
    {
        double X { get; set; }
        double Y { get; set; }
        double Z { get; set; }
        double Length { get; }
        string AsCSV();
        void ParseDXF(List<string> fileSection);
        double Dot(IVector3 v2);
        IVector3 Cross(IVector3 vector3);
        double DistanceTo(IVector3 p2);
        void Normalize();
        IVector3 Plus(IVector3 vector3);
        IVector3 Minus(IVector3 vector3);
        bool Equals(IVector3 vector3);
        bool NotEqualTo(IVector3 vector3);
        IVector3 MultiplyBy(double scalar);
        IVector3 InterpolateX(double x, IVector3 pt0, IVector3 pt1);

    }
}
