using IGeometryLib;
using System;
namespace GeometryLib
{
    public class BoundingBox : IBoundingBox
    {

        public IVector3 Min { get; set; }
        public IVector3 Max { get; set; }
        public IVector3 Size { get { return Max.Minus(Min); } }
        public IVector3 Center
        {
            get
            {
                IVector3 c = new Vector3();
                c.X = (Max.X + Min.X) / 2;
                c.Y = (Max.Y + Min.Y) / 2;
                c.Z = (Max.Z + Min.Z) / 2;
                return c;
            }
        }
        public BoundingBox()
        {
            Max = new Vector3(0, 0, 0);
            Min = new Vector3(0, 0, 0);
        }

        public BoundingBox(double xmin, double ymin, double zmin, double xmax, double ymax, double zmax)
        {
            double xMin = Math.Min(xmin, xmax);
            double yMin = Math.Min(ymin, ymax);
            double zMin = Math.Min(zmin, zmax);
            double xMax = Math.Max(xmin, xmax);
            double yMax = Math.Max(ymin, ymax);
            double zMax = Math.Max(zmin, zmax);

            Min = new Vector3(xMin, yMin, zMin);
            Max = new Vector3(xMax, yMax, zMax);
        }
        public BoundingBox(IVector3 min, IVector3 max)
        {
            Min = min;
            Max = max;
        }
        public bool Overlaps(IBoundingBox box)
        {
            if (Max.X < box.Min.X || Max.Y < box.Min.Y || Max.Z < box.Min.Z || Min.X > box.Max.X || Min.Y > box.Max.Y || Min.Z > box.Max.Z)
            {
                return false;
            }
            return true;
        }

        public bool Contains(IVector3 pt)
        {
            return (pt.X <= Max.X && pt.Y <= Max.Y && pt.Z <= Max.Z && pt.X >= Min.X && pt.Y >= Min.Y && pt.Z >= Min.Z);

        }


    }
}
