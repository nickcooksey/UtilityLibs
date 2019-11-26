namespace IGeometryLib
{
    public interface IGeometryRoutines<T> where T : IDwgEntity
    {
        T RotateX(IVector3 rotationPt, double angleRad);
        T RotateY(IVector3 rotationPt, double angleRad);
        T RotateZ(IVector3 rotationPt, double angleRad);
        T Translate(IVector3 translation);
        T Clone();
        IBoundingBox BoundingBox();
    }
}
