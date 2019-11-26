namespace IGeometryLib
{
    public interface IRay<T>
    {
        T Origin { get; set; }
        T Direction { get; set; }
        T InverseDir { get; }
        IBoundingBox BoundingBox();
        T PointOnRayAt(double distance);
    }
}
