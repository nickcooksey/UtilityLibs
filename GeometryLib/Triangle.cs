using IGeometryLib;
using System;
using System.Collections.Generic;

namespace GeometryLib
{
    public class Triangle : ITriangle
    {
        public IVector3[] Vertices { get { return vert; } }
        public IVector3 Normal { get { return normal; } }
        public uint Index { get { return index; } }
        public IBoundingBox BoundingBox
        {
            get { return boundingBox; }
        }

        private IVector3 v01;
        private IVector3 v12;
        private IVector3 v20;
        private IVector3[] vert;
        private IVector3 normal;
        private IBoundingBox boundingBox;
        private UInt32 index;
        public Triangle()
        {
            vert = new Vector3[3];
            normal = new Vector3();
            index = 0;
            boundingBox = new BoundingBox();
        }
        public Triangle(IVector3 v0, IVector3 v1, IVector3 v2)
        {
            vert = new Vector3[3];
            vert[0] = v0;
            vert[1] = v1;
            vert[2] = v2;

            this.index = 0;
            getBoundingBox();
            GetSideVectors();
            normal = v01.Cross(v12);
            normal.Normalize();
        }

        public Triangle(IVector3 v0, IVector3 v1, IVector3 v2, UInt32 index)
        {
            IVector3[] vert = new Vector3[3];
            vert[0] = v0;
            vert[1] = v1;
            vert[2] = v2;

            this.index = index;
            getBoundingBox();
            GetSideVectors();
            normal = v01.Cross(v12);
            normal.Normalize();
        }
        public Triangle(IVector3 v0, IVector3 v1, IVector3 v2, IVector3 norm, UInt32 index)
        {
            vert = new Vector3[3];
            vert[0] = v0;
            vert[1] = v1;
            vert[2] = v2;

            normal = norm;
            normal.Normalize();
            this.index = index;
            getBoundingBox();
            GetSideVectors();
        }
        private void getBoundingBox()
        {
            boundingBox = BoundingBoxBuilder.FromPtArray(vert);
        }
        public List<IVector3> AsPointGrid(double pointSpacing)
        {
            try
            {
                var gridPoints = new List<IVector3>();
                var side1Points = new List<IVector3>();
                var side2Points = new List<IVector3>();
                var side12 = new Line3(vert[1], vert[2]);
                var side01 = new Line3(vert[0], vert[1]);
                var side02 = new Line3(vert[0], vert[2]);
                var v20 = vert[0].Minus(vert[2]);
                var v02 = vert[2].Minus(vert[0]);
                var v01 = vert[1].Minus(vert[0]);
                var v12 = vert[2].Minus(vert[1]);
                var v21 = vert[1].Minus(vert[2]);
                var v10 = vert[0].Minus(vert[1]);
                double theta0 = Math.Acos(v01.Dot(v02) / (v01.Length * v02.Length));
                double theta1 = Math.Acos(v10.Dot(v12) / (v01.Length * v12.Length));
                double theta2 = Math.Acos(v20.Dot(v21) / (v02.Length * v12.Length));
                double thetaMax = Math.Max(theta1, Math.Max(theta0, theta2));
                double side1Spacing = pointSpacing / Math.Sin(thetaMax);
                IVector3 basePoint = new Vector3(vert[0]);
                if (theta0 == thetaMax)
                {
                    side1Points.AddRange(side01.BreakMany(side1Spacing));
                    side2Points.AddRange(side02.BreakMany(pointSpacing));
                    basePoint = vert[0];
                }
                else
                {
                    if (theta1 == thetaMax)
                    {
                        side1Points.AddRange(side01.BreakMany(side1Spacing));
                        side2Points.AddRange(side12.BreakMany(pointSpacing));
                        basePoint = vert[1];
                    }
                    else
                    {
                        if (theta2 == thetaMax)
                        {
                            side1Points.AddRange(side02.BreakMany(side1Spacing));
                            side2Points.AddRange(side12.BreakMany(pointSpacing));
                            basePoint = vert[2];
                        }
                    }
                }

                foreach (IVector3 side1Point in side1Points)
                {
                    var translation = side1Point.Minus(basePoint);
                    foreach (IVector3 side2Point in side2Points)
                    {
                        var s2Trans = side2Point.Translate(translation);
                        if (Contains(s2Trans))
                        {
                            gridPoints.Add(s2Trans);
                        }
                    }
                }
                return gridPoints;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<ITriangle> Split(IRay<IVector3> ray)
        {
            try
            {

                List<ITriangle> tris = new List<ITriangle>();
                IIntersection pt = IntersectedBy(ray);
                if (pt.Intersects)
                {
                    tris.Add(new Triangle(vert[0], vert[1], pt.Point, index));
                    tris.Add(new Triangle(vert[1], vert[2], pt.Point, index));
                    tris.Add(new Triangle(vert[2], vert[0], pt.Point, index));
                }
                else
                {
                    tris.Add(this);
                }
                return tris;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public IIntersection IntersectedBy(IRay<IVector3> ray)
        {
            try
            {
                IIntersection pt = IntersectsTriPlane(ray);
                if (pt.Intersects && Contains(pt.Point))
                {
                    pt.Intersects = true;
                }
                else
                {
                    pt.Intersects = false;
                }
                return pt;
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// test if triangle contains point
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public bool Contains(IVector3 pt)
        {
            try
            {
                GetSideVectors();
                IVector3 v0pt = pt.Minus(vert[0]);
                IVector3 v1pt = pt.Minus(vert[1]);
                IVector3 v2pt = pt.Minus(vert[2]);
                double testSide0 = v01.Cross(v0pt).Dot(Normal);
                double testSide1 = v12.Cross(v1pt).Dot(Normal);
                double testSide2 = v20.Cross(v2pt).Dot(Normal);
                return ((testSide0 >= 0) && (testSide1 >= 0) && (testSide2 >= 0));

            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// test if ray projects onto triangle plane returns true and projected point in out
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="rayOrigin"></param>
        /// <param name="projPt"></param>
        /// <returns></returns>
        public IIntersection IntersectsTriPlane(IRay<IVector3> ray)
        {
            try
            {
                var op = ray.Origin.Minus(vert[0]);
                ray.Direction.Normalize();
                double denom = ray.Direction.Dot(Normal);
                if (denom != 0)
                {
                    double t = -1 * op.Dot(Normal) / denom;
                    var projPt = ray.Origin.Plus(ray.Direction.MultiplyBy(t));

                    return new Intersection3(projPt, true);
                }
                else
                {
                    return new Intersection3();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void GetSideVectors()
        {
            v01 = vert[1].Minus(vert[0]);
            v12 = vert[2].Minus(vert[1]);
            v20 = vert[0].Minus(vert[2]);
        }

    }
}
