using IGeometryLib;
using System;

namespace GeometryLib
{
    public class Ray2 : IRay<IVector2>
    {
        public IVector2 Origin { get; set; }
        public IVector2 Direction { get; set; }
        public IVector2 InverseDir { get; private set; }
        public double DirectionTan { get; private set; }
        public double Length { get; private set; }
        public IBoundingBox BoundingBox()
        {
            try
            {
                return new BoundingBox(Origin.X, Origin.Y, 0, Direction.X, Direction.Y, 0);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IVector2 PointOnRayAt(double distance)
        {
            var pt = new Vector2(Origin.X + distance * Direction.X, Origin.Y + distance * Direction.Y);
            return pt;
        }
        public Ray2(Vector2 origin, Vector2 direction)
        {
            Origin = origin;
            direction.Normalize();
            Direction = direction;
            DirectionTan = Direction.Y / Direction.X;
            Length = 1;
        }
        public Ray2(Vector2 origin, Vector2 direction, double length)
        {
            Origin = origin;
            direction.Normalize();
            Direction = direction;
            DirectionTan = Direction.Y / Direction.X;
            Length = length;
        }
    }
    public class Ray3 : IRay<IVector3>
    {
        public IVector3 Origin { get; set; }
        public IVector3 Direction { get; set; }
        public IVector3 InverseDir { get { return inverseDir; } }
        public int[] Sign { get; set; }
        public double TMin { get; set; }
        public double TMax { get; set; }

        private IVector3 inverseDir;
        public Ray3()
        {
            Sign = new int[3];
            TMin = 0;
            TMax = double.MaxValue;
            Origin = new Vector3();
            Direction = new Vector3();
            inverseDir = new Vector3();
        }
        public Ray3(IVector3 origin, IVector3 dir, double minLength, double maxLength)
        {
            inverseDir = new Vector3(1 / dir.X, 1 / dir.Y, 1 / dir.Z);
            Sign = new int[3];
            TMin = minLength;
            TMax = maxLength;
            Sign[0] = inverseDir.X < 0 ? 1 : 0;
            Sign[1] = inverseDir.Y < 0 ? 1 : 0;
            Sign[2] = inverseDir.Z < 0 ? 1 : 0;
            Origin = origin;
            Direction = dir;
        }
        public Ray3(IVector3 origin, IVector3 dir)
        {
            inverseDir = new Vector3(1 / dir.X, 1 / dir.Y, 1 / dir.Z);
            Sign = new int[3];
            TMin = 0;
            TMax = double.MaxValue;
            Sign[0] = inverseDir.X < 0 ? 1 : 0;
            Sign[1] = inverseDir.Y < 0 ? 1 : 0;
            Sign[2] = inverseDir.Z < 0 ? 1 : 0;
            Origin = origin;
            Direction = dir;
        }
        public IBoundingBox BoundingBox()
        {
            return new BoundingBox(Origin.X, Origin.Y, Origin.Z, Direction.X, Direction.Y, Direction.Z);
        }
        public IVector3 PointOnRayAt(double distance)
        {
            var pt = new Vector3(Origin.X + distance * Direction.X, Origin.Y + distance * Direction.Y, Origin.Z + distance * Direction.Z);
            return pt;
        }


    }
}
