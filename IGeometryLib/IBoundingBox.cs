namespace IGeometryLib
{
    public interface IBoundingBox
    {
        IVector3 Min { get; set; }
        IVector3 Max { get; set; }
        IVector3 Size { get; }
        IVector3 Center { get; }
        bool Contains(IVector3 pt);
        bool Overlaps(IBoundingBox box);
    }
}
