namespace GeometryLib
{
    public class Box
    {

        public Vector3 Max { get; private set; }

        public Vector3 Min { get; private set; }

        private Vector3[] _bounds;
        private Vector3 _max;
        private Vector3 _min;
        public Box(Vector3 min, Vector3 max)
        {
            _bounds = new Vector3[2];
            _min = min;
            _max = max;

            _bounds[0] = _min;
            _bounds[1] = _max;
        }

        public bool IntersectedBy(ref Ray3 r)
        {
            double tmin = (_bounds[r.Sign[0]].X - r.Origin.X) * r.InverseDir.X;
            double tmax = (_bounds[1 - r.Sign[0]].X - r.Origin.X) * r.Direction.X;

            double tymin = (_bounds[r.Sign[1]].Y - r.Origin.Y) * r.Direction.Y;
            double tymax = (_bounds[1 - r.Sign[1]].Y - r.Origin.Y) * r.Direction.Y;
            if ((tmin > tmax) || (tymin > tymax))
            {
                return false;
            }
            if ((tmin > tymax) || (tymin > tmax))
            {
                return false;
            }
            if (tymin > tmin)
            {
                tmin = tymin;
            }
            if (tymax < tmax)
            {
                tmax = tymax;
            }
            double tzmin = (_bounds[r.Sign[2]].Z - r.Origin.Z) / r.Direction.Z;
            double tzmax = (_bounds[1 - r.Sign[2]].Z - r.Origin.Z) / r.Direction.Z;
            if ((tmin > tmax) || (tzmin > tzmax))
            {
                return false;
            }
            if ((tmin > tzmax) || (tzmin > tmax))
            {
                return false;
            }
            if (tzmin > tmin)
            {
                tmin = tzmin;
            }
            if (tzmax < tmax)
            {
                tmax = tzmax;
            }
            if ((tmin > r.TMax) || (tmax < r.TMin))
            {
                return false;
            }
            if (r.TMin < tmin)
            {
                r.TMin = tmin;
            }
            if (r.TMax > tmax)
            {
                r.TMax = tmax;
            }
            return true;
        }
    }
}
