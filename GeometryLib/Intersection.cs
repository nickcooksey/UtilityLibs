using IGeometryLib;
namespace GeometryLib
{

    public class Intersection3 : IIntersection
    {

        public bool Intersects { get; set; }
        public IVector3 Point { get; set; }
        public Intersection3()
        {
            Intersects = false;
            Point = new Vector3();
        }
        public Intersection3(IVector3 intersection, bool intersects)
        {
            Point = new Vector3
            {
                X = intersection.X,
                Y = intersection.Y,
                Z = intersection.Z
            };
            Intersects = intersects;
        }
    }
}
