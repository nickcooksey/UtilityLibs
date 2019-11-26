namespace IGeometryLib
{
    public interface IArc : IDwgEntity, IGeometryRoutines<IArc>
    {
        IVector3 Center { get; set; }
        double Radius { get; set; }
        double StartAngleRad { get; set; }
        double EndAngleRad { get; set; }
        double StartAngleDeg { get; }
        double EndAngleDeg { get; }
        bool ClosedArc { get; set; }
        IIntersection Intersects(ILine3 ray2);

    }
}
