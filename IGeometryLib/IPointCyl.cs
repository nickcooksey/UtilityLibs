namespace IGeometryLib
{
    public interface IPointCyl : IDwgEntity, IGeometryRoutines<IPointCyl>
    {
        double R { get; set; }
        double ThetaRad { get; set; }
        double Z { get; set; }
        double ThetaDegPosOnly { get; }
        double ThetaDeg { get; }
        IVector3 UnrollCylPt(double scaling, double unrollingRadius);
        IPointCyl InterpolateR(double radius, IPointCyl ptLow, IPointCyl ptHigh);
    }
}
