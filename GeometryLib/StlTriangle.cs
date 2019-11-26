using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawingIO
{
    public class STLTriangle
    {
        public Vector3[] Vertices { get { return vert; } }
        public Vector3 Normal { get { return normal; } }
        public UInt32 Index { get { return index; } }
        public Extents Extents() {  return extents;  }
        

        Vector3[] vert;
        Vector3 normal;
        Extents extents;
        UInt32 index;
       
        private void getExtents()
        {
            Extents[] extArr = new DrawingIO.Extents[]{vert[0].Extents(),vert[1].Extents(),vert[2].Extents()};
            extents = ExtentsBuilder.Union(extArr);
        }   
 
        /// <summary>
        /// splits triangle into 3 smaller triangles from intersect point of ray
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="rayOrigin"></param>
        /// <returns></returns>
        public List<STLTriangle> Split(Ray ray, Vector3 rayOrigin)
        {
            Vector3 pt;
            List<STLTriangle> tris = new List<STLTriangle>();
            if (IntersectedBy(ray, out pt))
            {
                tris.Add(new STLTriangle(vert[0], vert[1], pt, index));
                 tris.Add(new STLTriangle(vert[1], vert[2], pt, index));
                 tris.Add(new STLTriangle(vert[2], vert[0], pt, index));                
            }
            else
            {
                tris.Add(this);
            }
            return tris;

        }
        /// <summary>
        /// tests if triangle is intersected by vector from rayOrigin
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="rayOrigin"></param>
        /// <returns></returns>
        public bool IntersectedBy(Ray ray, out Vector3 projPt)
        {           
            bool intersects = false;            
            
            if (IntersectsPlane(ray, out projPt) && contains(projPt))
            {     
                    intersects = true;
            }    
                       
            return intersects;
        }
        /// <summary>
        /// test if triangle contains point
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        private bool contains(Vector3 pt)
        {
            Vector3 v12 = new Vector3(vert[1].X - vert[0].X, vert[1].Y - vert[0].Y, vert[1].Z - vert[0].Z);
            Vector3 v23 = new Vector3(vert[2].X - vert[1].X, vert[2].Y - vert[1].Y, vert[2].Z - vert[1].Z);
            Vector3 v31 = new Vector3(vert[2].X - vert[0].X, vert[2].Y - vert[0].Y, vert[2].Z - vert[0].Z);

            Vector3 v1pt = new Vector3(vert[0].X - pt.X, vert[0].Y - pt.Y, vert[0].Z - pt.Z);
            Vector3 v2pt = new Vector3(vert[1].X - pt.X, vert[1].Y - pt.Y, vert[1].Z - pt.Z); 
            Vector3 v3pt = new Vector3(vert[2].X - pt.X, vert[2].Y - pt.Y, vert[2].Z - pt.Z);
            double testSide1 = v12.Cross(v1pt).Dot(Normal);
            double testSide2 = v23.Cross(v2pt).Dot(Normal);
            double testSide3 = v31.Cross(v3pt).Dot(Normal);
            if ((testSide1 >= 0) && (testSide2 >= 0) && (testSide3 >= 0))
                return true;
            else
                return false;
        }
        /// <summary>
        /// test if ray projects onto triangle plane returns true and projected point in out
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="rayOrigin"></param>
        /// <param name="projPt"></param>
        /// <returns></returns>
        public bool IntersectsPlane(Ray ray,  out Vector3 projPt)
        {
            
            Vector3 op = new Vector3(vert[0].X - ray.Origin.X, vert[0].Y - ray.Origin.Y, vert[0].Z - ray.Origin.Z);
            ray.Direction.Normalize();
            double denom = ray.Direction.Dot(Normal);
            if (denom != 0)
            {
                double t = -1 * op.Dot(Normal) / denom;
                projPt = new Vector3(ray.Origin.X + t * ray.Direction.X, ray.Origin.Y + t * ray.Direction.Y, ray.Origin.Z + t * ray.Direction.Z);

                return true;
            }
            else
            {
                projPt = new Vector3();
                return false;
            }
        }
        public STLTriangle()
        {
            vert = new Vector3[3];
            normal = new Vector3();
            index = 0;
            extents = new Extents();
        }
        public STLTriangle(Vector3 v0, Vector3 v1, Vector3 v2, UInt32 index)
        {
            vert = new Vector3[3];
            vert[0] = v0;
            vert[1] = v1;
            vert[2] = v2;
            Vector3 v12 = new Vector3(vert[1].X - vert[0].X, vert[1].Y - vert[0].Y, vert[1].Z - vert[0].Z);
            Vector3 v23 = new Vector3(vert[2].X - vert[1].X, vert[2].Y - vert[1].Y, vert[2].Z - vert[1].Z);
            normal = v12.Cross(v23);
            this.index = index;
            getExtents();
        }
        public STLTriangle(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 norm, UInt32 index)
        {
            vert = new Vector3[3];
            vert[0] = v0;
            vert[1] = v1;
            vert[2] = v2;
            normal = norm;
            this.index = index;
            getExtents();
        }
    }
}
